namespace HMCSEngine
{
    internal sealed class TileAtlas : IDisposable
    {
        public readonly Texture Atlas;

        /// <summary>
        /// Represents an atlas used to draw tiles
        /// </summary>
        /// <param name="index">The index of the atlas so that multiple atlases can be loaded for tile animations</param>
        /// <exception cref="FileNotFoundException">Thrown when the source file cannot be found</exception>
        /// <exception cref="FileLoadException">Thrown when the source file cannot be loaded as an png image</exception>
        public TileAtlas(byte index)
        {
            Atlas = new Texture(Files.GetLevelAtlasFilePath(index));
        }

        private TileAtlas(Texture texture)
        {
            Atlas = texture;
        }

        public void Dispose()
        {
            Atlas.Dispose();
        }

        public static implicit operator TileAtlas(Texture texture)
        {
            return new TileAtlas(texture);
        }
            
    }
}
