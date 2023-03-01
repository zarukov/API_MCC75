using API_MCC75.Contexts;
using API_MCC75.Models;

namespace API_MCC75.Repositories.Data;

public class EducationRepository : GeneralRepository<int, Education>
{
    private readonly MyContext context;

    public EducationRepository(MyContext context) : base(context)
	{
        this.context = context;
    }

}
