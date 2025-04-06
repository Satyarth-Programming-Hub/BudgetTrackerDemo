using BudgetTrackerDemo.Models;

namespace BudgetTrackerDemo.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(BudgetTrackerContext context)
        {
            context.Database.EnsureCreated();
            
            // Check if the "Uncategorized" category already exists
            if (!context.Categories.Any(c => c.Name == "Uncategorized"))
            {
                // Add the "Uncategorized" category if it doesn't exist
                context.Categories.Add(new Category { Name = "Uncategorized" });
                context.SaveChanges();
            }

            //if (context.Categories.Any())
            //{
            //    return; // DB has been seeded
            //}
            //var categories = new[]
            //{
            //    new Category { Name = "Food" },
            //    new Category { Name = "Transport" },
            //    new Category { Name = "Entertainment" },
            //    new Category { Name = "Utilities" },
            //    new Category { Name = "Healthcare" },
            //    new Category { Name = "Uncategorized" }
            //};
            //context.Categories.AddRange(categories);
            //context.SaveChanges();
        }
    }
}
