using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApi1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class MyApiController : ControllerBase
{
    private readonly ILogger<MyApiController> _logger;

    public MyApiController(ILogger<MyApiController> logger){
        _logger = logger;
    }

    [ApiVersion("1.0")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("log from Get");
        return Ok("SimpleApi1 - GET");
    }
    
    [HttpGet, ApiVersion("2.0")]
    public async Task<IActionResult> GetV2()
    {
        _logger.LogInformation("log from GetV2");
        return Ok("SimpleApi1 V2 - GET");
    }
}