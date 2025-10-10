using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Gameplay.Views.UI
{
    public class ButtonView : IDisposable
    {
        private readonly Button _button;
        private readonly Label _label;

        public event Action<ButtonView> Clicked; 
        
        public ButtonView(Button button, Label label)
        {
            _button = button;
            _label = label;
            
            _button.clicked += OnClicked;
        }

        public Sprite Sprite
        {
            get => _button.style.backgroundImage.value.sprite;
            set => _button.style.backgroundImage = Background.FromSprite(value);
        }

        public string Text
        {
            get => _label.text;
            set => _label.text = value;
        }
        
        public void Dispose()
        {
            _button.clicked -= OnClicked;
        }

        private void OnClicked()
        {
            Clicked?.Invoke(this);
        }
    }
}