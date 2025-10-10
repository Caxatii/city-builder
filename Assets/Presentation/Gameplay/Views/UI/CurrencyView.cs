using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Gameplay.Views.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class CurrencyView : MonoBehaviour
    {
        private const string Currency = "Currency";
        
        private UIDocument _document;
        private Label _label;

        public string Text
        {
            get => _label.text;
            set => _label.text = value;
        }

        public void Initialize()
        {
            _document = GetComponent<UIDocument>();
            _label = _document.rootVisualElement.Q<Label>(Currency);
        }
    }
}