using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.View
{
    public class NewCounterPopupView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TMP_InputField _stepInputField;
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;
        
        public string Name => _nameInputField.text;
        public int Step => int.Parse(_stepInputField.text);
        
        public event Action CreateClicked;
        
        private void Awake()
        {
            _okButton.onClick.AddListener(() => CreateClicked?.Invoke());
            _cancelButton.onClick.AddListener(Close);
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
