using Microsoft.EntityFrameworkCore;

namespace MinimalApiDemo.Models
{
    public class PizzaDb : DbContext
    {
        public PizzaDb(DbContextOptions<PizzaDb> options)
       : base(options) { }

        public DbSet<Pizza> Todos => Set<Pizza>();
    }
}
