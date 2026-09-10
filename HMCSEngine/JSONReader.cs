using System.Text;
using System.Text.Json;

namespace HMCSEngine
{
    internal static class JSONReader
    {
        public static T? Read<T>(string filepath)
        {
            try
            {
                byte[] buffer = Files.ReadFileAllBytes(filepath);

                string text = Encoding.ASCII.GetString(buffer);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = true;

                return JsonSerializer.Deserialize<T>(text, options);
            }
            catch (Exception)
            {
                Debug.FatalLog($"Failed to read json file \"{filepath}\"");
                throw;
            }
        }
    }
}