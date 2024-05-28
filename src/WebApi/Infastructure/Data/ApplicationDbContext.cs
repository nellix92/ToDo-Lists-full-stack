using Microsoft.EntityFrameworkCore;
using webapi.Domains.Entities;

namespace webapi.Infastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
        public DbSet<ToDoList> ToDoLists => Set<ToDoList>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoList>()
                .HasMany(t => t.ToDoItems)
                .WithOne(ti => ti.ToDoList)
                .HasForeignKey(ti => ti.ToDoListId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
