using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Abstractions.Messaging
{
    /// <summary>
    /// Контракт обработчика запроса в паттерне CQRS.
    /// </summary>
    /// 
    /// <typeparam name="TQuery">Тип запроса, который обрабатывает данный обработчик.</typeparam>
    /// <typeparam name="TResult">Тип результата, возвращаемого после выполнения запроса.</typeparam>
    /// 
    /// <remarks>
    /// Обработчик инкапсулирует логику получения данных из домена или внешних источников,
    /// не изменяя состояние системы.
    /// </remarks>
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : IResult
    {
        /// <summary>
        /// Выполняет указанный запрос.
        /// </summary>
        /// 
        /// <param name="query">Запрос, который необходимо выполнить.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// 
        /// <returns>Результат выполнения запроса.</returns>
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
