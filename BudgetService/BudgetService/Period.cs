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

    public int GetOverlappingDays(Period another)
    {
        var overlappingEnd = End < another.End
            ? End
            : another.End;
        var overlappingStart = Start > another.Start
            ? Start
            : another.Start;

        return (overlappingEnd - overlappingStart).Days + 1;
    }
}