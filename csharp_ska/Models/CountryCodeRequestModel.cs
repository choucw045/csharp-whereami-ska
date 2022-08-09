using System.Text.Json.Serialization;

namespace csharp_ska.Models;

public class CountryCodeRequestModel
{
    [JsonPropertyName("query")]
    public string? Query { get; set; }
    
    [JsonPropertyName("fields")]
    public string? Fields { get; set; }
    
    [JsonPropertyName("lang")]
    public string? Lang { get; set; }
}
