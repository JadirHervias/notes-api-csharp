using API.Domain.Exceptions;

namespace API.Domain.Note;

public class NoteNotExistsException : DomainException
{
	public NoteNotExistsException(string id) : base($"The note <{id}> not exists")
	{
	}
}

