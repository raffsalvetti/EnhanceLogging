using Microsoft.AspNetCore.Mvc;

namespace SimpleApi2.Controllers;

[ApiController, Route("api/v{version:apiVersion}/[controller]/[action]")]
public class MyApiController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("SimpleApi2 - GET");
    }
}