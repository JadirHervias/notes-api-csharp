using API.App.Authorization.Basic;
using API.Domain.User;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [BasicAuth]
    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        IEnumerable<UserEntity> notes = await _service.List();

        string json = JsonConvert.SerializeObject(notes, Formatting.Indented);

        return Task.Run(() => Ok(json)).Result;
    }
}

