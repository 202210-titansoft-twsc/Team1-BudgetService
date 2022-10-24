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
        var overlappingEnd = End < another.End
            ? End
            : another.End;
        var overlappingStart = Start > another.Start
            ? Start
            : another.Start;

        return (overlappingEnd - overlappingStart).Days + 1;
    }
}