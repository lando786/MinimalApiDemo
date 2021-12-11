using Microsoft.EntityFrameworkCore;

namespace MinimalApiDemo.Models
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
       : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
