namespace Project.Application.Abstractions.Messaging
{
    /// <summary>
    /// Результат выполнения команды.
    /// </summary>
    public interface IResult
    { 
        /// <summary>
        /// Флаг успешности выполнения команды.
        /// </summary>
        public bool IsResultSuccess { get; }
         
        /// <summary>
        /// Сообщение о результате выполнения команды.
        /// </summary>
        public string ResultMessage { get; }
    }
}
