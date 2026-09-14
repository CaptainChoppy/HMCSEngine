namespace HMCSEngine
{
    internal static class HMCSLevelData
    {
        public const int MaxUniqueLevels = 256;

        private static LevelData[] Levels = new LevelData[MaxUniqueLevels];

        public static void LoadLevelData()
        {
            JSONLevelDataArrayObject? leveldata;

            try
            {
                leveldata = JSONReader.Read<JSONLevelDataArrayObject>(Files.LevelDataPath);

                if (leveldata == null)
                {
                    Debug.FatalLog($"JSONReader returned null when reading {Files.LevelDataPath}");
                    throw new NullReferenceException();
                }
            }
            catch (Exception e)
            {
                Debug.FatalLog($"Failed to load the leveldata file at \"\\levels\\leveldata.json\" Exception : {e}");
                throw;
            }

            Levels = JSONLevelDataArrayObject.CreateLevelDataFromJSON(leveldata);
        }

        public static LevelData GetEntityData(LevelDataID id)
        {
            return Levels[id.ID];
        }
    }

    internal struct LevelDataID
    {
        public readonly byte ID;

        public LevelDataID(byte id)
        {
            ID = id;
        }
    }

    internal class LevelData
    {
        public readonly string Name;
        public readonly int TileAnimationFrameCount;
        public readonly int TileAnimationSpeed;

        public LevelData(string name, int tileanimationframecount, int tileanimationspeed)
        {
            Name = name;
            TileAnimationFrameCount = tileanimationframecount;
            TileAnimationSpeed = tileanimationspeed;
        }
    }

    internal sealed class JSONLevelDataArrayObject
    {
        public readonly JSONLevelDataObject[] Levels;

        public JSONLevelDataArrayObject(JSONLevelDataObject[] levels)
        {
            Levels = levels;
        }

        public static LevelData[] CreateLevelDataFromJSON(JSONLevelDataArrayObject json)
        {
            LevelData[] leveldata = new LevelData[HMCSEntityData.MaxUniqueEntities];

            List<int> loadedids = new List<int>();

            for (int i = 0; i < json.Levels.Length; i++)
            {
                JSONLevelDataObject level = json.Levels[i];

                if (loadedids.Contains(level.ID) == true)
                {
                    Debug.WarningLog($"Level ID {level.ID} appears more than once in \"\\levels\\leveldata.json\" which is disallowed.");
                    continue;
                }

                loadedids.Add(level.ID);

                leveldata[level.ID] = new LevelData(level.Name, level.TileAnimationFrameCount, level.TileAnimationSpeed);
            }

            return leveldata;
        }
    }

    internal sealed class JSONLevelDataObject
    {
        public readonly int ID;
        public readonly string Name;
        public readonly int TileAnimationFrameCount;
        public readonly int TileAnimationSpeed;

        public JSONLevelDataObject(int id, string name, int tileanimationframecount, int tileanimationspeed)
        {
            ID = id;
            Name = name;
            TileAnimationFrameCount = tileanimationframecount;
            TileAnimationSpeed = tileanimationspeed;
        }
    }
}
