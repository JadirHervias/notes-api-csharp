using API.Domain.User;
using API.Infrastructure.Persistence.EntityFramework.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(nameof(NotesAPIContext.Users).ToDatabaseFormat());

        builder.HasKey(e => e.Id);

        builder
            .HasMany(e => e.Notes)
            .WithOne(e => e.User);

        builder.HasIndex(e => e.Email).IsUnique();

        builder.Property(e => e.Id);

        builder.Property(e => e.Password).IsRequired();

        builder.Property(e => e.FullName).IsRequired();

        builder.Property(e => e.Email).IsRequired();

        builder.HasData(GetFakeUsers());
    }

    private static List<UserEntity> GetFakeUsers()
    {
        List<UserEntity> users = new();

        users.Add(UserEntity.From(new UserPrimitives(
            Guid.NewGuid().ToString(),
            "John Doe",
            "john_doe@gmail.com",
            "john123",
            "AQAAAAEAACcQAAAAEBHySPor5SHQGHomzXOtc2/qHdkS8NsOyUCgXv2vpcHvhE9vqxapNN58amAkOtNaBg=="
        )));

        users.Add(UserEntity.From(new UserPrimitives(
            Guid.NewGuid().ToString(),
            "Johan Cruyff",
            "johancruyff_47@gmail.com",
            "johanCF",
            "AQAAAAEAACcQAAAAEBHySPor5SHQGHomzXOtc2/qHdkS8NsOyUCgXv2vpcHvhE9vqxapNN58amAkOtNaBg=="
        )));

        return users;
    }
}

