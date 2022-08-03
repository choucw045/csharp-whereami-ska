using System.Text.Json.Serialization;

namespace csharp_ska.Models;

public class CountryCodeResponseModel
{
    [JsonPropertyName("countryCode")] 
    public string CountryCode { get; set; }
}