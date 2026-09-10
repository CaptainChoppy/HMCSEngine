
namespace HMCSEngine
{
    internal static class HMCSEntityData
    {
        public const int MaxUniqueEntities = 256;

        public static readonly EntityData PlayerEntityData = new EntityData();

        private static EntityData[] EntityData = new EntityData[MaxUniqueEntities];

        public static void LoadEntityData()
        {
            JSONEntityDataArray? entitydata;

            try
            {
                entitydata = JSONReader.Read<JSONEntityDataArray>(Files.EntityDataDirectory);

                if (entitydata == null)
                {
                    throw new NullReferenceException("JSONReader.Read returned null.");
                }
            }
            catch(Exception e)
            {
                Debug.FatalLog($"Failed to load the entity data file at \\entities\\entitydata.json Exception : {e}");
                throw;
            }

            EntityData = JSONEntityDataArray.CreateEntityDataFromJSON(entitydata);
        }

        public static EntityData GetEntityData(EntityDataID? id)
        {
            if (id == null)
            {
                return PlayerEntityData;
            }

            return EntityData[id.ID];
        }

        public static void UnloadAllEntityAtlases()
        {
            for(int i = 0; i < MaxUniqueEntities; i++)
            {
                EntityData entitydata = EntityData[i];

                if (entitydata == null)
                {
                    continue;
                }

                if (entitydata.Atlas == null)
                {
                    continue;
                }

                entitydata.Atlas.Dispose();
                entitydata.Atlas = null;
            }
        }

        public static void LoadEntityAtlas(EntityDataID id)
        {
            EntityData[id.ID].LoadAtlas(id);
        }
    }

    internal sealed class EntityData
    {
        public readonly string Name;
        public readonly SpriteRenderMode RenderMode;
        public readonly EntityBehaviour Behaviour;
        public Texture? Atlas;

        public EntityData()
        {
            Name = "Player";
            RenderMode = SpriteRenderMode.T1x2;
            Behaviour = EntityBehaviour.None;
            Atlas = new Texture(Files.PlayerAtlasFilePath);
        }

        public EntityData(string name, SpriteRenderMode rendermode, EntityBehaviour behaviour, EntityDataID id)
        {
            Name = name;
            RenderMode = rendermode;
            Behaviour = behaviour;
        }

        public void LoadAtlas(EntityDataID id)
        {
            if(Atlas != null)
            {
                return;
            }

            Atlas = new Texture(Files.GetResourcesEntitiesSpriteAtlasFilePath(id.ID));
        }
    }

    internal sealed class JSONEntityDataArray
    {
        public readonly JSONEntityData[] Entities;

        public JSONEntityDataArray(JSONEntityData[] entities)
        {
            Entities = entities;
        }

        public static EntityData[] CreateEntityDataFromJSON(JSONEntityDataArray json)
        {
            EntityData[] entitydata = new EntityData[HMCSEntityData.MaxUniqueEntities];

            List<byte> loadedids = new List<byte>();

            for(int i = 0; i < json.Entities.Length; i++)
            {
                JSONEntityData entity = json.Entities[i];

                if (loadedids.Contains(entity.ID) == true)
                {
                    Debug.WarningLog($"Entity id {entity.ID} appears more than once in \\entities\\entitydata.json");
                    continue;
                }

                loadedids.Add(entity.ID);

                entitydata[entity.ID] = new EntityData(entity.Name, (SpriteRenderMode)(entity.RenderMode), (EntityBehaviour)(entity.Behaviour), new EntityDataID(entity.ID));
            }

            return entitydata;
        }
    }
    internal sealed class JSONEntityData
    {
        public readonly byte ID;
        public readonly string Name;
               
        public readonly int RenderMode;
        public readonly int Behaviour;

        public JSONEntityData(byte id, string name, int rendermode, int behaviour)
        {
            ID = id;
            Name = name;
            RenderMode = rendermode; 
            Behaviour = behaviour;
        }

        public override string ToString()
        {
            return $"ID:{ID}, Name:{Name}, RenderMode:{RenderMode}, Behaviour:{Behaviour}";
        }
    }

}
