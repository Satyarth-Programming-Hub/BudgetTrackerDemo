using BudgetTrackerDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerDemo.Data
{
    public class BudgetTrackerContext : DbContext
    {
        public BudgetTrackerContext(DbContextOptions<BudgetTrackerContext> options) : base(options){}
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
