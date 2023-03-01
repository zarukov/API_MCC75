using API_MCC75.Contexts;
using API_MCC75.Models;

namespace API_MCC75.Repositories.Data;

public class AccountRoleRepository : GeneralRepository<int, AccountRole>
{
    private readonly MyContext context;

    public AccountRoleRepository(MyContext context) : base(context)
	{
        this.context = context;
    }
}
