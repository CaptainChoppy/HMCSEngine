using Raylib_cs;

namespace HMCSEngine
{
    public static class Audio
    {
        public const int AudioTrackCount = 16;

        private static AudioTrack[] Tracks = new AudioTrack[AudioTrackCount];

        public static void Initialize()
        {
            Raylib.InitAudioDevice();
        }

        public static void Update()
        {
            for(int i = 0; i < Tracks.Length; i++)
            {
                Tracks[i].Update();
            }
        }

        public static void Load(SoundInfo soundinfo)
        {
            if(string.IsNullOrEmpty(soundinfo.Name))
            {
                return;
            }

            Tracks[soundinfo.Track].Load(soundinfo);
        }

        public static void Play(int track)
        {
            Tracks[track].Play();
        }

        public static void LoadAndPlay(SoundInfo soundinfo)
        {
            Load(soundinfo);
            Play(soundinfo.Track);
        }

        public static void Pause(int track)
        {
            Tracks[track].Pause();
        }

        public static void Stop(int track)
        {
            Tracks[track].Stop();
        }
    }

    internal struct AudioTrack
    {
        public SoundInfo SoundInfo
        {
            get;
            private set;
        }

        private Music Sound;

        public float Volume
        {
            get;
            set
            {
                
            }
        }

        public AudioTrack()
        {

        }

        public bool Playing => Raylib.IsMusicStreamPlaying(Sound);

        public void Load(SoundInfo soundinfo)
        {
            if (soundinfo.Priority == false && Playing == true)
            {
                return;
            }

            Raylib.UnloadMusicStream(Sound);

            SoundInfo = soundinfo;

            Sound = Raylib.LoadMusicStream(Files.GetResourcesSoundFilePath(soundinfo.Name));
            Sound.Looping = SoundInfo.Looping;
        }

        public void Update()
        {
            Raylib.UpdateMusicStream(Sound);
        }

        public void Play()
        {
            Raylib.PlayMusicStream(Sound);
        }

        public void Pause()
        {
            Raylib.PauseMusicStream(Sound);
        }

        public void Stop()
        {
            Raylib.StopMusicStream(Sound);
        }
    }

    public struct SoundInfo
    {
        public readonly string Name = "";

        public readonly int Track = 0;

        public readonly bool Priority = false;
        public readonly bool Looping = false;

        public SoundInfo(string name, int track, bool priority, bool looping)
        {
            Name = name;
            Track = track;
            Priority = priority;
            Looping = looping;
        }
    }
}
