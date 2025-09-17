using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Stores;
using Project.Application.Mappings;

namespace Project.Application.UseCases.Counters
{
    public class GetCountersQueryHandler : IQueryHandler<GetCountersQuery, GetCountersQueryResult>
    {
        private readonly ICountersStore _countersStore;
        
        public GetCountersQueryHandler(ICountersStore countersStore)
        {
            _countersStore = countersStore;
        }
        
        public async Task<GetCountersQueryResult> Handle(GetCountersQuery query, CancellationToken cancellationToken)
        {
            var counters = await _countersStore.GetAll(cancellationToken);
            
            var countersDto = counters.Select(CounterDtoMapper.MapToDto).ToArray();
            
            return new GetCountersQueryResult
            {
                IsResultSuccess = true,
                Counters = countersDto
            };
        }
    }
}
