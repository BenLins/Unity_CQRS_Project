using UnityEngine;
using VContainer.Unity;
using Project.Application.Abstractions.Messaging;
using Project.Application.Dto;
using Project.Application.UseCases.Counters;
using Project.View;

namespace Project.Presentation.Screens
{
    public class MainScreenPresenter : IStartable
    {
        private readonly IGameCoreMediator _gameCoreMediator;
        private readonly CountersContainerView _containerView;
        private readonly CounterView _counterViewPrefab;
        private readonly NewCounterButtonView _newCounterButtonView;
        private readonly NewCounterPopupView _newCounterPopupViewPrefab;
        private readonly ErrorPopupView _errorPopupViewPrefab;
        
        public MainScreenPresenter(
            IGameCoreMediator gameCoreMediator,
            CountersContainerView containerView,
            CounterView counterViewPrefab,
            NewCounterButtonView newCounterButtonView,
            NewCounterPopupView newCounterPopupViewPrefab,
            ErrorPopupView errorPopupViewPrefab)
        {
            _gameCoreMediator = gameCoreMediator;
            _containerView = containerView;
            _counterViewPrefab = counterViewPrefab;
            _newCounterButtonView = newCounterButtonView;
            _newCounterPopupViewPrefab = newCounterPopupViewPrefab;
            _errorPopupViewPrefab = errorPopupViewPrefab;
        }
        
        public async void Start()
        {
            _newCounterButtonView.Clicked += OnNewCounterButtonClick;
            
            var result = await _gameCoreMediator.HandleQuery(new GetCountersQuery());

            foreach (var counterDto in result.Counters)
            {
                InstantiateCounter(counterDto);
            }
        }

        private void InstantiateCounter(CounterDto counterDto)
        {
            var counterView = GameObject.Instantiate(_counterViewPrefab, _containerView.transform);
            
            counterView.Initialize(counterDto);
            
            counterView.Increment += () => IncrementCounter(counterView);
            counterView.Decrement += () => DecrementCounter(counterView);
        }
        
        private async void IncrementCounter(CounterView counterView)
        {
            var result = await _gameCoreMediator.HandleCommand(new ApplyCounterStepCommand
            {
                CounterId = counterView.Id,
                StepDirection = CounterStepDirection.Increment
            });
            
            if (!result.IsResultSuccess)
            {
                DisplayErrorPopup(result.ResultMessage);
                
                return;
            }
            
            counterView.SetValue(result.NewValue);
        }
        
        private async void DecrementCounter(CounterView counterView)
        {
            var result = await _gameCoreMediator.HandleCommand(new ApplyCounterStepCommand
            {
                CounterId = counterView.Id,
                StepDirection = CounterStepDirection.Decrement
            });
            
            if (!result.IsResultSuccess)
            {
                DisplayErrorPopup(result.ResultMessage);
                
                return;
            }
            
            counterView.SetValue(result.NewValue);
        }
        
        private void OnNewCounterButtonClick()
        {
            var popupView = GameObject.Instantiate(_newCounterPopupViewPrefab);
            
            popupView.CreateClicked += () =>
            {
                popupView.Close();
                
                OnNewCounterPopupCreateClicked(popupView.Name, popupView.Step);
            };
        }

        private async void OnNewCounterPopupCreateClicked(string name, int step)
        {
            var result = await _gameCoreMediator.HandleCommand(new CreateCounterCommand
            {
                Name = name,
                Step = step
            });
            
            if (!result.IsResultSuccess)
            {
                DisplayErrorPopup(result.ResultMessage);
                
                return;
            }
            
            InstantiateCounter(result.Counter);
        }
        
        private void DisplayErrorPopup(string message)
        {
            var popupView = GameObject.Instantiate(_errorPopupViewPrefab);
            
            popupView.DisplayMessage(message);
        }
    }
}
