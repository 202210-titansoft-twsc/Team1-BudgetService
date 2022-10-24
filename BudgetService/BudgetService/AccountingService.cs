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

        var period = new Period(start, end);

        return _budgetRepo.GetAll().Sum(budget => budget.OverlappingAmount(period));
    }

    private static Budget? GetBudget(DateTime dateTime, List<Budget> budgets)
    {
        var budget = budgets.FirstOrDefault(x => x.YearMonth == dateTime.ToString("yyyyMM"));

        return budget;
    }

    private static bool IsSameYearMonth(DateTime start, DateTime end)
    {
        return start.ToString("yyyyMM") == end.ToString("yyyyMM");
    }
}