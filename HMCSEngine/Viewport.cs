using System.Numerics;

namespace HMCSEngine
{
    internal static class Viewport
    {
        public static Vector2 Offset = Vector2.Zero;

        public static WorldPosition Position
        {
            get
            {
                WorldPosition position = Offset;

                if(Parent == null)
                {
                    return position;
                }

                position += Parent.Position;

                if(LevelHeader.BoundedCamera == false)
                {
                    return position;
                }

                position = CameraBounds.BoundPosition(position, LevelHeader.CameraBounds);

                return position;
            }
        }

        public static Entity? Parent = null;
    }

    internal struct CameraBounds
    {
        public readonly WorldPosition UpperBound;
        public readonly WorldPosition LowerBound;

        public CameraBounds(Vector2 lowerbound, Vector2 upperbound)
        {
            if(LowerBound.x > UpperBound.x || LowerBound.y > UpperBound.y)
            {
                throw new ArgumentException("Camera's lower bound was higher than upper bound");
            }

            LowerBound = lowerbound;
            UpperBound = upperbound;
        }

        public static WorldPosition BoundPosition(WorldPosition position, CameraBounds bounds)
        {
            WorldPosition newposition = position;

            if (position.x < bounds.LowerBound.x)
            {
                newposition.x = bounds.LowerBound.x;
            }
            else if (position.x > bounds.UpperBound.x)
            {
                newposition.x = bounds.UpperBound.x;
            }

            if (position.y < bounds.LowerBound.y)
            {
                newposition.y = bounds.LowerBound.y;
            }
            else if (position.y > bounds.UpperBound.y)
            {
                newposition.y = bounds.UpperBound.y;
            }

            return newposition;
        }
    }
}
