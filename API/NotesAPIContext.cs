using Microsoft.EntityFrameworkCore;
using API.Domain.User;
using API.Domain.Note;
using API.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

namespace API
{
    public class NotesAPIContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public NotesAPIContext(DbContextOptions<NotesAPIContext> options) : base(options)
        {
        }

        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<NoteEntity> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
        }
    }
}

