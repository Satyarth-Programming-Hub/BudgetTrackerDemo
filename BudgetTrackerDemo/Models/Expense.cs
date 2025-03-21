namespace BudgetTrackerDemo.Models
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Amount { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime Date { get; set; }
    }
}
