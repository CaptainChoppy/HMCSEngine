using System.Numerics;

namespace HMCSEngine
{
    internal struct WorldPosition
    {
        public int x;
        public int y;

        public static WorldPosition Zero => new WorldPosition(0, 0);
        public static WorldPosition One => new WorldPosition(1, 1);
        public static WorldPosition Right => new WorldPosition(1, 0);
        public static WorldPosition Left => new WorldPosition(-1, 0);
        public static WorldPosition Up => new WorldPosition(0, 1);
        public static WorldPosition Down => new WorldPosition(0, -1);

        public WorldPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static WorldPosition operator +(WorldPosition left, WorldPosition right)
        {
            return new WorldPosition(left.x + right.x, left.y + right.y);
        }
        public static WorldPosition operator -(WorldPosition left, WorldPosition right)
        {
            return new WorldPosition(left.x - right.x, left.y - right.y);
        }
        public static WorldPosition operator *(WorldPosition left, WorldPosition right)
        {
            return new WorldPosition(left.x * right.x, left.y * right.y);
        }

        public static WorldPosition operator *(WorldPosition left, int right)
        {
            return new WorldPosition(left.x * right, left.y * right);
        }
        public static WorldPosition operator -(WorldPosition position)
        {
            return new WorldPosition(-position.x, -position.y);
        }

        public static implicit operator WorldPosition(Vector2 position)
        {
            return new WorldPosition(Maths.FloorToInt(position.X), Maths.FloorToInt(position.Y));
        }

        public static explicit operator WorldPosition(ScreenPosition position)
        {
            return new WorldPosition(Maths.FloorToInt(position.x / Renderer.WindowScale) + Viewport.Position.x,
                Maths.FloorToInt((position.y - Renderer.WindowHeight) / -Renderer.WindowScale) + Viewport.Position.y);
        }
        public static explicit operator WorldPosition(TilePosition position)
        {
            return new WorldPosition(position.x * Tile.TileWidth, position.y * Tile.TileWidth);
        }
        public static explicit operator WorldPosition(BackgroundPosition position)
        {
            return new WorldPosition(position.x * LevelBackground.TextureWidth, position.y * LevelBackground.TextureHeight);
        }

        public override string ToString()
        {
            return $"world({x}, {y})";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (obj.GetType() != typeof(WorldPosition))
            {
                return false;
            }

            WorldPosition position = (WorldPosition)(obj);

            return (x == position.x) && (y == position.y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
