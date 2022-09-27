using Microsoft.EntityFrameworkCore;

namespace blog.Models
{
    public class context:DbContext
    {
        public DbSet<customer> customer { get; set; }
        public DbSet<catalog> catalogs { get; set; }
        public DbSet <news> news { get; set; }
        public DbSet<contactt>contactts { get; set; }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security = SSPI; Persist Security Info = False; Initial Catalog =Newspaperr ; Data Source =.");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
