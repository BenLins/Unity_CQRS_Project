#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Project.Domain.Entities;

namespace Project.Application.Abstractions.Stores
{
    public interface ICountersStore
    {
        public Task<Counter[]> GetAll(CancellationToken cancellationToken);
        
        public Task<Counter?> GetById(string id, CancellationToken cancellationToken);
        
        public Task Save(Counter counter, CancellationToken cancellationToken);
    }
}
