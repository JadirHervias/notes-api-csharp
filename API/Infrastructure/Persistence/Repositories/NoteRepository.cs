using API.Domain.Note;

namespace API.Infrastructure.Persistence.Repositories;

public class NoteRepository: INoteRepository
{
    private readonly NotesAPIContext _context;

    public NoteRepository(NotesAPIContext NotesAPIContext) => _context = NotesAPIContext;

    public Task<IEnumerable<NoteEntity>> List()
    {
        var query = from n in _context.Notes select n;

        return Task.Run(() => query.AsEnumerable());
    }

    public async Task<NoteEntity?> Get(Guid id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task Save(NoteEntity note)
    {
        NoteEntity? foundNote = await _context.Notes.FindAsync(note.Id);

        if (null == foundNote)
        {
            _context.Notes.Add(note);
        }
        else {
            _context.Notes.Update(note);
        }

        
        await _context.SaveChangesAsync();
    }
}

