using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public partial class LibraryDBContext : DbContext 
    {
        public LibraryDBContext() { }
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options) { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
    }
}
