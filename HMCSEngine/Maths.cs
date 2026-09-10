namespace HMCSEngine
{
    public static class Maths
    {
        public static int Mod(int left, int right)
        {
            return (left % right + right) % right;
        }
        public static float Mod(float left, int right)
        {
            return (left % right + right) % right;
        }
        public static float Mod(int left, float right)
        {
            return (left % right + right) % right;
        }
        public static float Mod(float left, float right)
        {
            return (left % right + right) % right;
        }

        public static int FloorToInt(float x)
        {
            return (int)(Math.Floor(x));
        }
    }
}
