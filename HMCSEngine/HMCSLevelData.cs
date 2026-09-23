namespace HMCSEngine
{
    public static class HMCSLevelData
    {
        public const int MaxUniqueLevels = 256;

        private static LevelData[] Levels = new LevelData[MaxUniqueLevels];

        static HMCSLevelData()
        {
            JSONLevelDataArrayObject? leveldata;

            try
            {
                string path = Files.LevelDataPath;

                leveldata = JSONReader.Read<JSONLevelDataArrayObject>(path);

                if (leveldata == null)
                {
                    Debug.FatalLog($"JSONReader returned null when reading {path}");
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

    public struct LevelDataID
    {
        public readonly byte ID;

        public LevelDataID(byte id)
        {
            ID = id;
        }
    }

    public class LevelData
    {
        public readonly string Name;
        public readonly int TileAnimationFrameCount;
        public readonly int TileAnimationSpeed;
        public readonly float ParalaxMagnitude;
        public readonly bool LoopOver;

        public LevelData(string name, int tileanimationframecount, int tileanimationspeed, float paralaxmagnitude, bool loopover)
        {
            Name = name;
            TileAnimationFrameCount = tileanimationframecount;
            TileAnimationSpeed = tileanimationspeed;
            ParalaxMagnitude = paralaxmagnitude;
            LoopOver = loopover;
        }
    }

    public sealed class JSONLevelDataArrayObject
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

                leveldata[level.ID] = new LevelData(level.Name, level.TileAnimationFrameCount, level.TileAnimationSpeed, level.ParalaxMagnitude, level.LoopOver);
            }

            return leveldata;
        }
    }

    public sealed class JSONLevelDataObject
    {
        public readonly int ID;
        public readonly string Name;
        public readonly int TileAnimationFrameCount;
        public readonly int TileAnimationSpeed;
        public readonly float ParalaxMagnitude;
        public readonly bool LoopOver;

        public JSONLevelDataObject(int id, string name, int tileanimationframecount, int tileanimationspeed, float paralaxmagnitude, bool loopover)
        {
            ID = id;
            Name = name;
            TileAnimationFrameCount = tileanimationframecount;
            TileAnimationSpeed = tileanimationspeed;
            ParalaxMagnitude = paralaxmagnitude;
            LoopOver = loopover;
        }
    }
}
