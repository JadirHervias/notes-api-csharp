namespace API.App.Requests;

public record CreateNoteRequest(string Title, string Content, int Priority, string UserId);

