using API_MCC75.Contexts;
using API_MCC75.Models;

namespace API_MCC75.Repositories.Data;

public class EmployeeRepository : GeneralRepository<string, Employee>
{
    private readonly MyContext context;

    public EmployeeRepository(MyContext context) : base(context)
	{
        this.context = context;
    }
}
