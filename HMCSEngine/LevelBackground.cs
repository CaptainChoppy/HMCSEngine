using Raylib_cs;
using System.Numerics;

namespace HMCSEngine
{
    internal static class LevelBackground
    {
        public const int TextureWidth = 1024;
        public const int TextureHeight = 512;

        public static int ParallaxMagnitude = 8;

        public static Texture? Background;

        public static void LoadBackground()
        {
            if(Background != null)
            {
                Background.Dispose();
                Background = null;
            }

            Background = new Texture(Files.CurrentLevelBackgroundFilePath);
        }

        public static void Draw()
        {
            if(Background == null)
            {
                return;
            }

            BackgroundPosition backgroundposition = (BackgroundPosition)(Viewport.Position);

            for(int x = backgroundposition.x - 1; x <= backgroundposition.x + 1; x++)
            {
                for (int y = backgroundposition.y - 1; y <= backgroundposition.y + 1; y++)
                {
                    Renderer.DrawTexture(Background.GetTexture(), new Rectangle(Vector2.Zero, new Vector2(2, 1) * TextureHeight), (WorldPosition)(new BackgroundPosition(x, y)));
                }
            }
        }
    }
}
