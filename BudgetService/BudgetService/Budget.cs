namespace BudgetService;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public DateTime LastDay()
    {
        var firstDay = DateTime.ParseExact(YearMonth,"yyyyMM", null);
        var daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
        return new DateTime(firstDay.Year, firstDay.Month, daysInMonth);
    }
}