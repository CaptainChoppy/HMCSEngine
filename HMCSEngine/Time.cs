namespace HMCSEngine
{
    public static class Time
    {
        public static float TimeScale = 1.0f;
        public static float DeltaTime => TimeScale / 45.0f;

        public static uint GlobalFrameTime = 0;
    }
}
