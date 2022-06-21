using API.Domain.Note;
using API.Infrastructure.Persistence.EntityFramework.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

public class NoteConfiguration : IEntityTypeConfiguration<NoteEntity>
{
    public void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        builder.ToTable(nameof(NotesAPIContext.Notes).ToDatabaseFormat());

        builder.HasKey(e => e.Id);

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Notes)
            .HasForeignKey(e => e.UserId);

        builder.HasIndex(e => e.Title);

        builder.Property(e => e.Id);

        builder
            .Property(e => e.Title)
            .IsUnicode(false)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(e => e.Content)
            .IsUnicode(false);

        builder.Property(e => e.Priority);

        builder.Property(e => e.UserId);

        builder.Ignore(e => e.Summary);
    }
}

