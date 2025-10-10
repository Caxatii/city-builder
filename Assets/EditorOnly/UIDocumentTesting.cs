using Infrastructure.Repositories.Gameplay.Buildings;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

namespace EditorOnly
{
    [RequireComponent(typeof(UIDocument))]
    public class UIDocumentTests : MonoBehaviour
    {
        private const string ElementsContainerTag = "ElementsContainer";
        private const string ButtonTag = "Button";
        private const string Cost = "Cost";

        [SerializeField] private BuildingRepository[] _repositories;
        
        private UIDocument _document;
        private void Awake()
        {
            _document = GetComponent<UIDocument>();

            VisualElement panel = _document.rootVisualElement.Q<VisualElement>(ElementsContainerTag);
            
            for (int i = 0; i < panel.childCount; i++)
            {
                UpdateUI(panel.Q<Button>("Button" + i), _repositories[i]);
            }
        }

        private void UpdateUI(Button button, BuildingRepository repository)
        {
            button.style.backgroundImage = Background.FromSprite(repository.Preview);

            button.Q<Label>(Cost).text = repository.Price.ToString();
        }
    }
}