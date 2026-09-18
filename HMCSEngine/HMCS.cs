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

        public static bool Running = false;

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
            Running = true;

            Raylib.SetTraceLogLevel(TraceLogLevel.Warning);

            try
            {
                Files.ProgramDirectory = Raylib.GetWorkingDirectoryAsString();

                Files.CheckProjectDirectoriesExist();

                Renderer.CreateWindow();
                EffectsLayers.Initalize();
                //Audio.Initialize();

                Tilemap.Initialize();
                HMCSLevelData.LoadLevelData();
                HMCSEntityData.LoadEntityData();

                Raylib.SetExitKey(KeyboardKey.Escape);
                Player = new Player(TilePosition.Zero);
            }
            catch(Exception e)
            {
                Debug.FatalLog(e);
                Quit();
                throw;
            }
        }

        public static void Update()
        {
            Inputs.Update();


            if (Inputs.KeyPressed(VKeyCodes.Function1))
            {
                Commands.CommandMode();
            }

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
            //Audio.Update();
            EffectsLayers.Update();
            GUI.Update();
            Renderer.Draw();

            Time.GlobalFrameTime++;
        }

        public static void Quit()
        {
            Running = false;
            Debug.InfoLog("Quitting");
            Debug.CreateLogFile(false);

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
