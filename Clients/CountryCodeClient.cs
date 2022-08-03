using System.Net.Http.Headers;
using System.Text.Json;
using csharp_ska.Models;

namespace csharp_ska.Clients;

public class CountryCodeClient
{
    private readonly HttpClient _client = new HttpClient();

    public async Task<string> GetCountryCodeByIp(string ip)
    {
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
            "http://ip-api.com/batch",
            JsonContent.Create(
                data,
                MediaTypeHeaderValue.Parse("application/json"),
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
        );
        if (!resp.IsSuccessStatusCode)
            throw new Exception($"{nameof(CountryCodeClient)} request failed.");

        var respString = await resp.Content.ReadAsStringAsync();
        try
        {
            var respData = JsonSerializer.Deserialize<List<CountryCodeResponseModel>>(respString);
            if (!respData.Any())
                throw new Exception($"{nameof(CountryCodeClient)} parse response error, returned empty array.");
            return respData.First().CountryCode;
        }
        catch (JsonException e)
        {
            throw new Exception(
                $"{nameof(CountryCodeClient)} parse response error, unexpected response: {respString}",
                e);
        }
    }
}