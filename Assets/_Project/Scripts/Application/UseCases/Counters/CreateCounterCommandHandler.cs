using System;
using System.Threading;
using System.Threading.Tasks;
using Project.Domain.Entities;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Stores;
using Project.Application.Mappings;

namespace Project.Application.UseCases.Counters
{
    public class CreateCounterCommandHandler : ICommandHandler<CreateCounterCommand, CreateCounterCommandResult>
    {
        private readonly ICountersStore _countersStore;
        
        public CreateCounterCommandHandler(ICountersStore countersStore)
        {
            _countersStore = countersStore;
        }
        
        public Task<CreateCounterCommandResult> Handle(CreateCounterCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                return Task.FromResult(new CreateCounterCommandResult
                {
                    IsResultSuccess = false,
                    ResultMessage = "Имя счетчика не может быть пустым."
                });
            
            if (command.Step < 0)
                return Task.FromResult(new CreateCounterCommandResult
                {
                    IsResultSuccess = false,
                    ResultMessage = "Шаг счетчика не может быть отрицательным."
                });
            
            var newCounter = new Counter(Guid.NewGuid().ToString(), command.Name, 0, command.Step);
            
            _countersStore.Save(newCounter, cancellationToken);
            
            return Task.FromResult(new CreateCounterCommandResult
            {
                IsResultSuccess = true,
                Counter = CounterDtoMapper.MapToDto(newCounter)
            });
        }
    }
}
