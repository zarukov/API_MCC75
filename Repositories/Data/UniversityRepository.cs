using API_MCC75.Repositories.Interface;
using API_MCC75.Contexts;
using API_MCC75.Models;

namespace API_MCC75.Repositories.Data;

public class UniversityRepository : GeneralRepository<int, University>
{
    private readonly MyContext context;

    public UniversityRepository(MyContext context) : base(context) 
    {
        this.context = context;
    }

}


