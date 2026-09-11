using Raylib_cs;
using System.Numerics;

namespace HMCSEngine
{
    internal static class Renderer
    {
        public static int TileHorizontalViewDistance => Screen.ReferenceWindowWidth / 16;
        public static int TileVerticalViewDistance => Screen.ReferenceWindowHeight / 16;

        public static bool ToggleDowner = true;
        public static bool ToggleUpper = true;
        public static bool ToggleHigher = true;

        public static void CreateWindow()
        {
            Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
            Raylib.SetTargetFPS(45);
            Raylib.InitWindow(Screen.Width, Screen.Height, HMCS.WindowTitle);
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Level.BackgroundClearColour);

            LevelBackground.Draw();

            if(ToggleDowner == true)
            {
                Level.DrawLayer(TileLayerName.Downer);
            }

            if (ToggleUpper == true)
            {
                Level.DrawLayer(TileLayerName.Upper);
            }

            Level.DrawEntities();
            HMCS.Player.Draw();

            if (ToggleHigher == true)
            {
                Level.DrawLayer(TileLayerName.Higher);
            }

            //GUI.Draw();

            Raylib.EndDrawing();
        }

        public static void CloseWindow()
        {
            Raylib.CloseWindow();
        }

        public static void DrawTexture(Texture texture, Rectangle texturesample, WorldPosition worldposition)
        {
            ScreenPosition screenposition = (ScreenPosition)(worldposition);

            ScreenPosition objectorigin = screenposition + (ScreenPosition.Down * Maths.FloorToInt(texturesample.Size.Y));

            Rectangle screenrectangle = new Rectangle(objectorigin, texturesample.Size * WindowScale);

            Raylib.DrawTexturePro(texture.GetTexture(), texturesample, screenrectangle, Vector2.Zero, 0, Color.White);
        }

        public static void DrawText(string text, int fontindex, int fontsize, int spacing, Color colour, WorldPosition position)
        {
            Raylib.DrawTextPro(Fonts.GetFont(fontindex), text, (ScreenPosition)(position), Vector2.Zero, 0.0f, fontsize * WindowScale, spacing * WindowScale, colour);
        }
        public static void DrawText(string text, int fontindex, int fontsize, int spacing, Color colour, ScreenPosition position)
        {
            Raylib.DrawTextPro(Fonts.GetFont(fontindex), text, position, Vector2.Zero, 0.0f, fontsize * WindowScale, spacing * WindowScale, colour);
        }

        public static void DrawScreenPixel(ScreenPosition position, Color color)
        {
            Raylib.DrawPixel(Maths.FloorToInt(position.x), Maths.FloorToInt(position.y), color);
        }
    }
}
