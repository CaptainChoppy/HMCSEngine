using Raylib_cs;

namespace HMCSEngine
{
    internal sealed class Texture : IDisposable
    {
        private readonly Texture2D SourceTexture;

        /// <summary>
        /// Creates a new texture using the data from the image source file at sourcepath
        /// </summary>
        /// <param name="sourcepath">The path to the png image source file</param>
        /// <exception cref="FileNotFoundException">Thrown when the source file cannot be found</exception>
        /// <exception cref="FileLoadException">Thrown when the source file cannot be loaded as an png image</exception>
        public Texture(string sourcepath)
        {
            try
            {
                SourceTexture = Files.LoadTexture(sourcepath);
            }
            catch (FileNotFoundException)
            {
                Debug.ErrorLog($"Could not load texture \"{sourcepath}\" because it doesnt exist");
                throw;
            }
            catch (FileLoadException)
            {
                Debug.ErrorLog($"Could not load texture \"{sourcepath}\" because it was not a png");
                throw;
            }
        }

        public Texture2D GetTexture()
        {
            return SourceTexture;
        }

        public void Dispose()
        {
            Raylib.UnloadTexture(SourceTexture);

            GC.SuppressFinalize(this);
        }
    }
}
