using System.Text.Json.Serialization;

namespace csharp_ska.Models;

public class IpResponseModel
{
    [JsonPropertyName("ip")]
    public string IP { get; set; }
}