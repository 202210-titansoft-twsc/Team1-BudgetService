namespace BudgetService;

public class Period
{
    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    private DateTime End { get; set; }
    private DateTime Start { get; set; }

    public int GetOverlappingDays(Budget budget)
    {
        var another = new Period(budget.FirstDay(), budget.LastDay());
        var lastDay = another.End;
        var firstDay = another.Start;
        var overlappingEnd = End < lastDay
            ? End
            : lastDay;
        var overlappingStart = Start > firstDay
            ? Start
            : firstDay;

        return (overlappingEnd - overlappingStart).Days + 1;
    }
}