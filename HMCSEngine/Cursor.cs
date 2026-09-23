using Raylib_cs;
using System.Numerics;

namespace HMCSEngine
{
    internal static class Cursor
    {
        public static Vector2 TruePosition
        {
            get
            {
                return Raylib.GetMousePosition();
            }
        }

        public static ScreenPosition Position
        {
            get
            {
                return (ScreenPosition)(TruePosition / Screen.WindowScale);
            }
        }

        public static Vector2 MousePositionPercentage
        {
            get
            {
                return (Vector2)(Position) / Screen.ReferenceScreenDimentions;
            }
        }
    }
}
