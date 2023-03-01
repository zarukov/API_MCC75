using Microsoft.AspNetCore.Mvc;

namespace API_MCC75.Repositories.Interface;

public interface IRepository<Key, Entity> where Entity : class
{
    Task<int> Insert(Entity entity);
    Task<IEnumerable<Entity>> GetAll();
    Task<Entity?> GetById(Key key);
    Task<int> Update(Entity entity);
    Task<int> Delete(Key key);
}
