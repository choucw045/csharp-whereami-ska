using System.Text.Json;
using csharp_ska.Models;

namespace csharp_ska.Services;

public class IpService
{
    private readonly HttpClient _client;

    public IpService(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetMyIp()
    {
        var respString = await _client.GetStringAsync("/?format=json");
        var respData = JsonSerializer.Deserialize<IpResponseModel>(respString);
        return respData.IP;
    }
}