namespace BudgetService;

public class AccountingService
{
    private readonly IBudgetRepo _budgetRepo;

    public AccountingService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        if (start > end)
        {
            return 0;
        }

        var budgets = _budgetRepo.GetAll();

        if (IsSameYearMonth(start, end))
        {
            var budget = GetBudget(start, budgets);
            var queryDays = (end - start).Days + 1;
            return queryDays * (budget.Amount / DateTime.DaysInMonth(start.Year, start.Month));
        }

        var current = start;
        var totalAmount = 0m;
        while (current < new DateTime(end.Year, end.Month, 1).AddMonths(1))
        {
            var currentBudget = GetBudget(current, budgets);
            decimal overlappingAmount;
            if (currentBudget.YearMonth == start.ToString("yyyyMM"))
            {
                var overlappingDays = (DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1);
                overlappingAmount = overlappingDays * GetDaysAmount(current, currentBudget.Amount);
            }
            else if (currentBudget.YearMonth == end.ToString("yyyyMM"))
            {
                var overlappingDays = end.Day;
                overlappingAmount = overlappingDays * (GetDaysAmount(current, currentBudget.Amount));
            }
            else
            {
                overlappingAmount = currentBudget.Amount;
            }

            totalAmount += overlappingAmount;

            current = current.AddMonths(1);
        }

        return totalAmount;
    }

    private static Budget? GetBudget(DateTime dateTime, List<Budget> budgets)
    {
        var budget = budgets.FirstOrDefault(x => x.YearMonth == dateTime.ToString("yyyyMM"));

        if (budget == null)
        {
            return new Budget();
        }

        return budget;
    }

    private static decimal GetDaysAmount(DateTime dateTime, int amount)
    {
        return amount / DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    private static bool IsSameYearMonth(DateTime start, DateTime end)
    {
        return start.ToString("yyyyMM") == end.ToString("yyyyMM");
    }
}