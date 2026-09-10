using System.Numerics;

namespace HMCSEngine
{
    internal struct ScreenPosition
    {
        public int x;
        public int y;

        public static ScreenPosition Zero => new ScreenPosition(0, 0);
        public static ScreenPosition One => new ScreenPosition(1, 1);
        public static ScreenPosition Right => new ScreenPosition(1, 0);
        public static ScreenPosition Left => new ScreenPosition(-1, 0);
        public static ScreenPosition Up => new ScreenPosition(0, 1);
        public static ScreenPosition Down => new ScreenPosition(0, -1);

        public ScreenPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static ScreenPosition operator +(ScreenPosition left, ScreenPosition right)
        {
            return new ScreenPosition(left.x + right.x, left.y + right.y);
        }
        public static ScreenPosition operator -(ScreenPosition left, ScreenPosition right)
        {
            return new ScreenPosition(left.x - right.x, left.y - right.y);
        }
        public static ScreenPosition operator *(ScreenPosition left, ScreenPosition right)
        {
            return new ScreenPosition(left.x * right.x, left.y * right.y);
        }

        public static ScreenPosition operator *(ScreenPosition left, int right)
        {
            return new ScreenPosition(left.x * right, left.y * right);
        }

        public static bool operator ==(ScreenPosition a, ScreenPosition b)
        {
            return (a.x == b.x) && (b.x == b.y);
        }
        public static bool operator !=(ScreenPosition a, ScreenPosition b)
        {
            return (a.x != b.x) && (b.x != b.y);
        }

        public static implicit operator Vector2(ScreenPosition position)
        {
            return new Vector2(position.x, position.y) * Renderer.WindowScale;
        }
        public static implicit operator ScreenPosition(Vector2 position)
        {
            return new Vector2(position.X, position.Y) * Renderer.WindowScale;
        }

        public static explicit operator ScreenPosition(WorldPosition position)
        {
            return new ScreenPosition(position.x - Viewport.Position.x,
             Renderer.ReferenceWindowHeight - (position.y - Viewport.Position.y));
        }
        public static explicit operator ScreenPosition(BackgroundPosition position)
        {
            return (ScreenPosition)((WorldPosition)(position));
        }
        public static explicit operator ScreenPosition(TilePosition position)
        {
            return (ScreenPosition)((WorldPosition)(position));
        }

        public override string ToString()
        {
            return $"screen({x}, {y})";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (obj.GetType() != typeof(ScreenPosition))
            {
                return false;
            }

            ScreenPosition position = (ScreenPosition)(obj);

            return (x == position.x) && (y == position.y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
