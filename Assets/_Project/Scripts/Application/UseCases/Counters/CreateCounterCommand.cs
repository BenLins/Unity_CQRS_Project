using Project.Application.Abstractions.Messaging;

namespace Project.Application.UseCases.Counters
{
    public class CreateCounterCommand : ICommand<CreateCounterCommandResult>
    {
        public string Name { get; set; }
        public int Step { get; set; }
    }
}
