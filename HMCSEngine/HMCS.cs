using Raylib_cs;

namespace HMCSEngine
{
    internal static class HMCS
    {
        public const string EngineVersion = "0.0.0";

        public const string DateTimeFormat = "yyyyMMddHHmmss";

        public static string WindowTitle { get; private set; } = "HMCS Engine " + EngineVersion;

        public static int LevelIndex => Level.LevelID;

        public static Player Player = null;

        public static Random RandomNumberGenerator = new Random(99);

        public static bool Pause = false;

        public static void SetWindowTitle(string title, bool includeversion)
        {
            WindowTitle = "";
            WindowTitle += title;

            if(includeversion == false)
            {
                return;
            }

            WindowTitle += " V" + EngineVersion;
        }

        public static void Initalize()
        {
            Raylib.SetTraceLogLevel(TraceLogLevel.Warning);

            try
            {
                Files.ProgramDirectory = Raylib.GetWorkingDirectoryAsString();

                Files.CheckProjectDirectoriesExist();

                Renderer.CreateWindow();
                EffectsLayers.Initalize();
                Audio.Initialize();

                Tilemap.Initialize();
                HMCSEntityData.LoadEntityData();

                Raylib.SetExitKey(KeyboardKey.Escape);
                Player = new Player(TilePosition.Zero);
            }
            catch(Exception e)
            {
                Debug.FatalLog(e);
                Debug.CreateLogFile();
                throw;
            }
        }

        public static void Update()
        {
            Inputs.Update();

            if (Inputs.KeyPressed(VKeyCodes.P))
            {
                Pause = !Pause;

                if(Pause == true)
                {
                    Debug.InfoLog("Paused");
                }
                else
                {
                    Debug.InfoLog("Unpaused");
                }
            }

            if(Pause == true)
            {
                return;
            }

            Player.Update();
            Level.Update();
            Audio.Update();
            EffectsLayers.Update();
            //GUI.Update();
            Renderer.Draw();

            Time.GlobalFrameTime++;
        }

        public static void Quit()
        {
            Debug.CreateLogFile();
            Renderer.CloseWindow();
        }

        public static void LoadLevel(byte id)
        {
            Player.SetPosition(TilePosition.Zero);
            Level.LoadLevel(id);

            Debug.InfoLog($"Loaded level {id}");
        }
    }
}
