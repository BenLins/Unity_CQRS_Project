using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.View
{
    public class NewCounterButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        public event Action Clicked;
        
        private void Awake()
        {
            _button.onClick.AddListener(() => Clicked?.Invoke());
        }
    }
}
