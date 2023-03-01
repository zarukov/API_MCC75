using API_MCC75.Contexts;
using API_MCC75.Models;

namespace API_MCC75.Repositories.Data;

public class ProfilingRepository : GeneralRepository<int, Profiling>
{
    private readonly MyContext context;

    public ProfilingRepository(MyContext context) : base(context)
	{
        this.context = context;
    }
}
