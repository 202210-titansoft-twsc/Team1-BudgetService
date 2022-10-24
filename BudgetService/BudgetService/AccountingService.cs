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
            if (budget == null)
            {
                return 0;
            }

            var queryDays = (end - start).Days + 1;
            return queryDays * (budget.Amount / DateTime.DaysInMonth(start.Year, start.Month));
        }

        var current = start;
        var totalAmount = 0m;
        while (current < new DateTime(end.Year, end.Month, 1).AddMonths(1))
        {
            var currentBudget = GetBudget(current, budgets);
            if (currentBudget != null)
            {
                int overlappingDays;
                if (currentBudget.YearMonth == start.ToString("yyyyMM"))
                {
                    overlappingDays = (currentBudget.LastDay() - start).Days + 1;
                    // overlappingAmount = overlappingDays * GetDaysAmount(current, currentBudget.Amount);
                }
                else if (currentBudget.YearMonth == end.ToString("yyyyMM"))
                {
                    overlappingDays = (end - currentBudget.FirstDay()).Days + 1;
                    // overlappingAmount = overlappingDays * GetDaysAmount(current, currentBudget.Amount);
                }
                else
                {
                    overlappingDays = (currentBudget.LastDay() - currentBudget.FirstDay()).Days + 1;
                    // overlappingAmount = overlappingDays * GetDaysAmount(current, currentBudget.Amount);
                }

                var overlappingAmount = overlappingDays * GetDaysAmount(current, currentBudget.Amount);

                totalAmount += overlappingAmount;
            }

            current = current.AddMonths(1);
        }

        return totalAmount;
    }

    private static Budget? GetBudget(DateTime dateTime, List<Budget> budgets)
    {
        var budget = budgets.FirstOrDefault(x => x.YearMonth == dateTime.ToString("yyyyMM"));

        return budget;
    }

    private static decimal GetDaysAmount(DateTime dateTime, int amount)
    {
        return amount / (decimal)DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    private static bool IsSameYearMonth(DateTime start, DateTime end)
    {
        return start.ToString("yyyyMM") == end.ToString("yyyyMM");
    }
}