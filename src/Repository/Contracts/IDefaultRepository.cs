using Entities;
using System.Linq.Expressions;

namespace Repository.Contracts
{
    public interface IDefaultRepository<T> where T : EntityBaseBson
    {
        Task<T?> FindByIdAsync(string id);
        Task<List<T>>? FindAllAsync();
        Task<List<T>>? QueryAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T target);
        Task UpdateAsync(string id, T target);
        Task RemoveAsync(string id);
    }
}
