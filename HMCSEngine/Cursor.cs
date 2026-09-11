using Raylib_cs;

namespace HMCSEngine
{
    internal static class Cursor
    {
        public static ScreenPosition Position
        {
            get
            {
                return (ScreenPosition)(Raylib.GetMousePosition());
            }
        }

        public static ScreenPosition MousePositionPercentage
        {
            get
            {
                return Position / Screen.ScreenDimentions;
            }
        }
    }
}
