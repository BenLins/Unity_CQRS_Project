using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Project.Application.Dto;

namespace Project.View
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private TextMeshProUGUI _stepText;
        [SerializeField] private Button _incrementButton;
        [SerializeField] private Button _decrementButton;

        public string Id { get; private set; }

        public event Action Increment;
        public event Action Decrement;
        
        public void Initialize(CounterDto counter)
        {
            Id = counter.Id;
            
            _nameText.text = counter.Name;
            
            _stepText.text = counter.Step.ToString();
            
            _valueText.text = counter.Value.ToString();
            
            _incrementButton.onClick.AddListener(() => Increment?.Invoke());
            _decrementButton.onClick.AddListener(() => Decrement?.Invoke());
        }
        
        public void SetValue(int value)
        {
            _valueText.text = value.ToString();
        }
    }
}
