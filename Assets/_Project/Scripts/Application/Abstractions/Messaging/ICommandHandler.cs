using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Abstractions.Messaging
{
    /// <summary>
    /// Контракт обработчика команды в паттерне CQRS.
    /// </summary>
    /// 
    /// <typeparam name="TCommand">Тип команды, которую обрабатывает данный обработчик.</typeparam>
    /// <typeparam name="TResult">Тип результата, возвращаемого после выполнения команды.</typeparam>
    /// 
    /// <remarks>
    /// Обработчик инкапсулирует логику изменения состояния системы.
    /// Он не выполняет напрямую бизнес-логику сущностей, а координирует вызовы методов сущностей домена.
    /// Так же обработчик может содержать валидацию команды и бизнес-правила, которые должны быть выполнены перед изменением состояния.
    /// </remarks>
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : IResult
    {
        /// <summary>
        /// Выполняет указанную команду.
        /// </summary>
        /// 
        /// <param name="command">Команда, которую необходимо выполнить.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// 
        /// <returns>Результат выполнения команды.</returns>
        Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
