namespace BudgetService;

public class Budget
{
    public int Amount { get; set; }
    public string YearMonth { get; set; }

    public Period CreatePeriod()
    {
        return new Period(FirstDay(), LastDay());
    }

    public decimal DailyAmount()
    {
        DateTime dateTime = FirstDay();
        return Amount / (decimal)DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    public DateTime FirstDay()
    {
        return DateTime.ParseExact(YearMonth, "yyyyMM", null);
    }

    public DateTime LastDay()
    {
        return new DateTime(FirstDay().Year, FirstDay().Month, Days());
    }

    private int Days()
    {
        return DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month);
    }
}