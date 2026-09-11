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

        public const int ReferenceWindowWidth = 320;
        public const int ReferenceWindowHeight = 240;

        public static int Width => (int)(ReferenceWindowWidth * WindowScale);
        public static int Height => (int)(ReferenceWindowHeight * WindowScale);

        public static ScreenPosition ScreenDimentions 
        { 
            get
            {
                return new ScreenPosition((int)(Width), (int)(Height));
            } 
        }
    }
}
