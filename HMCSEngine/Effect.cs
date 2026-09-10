using Raylib_cs;

namespace HMCSEngine
{
    internal abstract class Effect
    {
        public abstract void Update();
        public abstract void Draw();
    }

    internal class ColourTintEffect : Effect
    {
        public Color Colour;

        public ColourTintEffect(Color colour)
        {
            Colour = colour;
        }

        public override void Update()
        {
            return;
        }

        public override void Draw()
        {
            Raylib.DrawRectangle(0, 0, Renderer.WindowWidth, Renderer.WindowHeight, Colour);
        }
    }
}
