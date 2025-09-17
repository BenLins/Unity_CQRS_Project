using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using VContainer;
using Project.Application.Abstractions.Messaging;

namespace Project.Presentation.DI
{
    public sealed class VContainerApplicationMediator : IGameCoreMediator
    {
        private static readonly ConcurrentDictionary<Type, MethodInfo> HandleCache = new();
        private readonly IObjectResolver _resolver;

        public VContainerApplicationMediator(IObjectResolver resolver) => _resolver = resolver;

        public async Task<TResult> HandleCommand<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
            where TResult : IResult
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            
            return await Handle<TResult>(handlerType, command, cancellationToken);
        }

        public async Task<TResult> HandleQuery<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
            where TResult : IResult
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            
            return await Handle<TResult>(handlerType, query, cancellationToken);
        }
        
        private async Task<TResult> Handle<TResult>(Type handlerType, object command, CancellationToken cancellationToken)
        {
            var handler= _resolver.Resolve(handlerType);
            
            var method = GetHandleMethod(handlerType);

            var task = (Task<TResult>)method.Invoke(handler, new object[] { command, cancellationToken });
            
            return await task.ConfigureAwait(false);
        }

        private static MethodInfo GetHandleMethod(Type handlerType) =>
            HandleCache.GetOrAdd(handlerType, t =>
                t.GetMethod("Handle", BindingFlags.Instance | BindingFlags.Public)
                ?? throw new InvalidOperationException($"Handler {t} must have public Handle(...)"));
    }
}
