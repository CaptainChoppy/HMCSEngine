using Raylib_cs;

namespace HMCSEngine
{
    internal class Cursor
    {
        public static ScreenPosition Position
        {
            get
            {
                return (ScreenPosition)(Raylib.GetMousePosition());
            }
        }
    }
}
