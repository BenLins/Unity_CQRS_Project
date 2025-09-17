using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.View
{
    public class ErrorPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _okButton;
        
        private void Awake()
        {
            _okButton.onClick.AddListener(Close);
        }
        
        public void DisplayMessage(string message)
        {
            _messageText.text = message;
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
