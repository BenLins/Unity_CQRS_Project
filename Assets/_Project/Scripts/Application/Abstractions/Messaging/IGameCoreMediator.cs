using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Abstractions.Messaging
{
    /// <summary>
    /// Контракт медиатора для обработки команд и запросов в паттерне CQRS.
    /// </summary>
    ///
    /// <remarks>
    /// Медиатор инкапсулирует логику поиска и вызова соответствующего обработчика для команды или запроса к ядру игровой логики.
    /// </remarks>
    public interface IGameCoreMediator
    {
        /// <summary>
        /// Выполняет указанную команду.
        /// </summary>
        /// 
        /// <remarks>
        /// Команды изменяют состояние системы.
        /// </remarks>
        /// 
        /// <param name="command">Команда, которую необходимо выполнить.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// 
        /// <returns>Результат выполнения команды.</returns>
        public Task<TResult> HandleCommand<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
            where TResult : IResult;

        /// <summary>
        /// Выполняет указанный запрос.
        /// </summary>
        /// 
        /// <remarks>
        /// Запросы не изменяют состояние системы.
        /// </remarks>
        /// 
        /// <param name="query">Запрос, который необходимо выполнить.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// 
        /// <returns>Результат выполнения запроса.</returns>
        public Task<TResult> HandleQuery<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
            where TResult : IResult;
    }
}
