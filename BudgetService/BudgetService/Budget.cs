namespace BudgetService;

public class Budget
{
    public int Amount { get; set; }
    public string YearMonth { get; set; }

    public decimal OverlappingAmount(Period period)
    {
        return period.GetOverlappingDays(CreatePeriod()) * DailyAmount();
    }

    private Period CreatePeriod()
    {
        return new Period(FirstDay(), LastDay());
    }

    private decimal DailyAmount()
    {
        return Amount / (decimal)Days();
    }

    private int Days()
    {
        return DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month);
    }

    private DateTime FirstDay()
    {
        return DateTime.ParseExact(YearMonth, "yyyyMM", null);
    }

    private DateTime LastDay()
    {
        return new DateTime(FirstDay().Year, FirstDay().Month, Days());
    }
}