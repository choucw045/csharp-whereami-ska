using System.Net.Http.Headers;
using System.Text.Json;
using csharp_ska.Models;

namespace csharp_ska.Services;

public class CountryCodeService
{
    private readonly ILogger<CountryCodeService> _logger;
    private readonly HttpClient _client;

    public CountryCodeService(IHttpClientFactory httpClientFactory, ILogger<CountryCodeService> logger)
    {
        _logger = logger;
        _client = httpClientFactory.CreateClient("CountryCodeClient");
    }

    public async Task<string> GetCountryCodeByIp(string ip)
    {
        _logger.LogDebug("in GetCountryCodeByIp");

        var data = new List<CountryCodeRequestModel>()
        {
            new CountryCodeRequestModel
            {
                Query = ip,
                Fields = "countryCode",
                Lang = "en",
            }
        };
        var resp = await _client.PostAsync(
            "/batch",
            JsonContent.Create(
                data,
                MediaTypeHeaderValue.Parse("application/json"),
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
        );
        if (!resp.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(CountryCodeService)} request failed.");
            throw new Exception($"{nameof(CountryCodeService)} request failed.");
        }

        var respString = await resp.Content.ReadAsStringAsync();
        try
        {
            var respData = JsonSerializer.Deserialize<List<CountryCodeResponseModel>>(respString);
            if (!respData.Any())
            {
                _logger.LogError($"{nameof(CountryCodeService)} parse response error, returned empty array.");
                throw new Exception($"{nameof(CountryCodeService)} parse response error, returned empty array.");
            }

            return respData.First().CountryCode;
        }
        catch (JsonException e)
        {
            _logger.LogError($"{nameof(CountryCodeService)} parse response error, returned empty array.");
            throw new Exception(
                $"{nameof(CountryCodeService)} parse response error, unexpected response: {respString}",
                e);
        }
    }
}