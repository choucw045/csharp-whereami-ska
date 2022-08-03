using System.Text.Json;
using csharp_ska.Models;

namespace csharp_ska.Clients;

public class IpClient
{
    private readonly HttpClient _client = new HttpClient();

    public async Task<string> GetMyIp()
    {
        var respString = await _client.GetStringAsync("https://api.ipify.org?format=json");
        var respData = JsonSerializer.Deserialize<IpResponseModel>(respString);
        return respData.IP;
    }
}