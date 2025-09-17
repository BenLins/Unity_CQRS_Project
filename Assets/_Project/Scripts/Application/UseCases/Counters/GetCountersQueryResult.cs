using System.Collections.Generic;
using Project.Application.Abstractions.Messaging;
using Project.Application.Dto;

namespace Project.Application.UseCases.Counters
{
    public class GetCountersQueryResult : IResult
    {
        public bool IsResultSuccess { get; internal set; }
        public string ResultMessage { get; internal set; }
        public IEnumerable<CounterDto> Counters { get; internal set; }
    }
}
