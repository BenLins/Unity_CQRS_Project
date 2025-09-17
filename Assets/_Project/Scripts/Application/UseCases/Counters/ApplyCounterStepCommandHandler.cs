using System;
using System.Threading;
using System.Threading.Tasks;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Stores;

namespace Project.Application.UseCases.Counters
{
    public class ApplyCounterStepCommandHandler : ICommandHandler<ApplyCounterStepCommand, ApplyCounterStepCommandResult>
    {
        private readonly ICountersStore _countersStore;
        
        public ApplyCounterStepCommandHandler(ICountersStore countersStore)
        {
            _countersStore = countersStore;
        }

        public async Task<ApplyCounterStepCommandResult> Handle(ApplyCounterStepCommand command, CancellationToken cancellationToken)
        {
            var counter = await _countersStore.GetById(command.CounterId, cancellationToken);
            
            if (counter is null)
                return new ApplyCounterStepCommandResult
                {
                    IsResultSuccess = false,
                    ResultMessage = "Счетчик не найден."
                };

            try
            {
                switch (command.StepDirection)
                {
                    case CounterStepDirection.Increment:
                        counter.Increment();
                        break;
                    case CounterStepDirection.Decrement:
                        counter.Decrement();
                        break;
                    default:
                        return new ApplyCounterStepCommandResult
                        {
                            IsResultSuccess = false,
                            ResultMessage = "Неверное направление шага."
                        };
                }
            }
            catch (Exception e)
            {
                return new ApplyCounterStepCommandResult
                {
                    IsResultSuccess = false,
                    ResultMessage = e.Message
                };
            }
            
            await _countersStore.Save(counter, cancellationToken);
            
            return new ApplyCounterStepCommandResult
            {
                IsResultSuccess = true,
                NewValue = counter.Value
            };
        }
    }
}
