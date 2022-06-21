namespace API.App.Requests;

public record UpdateNoteRequest(string Title, string Content, int Priority);

