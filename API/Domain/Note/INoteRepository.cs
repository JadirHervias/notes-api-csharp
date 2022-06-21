namespace API.Domain.Note;

public interface INoteRepository
{
    Task<IEnumerable<NoteEntity>> List();
    Task<NoteEntity?> Get(Guid id);
    Task Save(NoteEntity note);
}