using System.Numerics;

namespace HMCSEngine
{
    internal static class Tilemap
    {
        public const int TileLayerCount = 3;

        public const int SizeOfTilemap = TileLayer.TileLayerWidth * TileLayer.TileLayerHeight * 4;
        public const int SizeOfTilemapLayer = TileLayer.TileLayerWidth * TileLayer.TileLayerHeight;

        private static readonly TileLayer[] TileLayers = new TileLayer[TileLayerCount];

        private static readonly byte[,] Interaction = new byte[TileLayer.TileLayerWidth, TileLayer.TileLayerHeight];

        public static void Initialize()
        {
            for(int i = 0; i <  TileLayerCount; i++)
            {
                TileLayers[i] = new TileLayer();
            }
        }

        private static void SetTile(Tile newtile, TilePosition position, TileLayerName layer)
        {
            int layerindex = (int)(layer);

            if (layerindex >= TileLayerCount)
            {
                Debug.ErrorLog($"Could not set tile to layer {(int)(layer)}");
                throw new IndexOutOfRangeException();
            }

            TileLayers[layerindex].SetTile(position, newtile);
        }

        public static Tile GetTile(TilePosition position, TileLayerName layer)
        {
            int layerindex = (int)(layer);

            if (layerindex >= TileLayerCount)
            {
                Debug.ErrorLog($"Could not get tile from layer {(int)(layer)}");
                throw new IndexOutOfRangeException();
            }

            return TileLayers[layerindex].GetTile(position);
        }

        public static byte GetInteraction(TilePosition position)
        {
            TilePosition boundedposition = position.BoundedPosition;

            return Interaction[boundedposition.x, boundedposition.y];
        }

        public static void LoadTileMap()
        {
            byte[] buffer = new byte[SizeOfTilemap];

            try
            {
                buffer = Files.ReadFileAllBytes(Files.CurrentLevelTilemapFilePath);
            }
            catch(Exception)
            {
                Debug.FatalLog($"Failed to load tilemap.");
                throw;
            }

            for(int i = 0; i < buffer.Length; i++)
            {
                int relativeindex = i % SizeOfTilemapLayer;

                TilePosition positon = new TilePosition(relativeindex % TileLayer.TileLayerWidth, relativeindex / TileLayer.TileLayerWidth);

                int layer = i / SizeOfTilemapLayer;

                if (layer != 3)
                {
                    SetTile(new Tile(buffer[i]), positon, (TileLayerName)(layer));
                }
                else
                {
                    Interaction[positon.x, positon.y] = buffer[i];
                }
            }
        }

        public static void DrawLayer(TileLayerName layer)
        {
            int layerindex = (int)(layer);

            TileLayers[layerindex].Draw();
        }
    }

    internal struct Tile
    {
        public const int TileWidth = 16;

        public byte ID;

        public Tile()
        {
            ID = 0;
        }

        public Tile(byte id)
        {
            ID = id;
        }

        public static Vector2 IDToAtlasCoordinates(byte id)
        {
            return new Vector2(id % 16, (int)(id / 16.0f)) * 16;
        }
    }

    internal enum TileLayerName
    {
        Downer = 0,
        Upper,
        Higher
    }
}
