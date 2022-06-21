using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using API.Domain.Note;

namespace API.Domain.User;

public class UserEntity : ISerializable
{
	public Guid Id { get; set; }

	public string UserName { get; set; }

	public string FullName { get; set; }

	public string Email { get; set; }

	[JsonIgnore]
	public string Password { get; set; }

	[JsonIgnore]
	public virtual ICollection<NoteEntity> Notes { get; private set; }

	public UserEntity() { }

	public UserEntity(
		Guid id,
		string fullName,
		string email,
		string userName,
		string password
	)
    {
		Id = id;
		FullName = fullName;
		Email = email;
		UserName = userName;
		Password = password;
		Notes = new HashSet<NoteEntity>();
	}

	public static UserEntity From(UserPrimitives userPrimitives)
	{
		return new UserEntity(
			Guid.Parse(userPrimitives.Id),
			userPrimitives.FullName,
			userPrimitives.Email,
			userPrimitives.UserName,
			userPrimitives.Password
		); ;
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("id", Id);
		info.AddValue("full_name", FullName);
		info.AddValue("email", Email);
		info.AddValue("username", UserName);
		info.AddValue("password", null);
	}
}


public struct UserPrimitives
{
	public string Id;
	public string FullName;
	public string Email;
	public string UserName;
	public string Password;

	public UserPrimitives(
		string id,
		string fullName,
		string email,
		string userName,
		string password
	)
	{
		Id = id;
		FullName = fullName;
		Email = email;
		UserName = userName;
		Password = password;
	}
}
