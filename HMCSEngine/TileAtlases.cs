
namespace HMCSEngine
{
    internal static class TileAtlases
    {
        public const int AtlasWidth = 256;
        public const int AtlasHeight = 256;

        public const byte MaxAtlases = 16;

        public static TileAtlas?[] Atlases = new TileAtlas?[MaxAtlases];

        private static byte NumberOfAtlases = 1;

        public static void LoadAtlases()
        {
            for (byte i = 0; i < MaxAtlases; i++)
            {
                if(File.Exists(Files.GetLevelAtlasFilePath(i)) == false)
                {
                    NumberOfAtlases = i;
                    break;
                }

                LoadAtlas(i);
            }

            if(NumberOfAtlases != 0)
            {
                return;
            }

            NumberOfAtlases = 1;
            Atlases[0] = HMCS.DefaultTexture;

            Debug.ErrorLog("Failed to load any tile atlases so using default");
        }

        /// <summary>
        /// Loads the atlas with the index from the current level directory
        /// </summary>
        /// <param name="index">The index number of the atlas</param>
        private static void LoadAtlas(byte index)
        {
            try
            {
                Atlases[index] = new TileAtlas(index);
            }
            catch(Exception)
            {
                Debug.ErrorLog($"Failed to create tile atlas {index}");
            }
        }

        public static TileAtlas? GetCurrentAtlas()
        {
            if(NumberOfAtlases == 0 || Time.GlobalFrameTime % NumberOfAtlases > MaxAtlases)
            {
                return null;
            }

            return Atlases[Time.GlobalFrameTime % NumberOfAtlases];
        }

        public static void UnloadAtlases()
        {
            for(int i = 0; i < MaxAtlases; i++)
            {
                TileAtlas? atlas = Atlases[i];

                if (atlas != null)
                {
                    atlas.Dispose();
                }

                Atlases[i] = null;
            }
        }
    }
}