using System.Numerics;

namespace HMCSEngine
{
    internal static class LevelHeader
    {
        public static float ParallaxMagnitude = 8.0f;

        public static bool TileLoopOver = true;

        public static bool BoundedCamera = false;
        public static CameraBounds CameraBounds = new CameraBounds(-Vector2.One * 16 * 5, Vector2.One * 16 * 5);

        public static string BackgroundSongName = "";

        public static void LoadLevelHeader(byte id)
        {
            //TODO load header
        }
    }
}
