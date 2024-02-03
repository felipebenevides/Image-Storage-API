using ImageStorageAPI.Models;

namespace ImageStorageAPI.Services;

public class RandomImage:IRandomImage
{
    private static IHttpClientFactory _httpClientFactory;

    public RandomImage(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
  
   public  async Task<List<Image>> GetRandomImages()
    {
        var httpClient =  _httpClientFactory.CreateClient();
        var listRandom = new List<Image>();

        for (int i = 0; i < 10; i++)
        {
            try
            {
                // Exemplo de URL de uma API que fornece imagens aleatórias
                string imageUrl = "https://source.unsplash.com/random";

                // Faz uma requisição HTTP para obter a imagem
                var response = await httpClient.GetAsync(imageUrl);
                response.EnsureSuccessStatusCode();

                // Lê o conteúdo da resposta como um array de bytes
                var imageData = await response.Content.ReadAsByteArrayAsync();

                // Cria uma nova imagem e salva no banco de dados
                listRandom.Add(
                    new Image()
                    {
                        Name = $"image_{i}.jpg",
                        Data = imageData
                    });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar imagem: {ex.Message}");
            }
        }

        return listRandom;
    }
}