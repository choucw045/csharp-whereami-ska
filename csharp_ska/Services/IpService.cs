using System.Text.Json;
using csharp_ska.Models;

namespace csharp_ska.Services;

public class IpService
{
    private readonly ILogger<IpService> _logger;
    private readonly HttpClient _client;

    public IpService(IHttpClientFactory httpClientFactory, ILogger<IpService> logger)
    {
        _logger = logger;
        _client = httpClientFactory.CreateClient("IpClient");
    }

    public async Task<string> GetMyIp()
    {
        _logger.LogDebug("in GetMyIp");
        var respString = await _client.GetStringAsync("/?format=json");
        try
        {
            var respData = JsonSerializer.Deserialize<IpResponseModel>(respString);
            return respData.IP;
        }
        catch (JsonException e)
        {
            _logger.LogError($"{nameof(IpService)} parse response error, unexpected response: {respString}");
            throw new Exception($"{nameof(IpService)} parse response error, unexpected response: {respString}", e);
        }
    }
}