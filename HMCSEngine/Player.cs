using Raylib_cs;
using System.Numerics;

namespace HMCSEngine
{
    internal sealed class Player : Entity
    {
        public bool FreezeInputs = false;

        public Player(TilePosition position) : base(position, null, null, 0)
        {
            Viewport.Parent = this;
            Viewport.Offset = new Vector2(-(9.5f * Tile.TileWidth), -(5.5f * Tile.TileWidth));
            ColorTint = Color.White;
        }

        public override void Update()
        {
            GetInputs();
            Move();
        }

        private void GetInputs()
        {
            if(FreezeInputs == true)
            {
                return;
            }

            if (Inputs.KeyDown(VKeyCodes.UpArrow))
            {
                Direction = WorldPosition.Up;
            }
            else if(Inputs.KeyDown(VKeyCodes.DownArrow))
            {
                Direction = WorldPosition.Down;
            }
            else if (Inputs.KeyDown(VKeyCodes.RightArrow))
            {
                Direction = WorldPosition.Right;
            }
            else if (Inputs.KeyDown(VKeyCodes.LeftArrow))
            {
                Direction = WorldPosition.Left;
            }
            else
            {
                Direction = WorldPosition.Zero;
            }

            if (Inputs.KeyPressed(VKeyCodes.Q))
            {
                Renderer.ToggleDowner = !Renderer.ToggleDowner;
                Debug.InfoLog($"Toggled downer to {Renderer.ToggleDowner}");
            }
            if (Inputs.KeyPressed(VKeyCodes.W))
            {
                Renderer.ToggleUpper = !Renderer.ToggleUpper;
                Debug.InfoLog($"Toggled upper to {Renderer.ToggleUpper}");
            }
            if (Inputs.KeyPressed(VKeyCodes.E))
            {
                Renderer.ToggleHigher = !Renderer.ToggleHigher;
                Debug.InfoLog($"Toggled higher to {Renderer.ToggleHigher}");
            }

            if (Inputs.KeyPressed(VKeyCodes.OEMEquals))
            {
                Renderer.WindowScale *= 2.0f;
                Debug.InfoLog($"Set window scale to {Renderer.WindowScale}");
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Minus))
            {
                Renderer.WindowScale /= 2.0f;
                Debug.InfoLog($"Set window scale to {Renderer.WindowScale}");
            }
        }
    }
}   
