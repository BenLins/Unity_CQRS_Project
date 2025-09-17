using Project.Application.Abstractions.Messaging;
using Project.Application.Dto;

namespace Project.Application.UseCases.Counters
{
    public class CreateCounterCommandResult : IResult
    {
        public bool IsResultSuccess { get; internal set; }
        public string ResultMessage { get; internal set; }
        
        public CounterDto Counter { get; internal set; }
    }
}
