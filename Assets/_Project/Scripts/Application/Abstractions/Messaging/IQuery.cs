namespace Project.Application.Abstractions.Messaging
{
    /// <summary>
    /// Интерфейс запроса в паттерне CQRS.
    /// </summary>
    /// 
    /// <typeparam name="TResult">Тип результата, возвращаемого после выполнения запроса.</typeparam>
    /// 
    /// <remarks>
    /// Запросы не изменяют состояние системы.
    /// Используется вместе с <see cref="IQueryHandler{TQuery,TResponse}"/> и медиатором <see cref="IGameCoreMediator"/>.
    /// </remarks>
    public interface IQuery<out TResult>
        where TResult : IResult
    {
        
    }
}
