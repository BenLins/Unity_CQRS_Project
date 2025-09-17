namespace Project.Application.Abstractions.Messaging
{
    /// <summary>
    /// Интерфейс команды в паттерне CQRS.
    /// </summary>
    /// 
    /// <typeparam name="TResult">Тип результата, возвращаемого после выполнения команды.</typeparam>
    /// 
    /// <remarks>
    /// Команды изменяют состояние системы.
    /// Используется вместе с <see cref="ICommandHandler{TCommand,TResponse}"/> и медиатором <see cref="IGameCoreMediator"/>.
    /// </remarks>
    public interface ICommand<out TResult>
        where TResult : IResult
    {
        
    }
}
