using Products.Constants.Enums;
using Products.Domain.Entities;

namespace Products.Repository.Interfaces
{
    public interface ICodeTrackerRepository : IGenericRepository<CodeTracker>
    {
        Task<int> GetNextCodeAsync(CodeTrackerKey key);
    }
}
