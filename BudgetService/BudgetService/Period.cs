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

    public int GetOverlappingDays(Budget currentBudget)
    {
        DateTime overlappingEnd = End < currentBudget.LastDay()
            ? End
            : currentBudget.LastDay();
        DateTime overlappingStart = Start > currentBudget.FirstDay()
            ? Start
            : currentBudget.FirstDay();
        if (currentBudget.YearMonth == Start.ToString("yyyyMM"))
        {
            // overlappingEnd = currentBudget.LastDay();
            // overlappingStart = Start;
        }
        else if (currentBudget.YearMonth == End.ToString("yyyyMM"))
        {
            // overlappingEnd = End;
            // overlappingStart = currentBudget.FirstDay();
        }
        else
        {
            // overlappingEnd = currentBudget.LastDay();
            // overlappingStart = currentBudget.FirstDay();
        }

        return (overlappingEnd - overlappingStart).Days + 1;
    }
}