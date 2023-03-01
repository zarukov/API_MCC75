using API_MCC75.Contexts;
using API_MCC75.Models;

namespace API_MCC75.Repositories.Data;

public class RoleRepository : GeneralRepository<int, Role>
{
    private readonly MyContext context;

    public RoleRepository(MyContext context) : base(context)
	{
        this.context = context;
    }
}
