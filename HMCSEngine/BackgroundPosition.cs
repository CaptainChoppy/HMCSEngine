namespace HMCSEngine
{
    internal struct BackgroundPosition
    {
        public int x;
        public int y;

        public static BackgroundPosition Zero => new BackgroundPosition(0, 0);
        public static BackgroundPosition One => new BackgroundPosition(1, 1);
        public static BackgroundPosition Right => new BackgroundPosition(1, 0);
        public static BackgroundPosition Left => new BackgroundPosition(-1, 0);
        public static BackgroundPosition Up => new BackgroundPosition(0, 1);
        public static BackgroundPosition Down => new BackgroundPosition(0, -1);

        public BackgroundPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static explicit operator BackgroundPosition(WorldPosition position)
        {
            return new BackgroundPosition(Maths.FloorToInt((position.x / (float)(LevelBackground.TextureWidth))), 
                Maths.FloorToInt(position.y / (float)(LevelBackground.TextureHeight)));
        }
        public static explicit operator BackgroundPosition(ScreenPosition position)
        {
            return (BackgroundPosition)((WorldPosition)(position));
        }
        public static explicit operator BackgroundPosition(TilePosition position)
        {
            return (BackgroundPosition)((WorldPosition)(position));
        }

        public override string ToString()
        {
            return $"background({x}, {y})";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (obj.GetType() != typeof(BackgroundPosition))
            {
                return false;
            }

            BackgroundPosition position = (BackgroundPosition)(obj);

            return (x == position.x) && (y == position.y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
