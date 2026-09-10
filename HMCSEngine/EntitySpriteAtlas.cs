using Raylib_cs;
using System.Numerics;

namespace HMCSEngine
{
    internal sealed class EntitySpriteAtlas
    {
        public const int AtlasWidth = 256;
        public const int AtlasHeight = 256;

        public const int SpritePixelWidth = 16;
        
        private byte CurrentSpriteID
        {
            get
            {
                if(Animation == null)
                {
                    return 0;
                }

                return Animation.GetSpriteID();
            }
        }

        private Entity Parent;

        public SpriteAnimation? Animation;

        public Texture? Atlas => Parent.Properties.Atlas;

        public EntitySpriteAtlas(Entity parent)
        {
            Parent = parent;
        }

        public void Draw()
        {
            DrawCurrentSprite();
        }

        public void Step()
        {
            if(Animation == null)
            {
                return;
            }

            Animation.Step();
        }

        private void DrawCurrentSprite()
        {
            if(Atlas == null)
            {
                Debug.WarningLog($"Could not draw sprite for entity (DataID: {Parent.DataID}) (EntityID: {Parent.ID})");
                return;
            }

            Vector2 tilesize;
            Vector2 atlascoordinates = IDToAtlasCoordinates(CurrentSpriteID, Parent.Properties.RenderMode);
            WorldPosition position = Parent.Position;

            switch (Parent.Properties.RenderMode)
            {
                case SpriteRenderMode.T1x1:
                    tilesize = new Vector2(1, 1) * SpritePixelWidth;
                    break;
                case SpriteRenderMode.T1x2:
                    tilesize = new Vector2(1, 2) * SpritePixelWidth;
                    atlascoordinates -= new Vector2(0, 1) * SpritePixelWidth;
                    break;
                case SpriteRenderMode.T2x1:
                    tilesize = new Vector2(2, 1) * SpritePixelWidth;
                    break;
                case SpriteRenderMode.T2x2:
                    tilesize = new Vector2(2, 2) * SpritePixelWidth;
                    atlascoordinates -= new Vector2(0, 1) * SpritePixelWidth;
                    break;
                default:
                    tilesize = new Vector2(1, 1) * SpritePixelWidth;
                    break;
            }

            Rectangle rect = new Rectangle(atlascoordinates, tilesize);

            Renderer.DrawTexture(Atlas.GetTexture(), rect, position);
        }

        public static Vector2 IDToAtlasCoordinates(byte id, SpriteRenderMode rendermode)
        {
            switch (rendermode)
            {
                case SpriteRenderMode.T1x1:
                    return new Vector2(id % 16, id / 16) * new Vector2(1, 1) * SpritePixelWidth;
                case SpriteRenderMode.T2x1:
                    return new Vector2(id % 16, id / 16) * new Vector2(2, 1) * SpritePixelWidth;
                case SpriteRenderMode.T1x2:
                    return new Vector2(id % 16, id / 16) * new Vector2(1, 2) * SpritePixelWidth;
                case SpriteRenderMode.T2x2:
                    return new Vector2(id % 16, id / 16) * new Vector2(2, 2) * SpritePixelWidth;
                default:
                    return Vector2.Zero;
            }
        }
    }

    internal enum SpriteRenderMode : byte
    {
        T1x1 = 0,
        T2x1,
        T1x2,
        T2x2,
    }
}
