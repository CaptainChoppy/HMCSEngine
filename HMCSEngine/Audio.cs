using Raylib_cs;

namespace HMCSEngine
{
    public static class Audio
    {
        public const int MaxSoundSlots = 32;
        public static readonly SoundSlot[] SoundSlots = new SoundSlot[MaxSoundSlots];
    }

    public sealed class SoundSlot
    {


        public void LoadSound()
        {

        }
    }

    internal struct AudioTrack
    {
        private readonly Queue<SoundQueueInfo> SoundStack = new Queue<SoundQueueInfo>();
        public bool Paused = false;

        public AudioTrack()
        {

        }

        public void AddSoundToQueue(SoundQueueInfo info)
        {
            SoundStack.Enqueue(info);
        }

        public void AddSoundToFrontOfQueue(SoundQueueInfo info)
        {

        }
    }

    internal struct SoundQueueInfo
    {
        public readonly string Name;
        public readonly uint Time;
        public readonly

        public SoundQueueInfo(string name, uint time)
        {
            Name = name;
            Time = time;
        }
    }

    public class SoundID
    {
        public readonly byte ID;

        public SoundID(byte id)
        {
            ID = id;
        }
    }
}
