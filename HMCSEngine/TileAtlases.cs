
namespace HMCSEngine
{
    internal static class TileAtlases
    {
        public const int AtlasWidth = 256;
        public const int AtlasHeight = 256;

        public const byte MaxAtlases = 16;

        public static TileAtlas?[] Atlases = new TileAtlas[MaxAtlases];

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

            if(NumberOfAtlases == 0)
            {
                Debug.FatalLog("Failed to load any tile atlases");
                throw new FileLoadException();
            }
        }

        private static void LoadAtlas(byte index)
        {
            Atlases[index] = new TileAtlas(index);
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

    internal sealed class TileAtlas : IDisposable
    {
        public readonly Texture Atlas;

        public TileAtlas(byte index)
        {
            Atlas = new Texture(Files.GetLevelAtlasFilePath(index));
        }

        public void Dispose()
        {
            Atlas.Dispose();
        }
    }
}