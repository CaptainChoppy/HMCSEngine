using System.Net.Http.Headers;

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

        public static string ProjectDirectory => Path.Combine(ProgramDirectory, "HMCS\\");
        public static string LevelsDirectory => Path.Combine(ProjectDirectory, "levels\\");

        public static string CurrentLevelDirectory => Path.Combine(LevelsDirectory, HMCS.LevelIndex.ToString());

        public static string GetLevelAtlasFilePath(byte index)
        {
            return Path.Combine(CurrentLevelDirectory, "atlas" + index.ToString() + ImageFileExtention);
        }
        public static string CurrentLevelBackgroundFilePath => Path.Combine(CurrentLevelDirectory, "background" + ImageFileExtention);
        public static string CurrentLevelTilemapFilePath => Path.Combine(CurrentLevelDirectory, "tilemap" + TilemapFileExtention);

        public static string ResourcesDirectory => Path.Combine(ProjectDirectory, "resources\\");

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

        public static string PlayerDirectory => Path.Combine(ProjectDirectory, "player\\");
        public const string PlayerAtlasFileName = "spriteatlas" + ImageFileExtention;
        public static string PlayerAtlasFilePath => Path.Combine(PlayerDirectory, PlayerAtlasFileName);

        public static string EntitiesDirectory => Path.Combine(ProjectDirectory, "entities\\");
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

        public static string HeaderFilePath => Path.Combine(ProjectDirectory, "header.txt");

        public static void CheckProjectDirectoriesExist()
        {
            //Directories

            if (Directory.Exists(ProjectDirectory) == false)
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
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open);

                byte[] buffer = new byte[stream.Length];

                stream.ReadExactly(buffer);

                stream.Close();

                return buffer;
            }
            catch(Exception e)
            {
                Debug.ErrorLog($"Failed to load file \"{path}\". Exception : {e}");
                throw;
            }
        }
    }
}
