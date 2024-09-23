using System.Threading;
using System.Threading.Tasks;

namespace Core.Repository.Providers.EntityFramework
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}