using Raylib_cs;
using System.Linq.Expressions;

namespace HMCSEngine
{
    internal static class Files
    {
        public const string ExtentionSeperator = ".";

        public const string ImageFileExtention = ExtentionSeperator + "png";
        public const string TextFileExtention = ExtentionSeperator + "txt";
        public const string SoundFileExtention = ExtentionSeperator + "wav";
        public const string TilemapFileExtention = ExtentionSeperator + "tlm";
        public const string JSONFileExtention = ExtentionSeperator + "json";
        public const string FontFileExtention = ExtentionSeperator + "ttf";

        public static string ProgramDirectory = "";

        public static string EngineDirectory => Path.Combine(ProgramDirectory, "HMCS\\");
        public static string LogsDirectory => Path.Combine(ProgramDirectory, "Logs\\");

        public static string DefaultTexturePath => Path.Combine(EngineDirectory, "defaulttexture" + ImageFileExtention);

        public static string LevelsDirectory => Path.Combine(EngineDirectory, "levels\\");

        public static string LevelDataPath => Path.Combine(LevelsDirectory, "leveldata" + JSONFileExtention);

        public static string CurrentLevelDirectory => Path.Combine(LevelsDirectory, HMCS.LevelID.ToString());

        public static string GetLevelAtlasFilePath(byte index)
        {
            return Path.Combine(CurrentLevelDirectory, "atlas" + index.ToString() + ImageFileExtention);
        }
        public static string CurrentLevelBackgroundFilePath => Path.Combine(CurrentLevelDirectory, "background" + ImageFileExtention);
        public static string CurrentLevelTilemapFilePath => Path.Combine(CurrentLevelDirectory, "tilemap" + TilemapFileExtention);

        public static string ResourcesDirectory => Path.Combine(EngineDirectory, "resources\\");

        public static string ResourcesSoundsDirectory => Path.Combine(ResourcesDirectory, "sounds\\");
        public static string GetResourcesSoundFilePath(string name)
        {
            return Path.Combine(ResourcesSoundsDirectory, name + SoundFileExtention);
        }

        public static string ResourcesFontsDirectory => Path.Combine(ResourcesDirectory, "fonts\\");
        public static string GetResourcesFontFilePath(string name)
        {
            return Path.Combine(ResourcesFontsDirectory, name + FontFileExtention);
        }

        public static string ResourcesImagesDirectory => Path.Combine(ResourcesDirectory, "images\\");
        public static string GetResourcesImageFilePath(string name)
        {
            return Path.Combine(ResourcesImagesDirectory, name + ImageFileExtention);
        }

        public static string PlayerDirectory => Path.Combine(EngineDirectory, "player\\");
        public const string PlayerAtlasFileName = "spriteatlas" + ImageFileExtention;
        public static string PlayerAtlasFilePath => Path.Combine(PlayerDirectory, PlayerAtlasFileName);

        public static string EntitiesDirectory => Path.Combine(EngineDirectory, "entities\\");
        public const string EntityDataFileName = "entitydata" + JSONFileExtention;
        public static string EntityDataDirectory => Path.Combine(EntitiesDirectory, EntityDataFileName);

        public const string EntitiesSpriteAtlasFileName = "spriteatlas" + ImageFileExtention;
        public static string GetResourcesEntityDirectory(byte id)
        {
            return Path.Combine(EntitiesDirectory, id.ToString());
        }
        public static string GetResourcesEntitiesSpriteAtlasFilePath(byte id)
        {
            return Path.Combine(GetResourcesEntityDirectory(id), EntitiesSpriteAtlasFileName);
        }

        public static string HeaderFilePath => Path.Combine(EngineDirectory, "header.txt");

        static Files()
        {
            try
            {
                ProgramDirectory = Raylib.GetWorkingDirectoryAsString();
                CheckEngineDirectoriesExist();
            }
            catch(Exception e)
            {
                Debug.FatalLog(e.Message);

                Debug.InfoLog("Press any return to exit");
                Debug.GetConsoleInput();

                throw;
            }
        }

        /// <summary>
        /// Checks if all of the engine directories exist
        /// </summary>
        /// <exception cref="DirectoryNotFoundException">Thrown when a nessesary directory is not found</exception>
        /// <exception cref="FileNotFoundException">Thrown when a nessesary file was not found</exception>
        public static void CheckEngineDirectoriesExist()
        {
            //Directories

            if (Directory.Exists(EngineDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find project directory \"HMCS\\\"");
            }

            if (File.Exists(HeaderFilePath) == false)
            {
                throw new FileNotFoundException("Could not find header file \"HMCS\\header.txt\"");
            }

            if (Directory.Exists(LevelsDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find levels directory \"HMCS\\levels\\\"");
            }

            //Resources

            if (Directory.Exists(ResourcesDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find resources directory \"HMCS\\resources\\\"");
            }

            if (Directory.Exists(ResourcesSoundsDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find sounds directory \"HMCS\\resources\\sounds\\\"");
            }

            if (Directory.Exists(ResourcesFontsDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find fonts directory \"HMCS\\resources\\fonts\\\"");
            }

            if (Directory.Exists(ResourcesImagesDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find images directory \"HMCS\\resources\\images\\\"");
            }

            //Player

            if (Directory.Exists(PlayerDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find sounds directory \"HMCS\\resources\\player\\\"");
            }

            if (Directory.Exists(EntitiesDirectory) == false)
            {
                throw new DirectoryNotFoundException("Could not find sounds directory \"HMCS\\resources\\entities\\\"");
            }

            //Files

            //Player

            if (File.Exists(PlayerAtlasFilePath) == false)
            {
                throw new FileNotFoundException($"Could not find image file \"HMCS\\player\\spriteatlas{ImageFileExtention}\"");
            }
        }

        public static byte[] ReadFileAllBytes(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            byte[] buffer = new byte[stream.Length];

            stream.ReadExactly(buffer);

            stream.Close();

            return buffer;
        }


        /// <summary>
        /// When given a valid file path, it will load the texture as a raylib Texture2D
        /// </summary>
        /// <param name="path">a path to a png image file</param>
        /// <returns>A raylib Texture2D loaded from the file at the path</returns>
        /// <exception cref="FileNotFoundException">Thrown when the file is not found</exception>
        /// <exception cref="FileLoadException">Thrown when the file doesnt have the png extention</exception>
        public static Texture2D LoadTexture(string path)
        {
            if(File.Exists(path) == false)
            {
                throw new FileNotFoundException($"Texture file {Path.GetFileName(path)} does not exist");
            }

            if (Path.GetExtension(path) != ImageFileExtention)
            {
                throw new FileLoadException($"File {Path.GetFileName(path)} had the wrong extention (should be \".png\" for images)");
            }

            return Raylib.LoadTexture(path);
        }
    }
}
