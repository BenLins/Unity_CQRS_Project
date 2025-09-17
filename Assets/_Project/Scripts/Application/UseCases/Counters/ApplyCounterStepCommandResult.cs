using Project.Application.Abstractions.Messaging;

namespace Project.Application.UseCases.Counters
{
    public class ApplyCounterStepCommandResult : IResult
    {
        public bool IsResultSuccess { get; internal set; }
        public string ResultMessage { get; internal set; }
        public int NewValue { get; internal set; }
    }
}
