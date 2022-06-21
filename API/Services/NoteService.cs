using API.Domain.Note;

namespace API.Services;

public class NoteService : INoteService
{
    readonly INoteRepository _repository;

    public NoteService(INoteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NoteEntity>> List()
    {
        return await _repository.List();
    }

    public async Task<NoteEntity?> Get(string id)
    {
        NoteEntity? note = await _repository.Get(Guid.Parse(id));

        if (null == note)
        {
            throw new NoteNotExistsException(id);
        }

        return note;
    }

    public async Task<NoteEntity> Create(string title, string? content, int priority, string userId)
    {
        string id = Guid.NewGuid().ToString();

        NoteEntity? note = await _repository.Get(Guid.Parse(id));

        if (null != note)
        {
            throw new NoteAlreadyExistsException(id);
        }

        NoteEntity newNote = NoteEntity.From(new NotePrimitives(
            id,
            title,
            content,
            priority,
            userId
        ));

        await _repository.Save(newNote);

        return newNote;
    }

    public async Task<NoteEntity> Update(string id, string title, string content, int priority)
    {
        NoteEntity? note = await _repository.Get(Guid.Parse(id));

        if (null == note)
        {
            throw new NoteNotExistsException(id);
        }

        note.Title = title;
        note.Content = content;
        note.Priority = Enum.IsDefined(typeof(NotePriority), priority)
                ? Enum.Parse<NotePriority>(priority.ToString())
                : NotePriority.MEDIUM;

        await _repository.Save(note);

        return note;
    }
}

public interface INoteService
{
    Task<IEnumerable<NoteEntity>> List();
    Task<NoteEntity?> Get(string id);
    Task<NoteEntity> Create(string title, string? content, int priority, string userId);
    Task<NoteEntity> Update(string id, string title, string content, int priority);
}
