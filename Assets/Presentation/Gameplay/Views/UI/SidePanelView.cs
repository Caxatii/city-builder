using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Gameplay.Views.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class SidePanelView : MonoBehaviour
    {
        private const string Button = "Button";
        private UIDocument _document;

        private List<ButtonView> _buttons = new();
        
        public IReadOnlyList<ButtonView> Views => _buttons;
        
        public void Initialize()
        {
            _document = GetComponent<UIDocument>();

            var root = _document.rootVisualElement.Q("ElementsContainer");

            for (int i = 0; i < root.childCount; i++)
            {
                CreateButton(root.Q<Button>(Button + i));
            }
        }
        
        private void CreateButton(Button button)
        {
            _buttons.Add(new ButtonView(button, button.Q<Label>("Cost")));
        }
    }
}