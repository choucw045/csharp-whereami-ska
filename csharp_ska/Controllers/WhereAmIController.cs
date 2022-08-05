using csharp_ska.Models;
using csharp_ska.Services;
using Microsoft.AspNetCore.Mvc;

namespace csharp_ska.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WhereAmIController : ControllerBase
{
    private readonly ILogger<WhereAmIController> _logger;
    private readonly IpService _ipService;
    private readonly CountryCodeService _countryCodeService;

    public WhereAmIController(ILogger<WhereAmIController> logger, IpService ipService,
        CountryCodeService countryCodeService)
    {
        _logger = logger;
        _ipService = ipService;
        _countryCodeService = countryCodeService;
    }

    [HttpGet]
    public async Task<WhereAmIModel> Get()
    {
        var ip = await _ipService.GetMyIp();
        var countryCode = await _countryCodeService.GetCountryCodeByIp(ip);

        return new WhereAmIModel()
        {
            IP = ip,
            CountryCode = countryCode,
        };
    }
}