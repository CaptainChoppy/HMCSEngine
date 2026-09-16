using Raylib_cs;

namespace HMCSEngine
{
    internal static class Cursor
    {
        public static ScreenPosition TruePosition
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
                return (TruePosition / Screen.WindowScale);
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
