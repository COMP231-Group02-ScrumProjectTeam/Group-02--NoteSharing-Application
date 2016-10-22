using Microsoft.EntityFrameworkCore;



namespace NoteShare.Models
{
    public class NoteSharingContext: DbContext
    {
        public NoteSharingContext(DbContextOptions<NoteSharingContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<UserComment> UserComments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
