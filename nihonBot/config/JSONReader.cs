using System.Text.Json;
using System.IO; 
using System.Threading.Tasks; 


namespace NihonBot.config
{
    internal class JSONReader
    {
        public string? Token {get; private set;}

        public async Task ReadJSON(){
            try
            {
                string filePath = "config/config.json";

                string jsonContent = await File.ReadAllTextAsync(filePath);

                var jsonDocument = JsonDocument.Parse(jsonContent);

                if (jsonDocument.RootElement.TryGetProperty("token", out var tokenElement))
                {
                    Token = tokenElement.GetString();
                }
                else
                {
                    throw new KeyNotFoundException("El campo 'token' no existe en el JSON.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el JSON: {ex.Message}");
                throw;
            }
        }
    }

}