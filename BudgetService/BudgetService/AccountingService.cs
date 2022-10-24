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
}