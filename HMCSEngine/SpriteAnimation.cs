namespace HMCSEngine
{
    internal sealed class SpriteAnimation
    {
        private readonly byte[] Frames = new byte[byte.MaxValue];
        private byte AnimationLength = 0;

        private byte CurrentFrame = 0;

        public SpriteAnimation(byte[] frames, byte length)
        {
            Frames = frames;
            AnimationLength = length;
        }

        public void Step()
        {
            CurrentFrame++;

            if(CurrentFrame == AnimationLength)
            {
                CurrentFrame = 0;
            }
        }

        public byte GetSpriteID()
        {
            return Frames[CurrentFrame];
        }
    }
}
