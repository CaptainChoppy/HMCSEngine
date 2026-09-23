using Raylib_cs;

namespace HMCSEngine
{
    internal static class HMCS
    {
        /// <summary>The current engine version</summary>
        public const string EngineVersion = "0.0.0";

        /// <summary>The format of dates and times</summary>
        public const string DateTimeFormat = "yyyyMMddHHmmss";

        /// <summary>The current window title. Can be set using HMCS.SetWindowTitle()</summary>
        public static string WindowTitle { get; private set; } = "HMCS Engine " + EngineVersion;

        /// <summary>The current level ID</summary>
        public static int LevelID => Level.LevelID;

        /// <summary>Reference to the Player instance</summary>
        public static Player Player;

        /// <summary>An RNG object that should be used by everything to get random numbers</summary>
        public static Random RandomNumberGenerator = new Random(99);

        /// <summary>A bool that when true will stop all player, entity, audio, GUI, level and time updates. Inputs will still be updated no matter what</summary>
        public static bool Pause = false;

        /// <summary>A bool used to check if the engine is running and if it is false then HMCS.Quit() should be called to clean up and finish</summary>
        public static bool Running { get; private set; } = false;

        /// <summary>The default texture that has a 16x16 grid of 16x16 pixel tiles with numbers</summary>
        public static readonly Texture DefaultTexture;

        static HMCS()
        {
            Running = true;

            Raylib.SetTraceLogLevel(TraceLogLevel.Warning);

            try
            {
                Renderer.CreateWindow();
                //Audio.Initialize();
                DefaultTexture = new Texture(Files.DefaultTexturePath);

                Raylib.SetExitKey(KeyboardKey.Escape);
                Player = new Player(TilePosition.Zero);

                LoadLevel(0);
            }
            catch
            {
                Quit();

                throw;
            }
        }

        /// <summary>
        /// Sets the window title 
        /// </summary>
        /// <param name="title">The title of the window</param>
        /// <param name="includeversion">Choose to include the engine version in the title e.g. "[Title] V1.0.0"</param>
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

        /// <summary>
        /// Should be called in a while loop and after HMCS.Initalize() has been run
        /// </summary>
        public static void Update()
        {
            Running = Running && (Raylib.WindowShouldClose() == false);

            if(Running == false)
            {
                return;
            }

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

        /// <summary>
        /// Call to quit the game
        /// </summary>
        public static void Quit()
        {
            Running = false;
            Debug.InfoLog("Quitting");
            Debug.CreateLogFile(false);

            Renderer.CloseWindow();
        }

        /// <summary>
        /// Will load the player into a level and HMCS.Initalize() must be called before hand
        /// </summary>
        /// <param name="id">The ID of the level to be loaded</param>
        public static void LoadLevel(byte id)
        {
            Player.SetPosition(TilePosition.Zero);
            Level.LoadLevel(id);

            Debug.InfoLog($"Loaded level {id}");
        }
    }
}
