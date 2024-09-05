using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ILCSDbContext : DbContext
    {
        public ILCSDbContext(DbContextOptions<ILCSDbContext> options)
        : base(options)
        {
        }

        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>()
                .ToTable("ToDoList")
                .HasKey(t => t.Id);
        }
    }
}
