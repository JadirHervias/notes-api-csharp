using API.App.Authorization.Basic;
using API.App.Requests;
using API.Domain.Note;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
	private readonly INoteService _service;

	public NoteController(INoteService service)
    {
        _service = service;
    }

    [BasicAuth]
    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        IEnumerable<NoteEntity> notes = await _service.List();

        string json = JsonConvert.SerializeObject(notes, Formatting.Indented);

        return Task.Run(() => Ok(json)).Result;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            NoteEntity? note = await _service.Get(id);

            return Task.Run(() => Ok(note)).Result;
        }
        catch (NoteNotExistsException ex)
        {
            return Task.Run(() => NotFound(ex.Message)).Result;
        }
        catch (Exception ex)
        {
            return Task.Run(() => Conflict(ex.Message)).Result;
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Store([FromBody] CreateNoteRequest request)
    {
        try
        {
            NoteEntity note = await _service.Create(
                request.Title,
                request.Content,
                request.Priority,
                request.UserId
            );

            return Task.Run(() => Created("", note)).Result;
        }
        catch (NoteAlreadyExistsException ex)
        {
            return Task.Run(() => Conflict(ex.Message)).Result;
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateNoteRequest request)
    {
        try
        {
            NoteEntity note = await _service.Update(
                id,
                request.Title,
                request.Content,
                request.Priority
            );

            return Task.Run(() => Ok(note)).Result;
        }
        catch (NoteNotExistsException ex)
        {
            return Task.Run(() => NotFound(ex.Message)).Result;
        }
    }
}

