namespace HMCSEngine
{
    internal struct TilePosition
    {
        public const int MinX = 0;
        public const int MaxX = 256 - 1;
        public const int MinY = 0;
        public const int MaxY = 192 - 1;

        public int x;
        public int y;

        public TilePosition BoundedPosition => new TilePosition(Maths.Mod(x, TileLayer.TileLayerWidth), Maths.Mod(y, TileLayer.TileLayerHeight));

        public static TilePosition Zero => new TilePosition(0, 0);
        public static TilePosition One => new TilePosition(1, 1);
        public static TilePosition Right => new TilePosition(1, 0);
        public static TilePosition Left => new TilePosition(-1, 0);
        public static TilePosition Up => new TilePosition(0, 1);
        public static TilePosition Down => new TilePosition(0, -1);

        public TilePosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static TilePosition operator +(TilePosition a, TilePosition b)
        {
            return new TilePosition(a.x + b.x, a.y + b.y);
        }
        public static TilePosition operator -(TilePosition a, TilePosition b)
        {
            return new TilePosition(a.x - b.x, a.y - b.y);
        }
        public static TilePosition operator *(TilePosition a, TilePosition b)
        {
            return new TilePosition(a.x * b.x, a.y * b.y);
        }
        public static TilePosition operator *(TilePosition a, int b)
        {
            return new TilePosition(a.x * b, a.y * b);
        }

        public static bool operator ==(TilePosition a, TilePosition b)
        {
            return (a.x == b.x) && (b.x == b.y);
        }
        public static bool operator !=(TilePosition a, TilePosition b)
        {
            return (a.x != b.x) && (b.x != b.y);
        }

        public static explicit operator TilePosition(WorldPosition position)
        {
            return new TilePosition(Maths.FloorToInt(position.x / Tile.TileWidth), Maths.FloorToInt(position.y / Tile.TileWidth));
        }
        public static explicit operator TilePosition(ScreenPosition position)
        {
            return (TilePosition)((WorldPosition)(position));
        }
        public static explicit operator TilePosition(BackgroundPosition position)
        {
            return (TilePosition)((WorldPosition)(position));
        }

        public override string ToString()
        {
            return $"tile({x}, {y})";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (obj.GetType() != typeof(TilePosition))
            {
                return false;
            }

            TilePosition position = (TilePosition)(obj);

            return (x == position.x) && (y == position.y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
