using API.Domain.Exceptions;

namespace API.Domain.Note;

public class NoteAlreadyExistsException : DomainException
{
	public NoteAlreadyExistsException(string id) : base($"The note <{id}> already exists")
	{
	}
}

