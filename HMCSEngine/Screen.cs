using Raylib_cs; 

namespace HMCSEngine
{
    internal static class Screen
    {
        private static float windowscale = 1.0f;
        public static float WindowScale
        {
            get
            {
                return windowscale;
            }
            set
            {
                windowscale = value;
                Raylib.SetWindowSize(Width, Height);
            }
        }

        public const int ReferenceWidth = 320;
        public const int ReferenceHeight = 240;

        public static int Width => (int)(ReferenceWidth * WindowScale);
        public static int Height => (int)(ReferenceHeight * WindowScale);

        public static ScreenPosition ScreenDimentions 
        { 
            get
            {
                return new ScreenPosition(Width, Height);
            } 
        }
    }
}
