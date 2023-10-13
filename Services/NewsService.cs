using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using todoApi.Models;

public class NewsService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NewsService> _logger;
    private readonly string _key;

    public NewsService(HttpClient httpClient, ILogger<NewsService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "newsAppApi");
        _key = configuration["NewsAPi:Key"];
    }

    public async Task<NewsApiResponse> GetNewsAsync()
    {
        try
        {
            var url = $"https://newsapi.org/v2/everything?q=apple&from=2023-10-06&to=2023-10-06&sortBy=popularity&apiKey={_key}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var newsResponse = JsonSerializer.Deserialize<NewsApiResponse>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return newsResponse;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error fetching news: {response.StatusCode}. Response content: {errorContent}");
                return null;
            }
        }
        catch(Exception err)
        {
            _logger.LogError($"Exception occurred: {err}");
            return null;
        }
    }
}
