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

        public static ushort EntityCounter { get; private set; }

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

        /// <summary>
        /// Spawns an entity with the ID at the position
        /// </summary>
        /// <param name="position">The position where the entity will spawn</param>
        /// <param name="id">The ID of the entity to spawn</param>
        /// <exception cref="InvalidEntityException">Thrown when the entity with the ID does not exist</exception>
        public static void SpawnEntity(TilePosition position, EntityDataID id)
        {
            EntityData? data = HMCSEntityData.GetEntityData(id);

            if(data == null)
            {
                throw new InvalidEntityException();
            }

            Entity entity = new Entity(position, HMCS.Player, id, EntityCounter);
            Entities.Add(entity);
            EntityCounter++;
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

    public sealed class InvalidEntityException : Exception
    {
        public InvalidEntityException() : base() { }
        public InvalidEntityException(string message) : base(message) { }
    }
}
