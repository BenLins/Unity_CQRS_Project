using Project.Application.Abstractions.Messaging;

namespace Project.Application.UseCases.Counters
{
    public class ApplyCounterStepCommand : ICommand<ApplyCounterStepCommandResult>
    {
        public string CounterId { get; set; }
        public CounterStepDirection StepDirection { get; set; }
    }
    
    public enum CounterStepDirection
    {
        Increment,
        Decrement
    }
}
