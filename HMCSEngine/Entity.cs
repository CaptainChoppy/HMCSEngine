using Raylib_cs;

namespace HMCSEngine
{
    internal class Entity
    {
        public readonly Entity? Following;

        public WorldPosition Position { get; protected set; } = WorldPosition.Zero;

        public readonly EntityData Properties;

        protected WorldPosition Direction { get; set; }

        protected Color ColorTint = Color.White;

        protected readonly EntitySpriteAtlas Atlas;
        public readonly EntityDataID? DataID;
        public readonly ushort ID;

        public Entity(TilePosition position, Entity? following, EntityDataID? dataid, ushort id)
        {
            Position = (WorldPosition)(position);

            DataID = dataid;
            ID = id;
            
            if(DataID != null)
            {
                HMCSEntityData.LoadEntityAtlas(DataID);
            }

            Properties = HMCSEntityData.GetEntityData(dataid);

            Atlas = new EntitySpriteAtlas(this);

            Following = following;
        }

        public void Draw()
        {
            Atlas.Draw();

            string idtext;

            if(DataID == null)
            {
                idtext = "null";
            }
            else
            {
                idtext = DataID.ToString();
            }

            Renderer.DrawText(idtext, 0, 8, 1, ColorTint, Position);
        }

        public virtual void Update()
        {
            Atlas.Step();
            MovementOppertunity();
        }

        protected void Move()
        {
            Position += Direction * 8;
        }

        private void MovementOppertunity()
        {
            switch (Properties.Behaviour)
            {
                case EntityBehaviour.None:
                    Direction = WorldPosition.Zero;
                    break;
                case EntityBehaviour.Random:
                    Direction = EntityAIRandom();
                    break;
                case EntityBehaviour.SmartRandom:
                    Direction = EntityAISmartRandom();
                    break;
                case EntityBehaviour.Follow:
                    Direction = EntityAIFollow();
                    break;
                case EntityBehaviour.Avoid:
                    Direction = EntityAIAvoid();
                    break;
                default:
                    throw new NotImplementedException("Entity behaviour has not been implemented or does not exist");
            }
            
            Move();
        }

        private WorldPosition EntityAIRandom()
        {
            int random = HMCS.RandomNumberGenerator.Next(5);

            switch (random)
            {
                case 0:
                    return WorldPosition.Up;
                case 1:
                    return WorldPosition.Down;
                case 2:
                    return WorldPosition.Left;
                case 3:
                    return WorldPosition.Right;
                default:
                    return WorldPosition.Zero;
            }
        }
        private WorldPosition EntityAISmartRandom()
        {
            int random = HMCS.RandomNumberGenerator.Next(3);

            if (random != 0)
            {
                return Direction;
            }

            return EntityAIRandom();
        }
        private WorldPosition EntityAIFollow()
        {
            if (Following == null)
            {
                return WorldPosition.Zero;
            }

            float dx = Position.x - Following.Position.x;
            float dy = Position.y - Following.Position.y;

            if (dy == 0 || dx != 0)
            {
                if (dx < 0)
                {
                    return WorldPosition.Right;
                }

                return WorldPosition.Left;
            }
            if (dx == 0 || dy != 0)
            {
                if (dy < 0)
                {
                    return WorldPosition.Up;
                }
            }

            return WorldPosition.Down;
        }
        private WorldPosition EntityAIAvoid()
        {
            WorldPosition direction = EntityAIFollow();

            return -direction;
        }

        public void SetPosition(TilePosition newposition)
        {
            Position = (WorldPosition)(newposition);
        }
    }

    internal class EntityDataID
    {
        public readonly byte ID;

        public EntityDataID(byte id)
        {
            ID = id;
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }

    internal enum EntityBehaviour
    {
        None = 0,
        Random,
        SmartRandom,
        Follow,
        Avoid
    }
}
