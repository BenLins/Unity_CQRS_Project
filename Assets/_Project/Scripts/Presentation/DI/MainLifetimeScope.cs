using UnityEngine;
using VContainer;
using VContainer.Unity;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Stores;
using Project.Application.UseCases.Counters;
using Project.Infrastructure.Stores;
using Project.Presentation.Screens;
using Project.View;

namespace Project.Presentation.DI
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private CounterView _counterViewPrefab;
        [SerializeField] private NewCounterPopupView _newCounterPopupView;
        [SerializeField] private ErrorPopupView _errorPopupView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ICountersStore, PlayerPrefsCountersStore>(Lifetime.Singleton);

            builder.Register<GetCountersQueryHandler>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<CreateCounterCommandHandler>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<ApplyCounterStepCommandHandler>(Lifetime.Transient).AsImplementedInterfaces();

            builder.Register<IGameCoreMediator ,VContainerApplicationMediator>(Lifetime.Transient);
            
            builder.RegisterInstance(_counterViewPrefab);
            builder.RegisterInstance(_newCounterPopupView);
            builder.RegisterInstance(_errorPopupView);
            builder.RegisterComponentInHierarchy<NewCounterButtonView>();
            builder.RegisterComponentInHierarchy<CountersContainerView>();
            
            builder.RegisterEntryPoint<MainScreenPresenter>();
        }
    }
}
