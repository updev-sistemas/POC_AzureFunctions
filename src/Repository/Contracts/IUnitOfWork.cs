using Entities;

namespace Repository.Contracts
{
    public interface IUnitOfWork
    {
        IDefaultRepository<StateBson> States { get; }
    }
}
