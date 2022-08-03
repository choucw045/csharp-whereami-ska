using csharp_ska.Clients;
using csharp_ska.Models;
using Microsoft.AspNetCore.Mvc;

namespace csharp_ska.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WhereAmIController : ControllerBase
{
    private readonly ILogger<WhereAmIController> _logger;
    private readonly IpClient _ipClient;
       private readonly CountryCodeClient _countryCodeClient;

    public WhereAmIController(ILogger<WhereAmIController> logger, IpClient ipClient,
        CountryCodeClient countryCodeClient)
    {
        _logger = logger;
        _ipClient = ipClient;
        _countryCodeClient = countryCodeClient;
    }

    [HttpGet]
    public async Task<WhereAmIModel> Get()
    {
        var ip = await _ipClient.GetMyIp();
        var countryCode = await _countryCodeClient.GetCountryCodeByIp(ip);

        return new WhereAmIModel()
        {
            IP = ip,
            CountryCode = countryCode,
        };
    }
}