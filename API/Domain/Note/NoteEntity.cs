using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using API.Domain.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Domain.Note;

public class NoteEntity : ISerializable
{
    public Guid Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; }

    public string? Content { get; set; }

    public NotePriority Priority { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ValidateNever]
    public virtual UserEntity User { get; set; }

    public string? Summary { get; internal set; }

    public NoteEntity() { }

    public NoteEntity(
        Guid id,
        string title,
        string? content,
        NotePriority priority,
        Guid userId,
        string? summary
    )
    {
        Id = id;
        Title = title;
        Content = content;
        Priority = priority;
        UserId = userId;
        Summary = summary;
    }

    public static NoteEntity From(NotePrimitives notePrimitives)
    {
        return new NoteEntity(
            Guid.Parse(notePrimitives.Id),
            notePrimitives.Title,
            notePrimitives.Content,
            Enum.IsDefined(typeof(NotePriority), notePrimitives.Priority)
                ? Enum.Parse<NotePriority>(notePrimitives.Priority.ToString())
                : NotePriority.MEDIUM,
            Guid.Parse(notePrimitives.UserId),
            SummarizeContent(notePrimitives.Content)
        );
    }

    public static string? SummarizeContent(string? content)
    {
        return null != content
               ? $"{content?[0..Math.Min(content.Length, 22)].Trim()}..."
               : null;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("id", Id);
        info.AddValue("title", Title);
        info.AddValue("content", Content);
        info.AddValue("priority", Priority);
        info.AddValue("user_id", UserId);
        info.AddValue("summary", SummarizeContent(Content));
    }
}

public enum NotePriority
{
    HIGH = 1,
    MEDIUM = 2,
    LOW = 3
}

public struct NotePrimitives
{
    public string Id;
    public string Title;
    public string? Content;
    public int Priority;
    public string UserId;

    public NotePrimitives(
        string id,
        string title,
        string? content,
        int priority,
        string userId
    )
    {
        Id = id;
        Title = title;
        Content = content;
        Priority = priority;
        UserId = userId;
    }
}
