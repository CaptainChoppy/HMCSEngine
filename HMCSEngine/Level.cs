using Raylib_cs;

namespace HMCSEngine
{
    internal static class Level
    {
        private static byte levelid = 0;
        public static byte LevelID
        {
            get
            {
                return levelid;
            }
            private set
            {
                levelid = value;
            }
        }

        private static List<Entity> Entities = new List<Entity>();

        public static Color BackgroundClearColour { get; private set; }

        private static ushort entitycounter = 0;
        public static ushort EntityCounter
        {
            get
            {
                ushort counter = entitycounter;
                entitycounter++;
                return counter;
            }
            private set
            {
                entitycounter = value;
            }
        }

        public static void LoadLevel(byte id)
        {
            LevelID = id;
            EntityCounter = 0;

            UnloadCurrentEntities();
            TileAtlases.UnloadAtlases();
            HMCSEntityData.UnloadAllEntityAtlases();

            LevelHeader.LoadLevelHeader(id);
            LevelBackground.LoadBackground();
            TileAtlases.LoadAtlases();
            Tilemap.LoadTileMap();
            LoadEntities();
        }

        private static void UnloadCurrentEntities()
        {
            List<Entity> entitiestoremove = new List<Entity>(Entities);

            foreach (Entity e in entitiestoremove)
            {
                Entities.Remove(e);
            }
        }

        private static void LoadEntities()
        {
            SpawnEntity(TilePosition.One * 8, new EntityDataID(0));
            SpawnEntity(TilePosition.One * 9, new EntityDataID(1));
            SpawnEntity(TilePosition.One * 10, new EntityDataID(2));
        }

        private static void SpawnEntity(TilePosition position, EntityDataID id)
        {
            EntityData data = HMCSEntityData.GetEntityData(id);

            if(data == null)
            {
                Debug.WarningLog($"Entity {id} does not exist.");
                return;
            }

            Entity entity = new Entity(position, HMCS.Player, id, EntityCounter);
            Entities.Add(entity);
        }

        public static void Update()
        {
            foreach (Entity e in Entities)
            {
                e.Update();
            }
        }

        public static void DrawLayer(TileLayerName layer)
        {
            Tilemap.DrawLayer(layer);
        }

        public static void DrawEntities()
        {
            foreach (Entity e in Entities)
            {
                e.Draw();
            }
        }
    }
}
