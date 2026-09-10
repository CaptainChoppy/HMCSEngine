using System.Numerics;
using Raylib_cs;

namespace HMCSEngine
{
    internal struct TileLayer
    {
        public const int TileLayerWidth = 256;
        public const int TileLayerHeight = 192;

        private readonly Tile[,] Tiles;
        
        public TileLayer()
        {
            Tiles = new Tile[TileLayerWidth, TileLayerHeight];
        }

        public void SetTile(TilePosition position, Tile newtile)
        {
            TilePosition boundedposition = position.BoundedPosition;

            Tiles[boundedposition.x, boundedposition.y] = newtile;
        }

        public Tile GetTile(TilePosition position)
        {
            TilePosition boundedposition = position.BoundedPosition;

            return Tiles[boundedposition.x, boundedposition.y];
        }

        public void Draw()
        {
            TilePosition camerapositon = (TilePosition)(Viewport.Position);

            for (int x = camerapositon.x - 1; x <= camerapositon.x + Renderer.TileHorizontalViewDistance; x++)
            {
                for (int y = camerapositon.y - 1; y <= camerapositon.y + Renderer.TileVerticalViewDistance; y++)
                {
                    DrawTile(new TilePosition(x, y));
                }
            }
        }

        private void DrawTile(TilePosition positon)
        {
            byte id = GetTile(positon).ID;

            Rectangle texturerect = new Rectangle(Tile.IDToAtlasCoordinates(id), Vector2.One * Tile.TileWidth);

            TileAtlas? atlas = TileAtlases.GetCurrentAtlas();

            if (atlas == null)
            {
                return;
            }

            Renderer.DrawTexture(atlas.Atlas.GetTexture(), texturerect, (WorldPosition)(positon));
        }
    }
}
