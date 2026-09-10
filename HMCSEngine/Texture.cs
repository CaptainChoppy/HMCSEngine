using Raylib_cs;

namespace HMCSEngine
{
    internal sealed class Texture : IDisposable
    {
        private readonly Texture2D SourceTexture;

        public Texture(string sourcepath)
        {
            SourceTexture = Raylib.LoadTexture(sourcepath);
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
