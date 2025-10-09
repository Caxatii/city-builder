using System;
using System.Collections.Generic;
using Infrastructure.Repositories.Buildings;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    [RequireComponent(typeof(UIDocument))]
    public class UIDocumentTests : MonoBehaviour
    {
        private const string ElementsContainerTag = "ElementsContainer";
        private const string ButtonViewTag = "ButtonView";

        [SerializeField] private BuildingRepository[] _repositories;
        
        private UIDocument _document;
        private VisualTreeAsset _listElement;
        private ListView _listView;

        private List<VisualElement> _listElements = new();
        
        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            
            var root = _document.rootVisualElement;
            _listView = root.Q<ListView>(ElementsContainerTag);
        }

        private void OnEnable()
        {
            foreach (BuildingRepository repository in _repositories)
            {
                _listView.Add(CreatePanel(repository));
            }
        }

        private void OnDisable()
        {
            foreach (VisualElement element in _listElements)
            {
                _listView.Remove(element);
            }
        }

        private VisualElement CreatePanel(BuildingRepository repository)
        {
            VisualElement element = _listElement.CloneTree();
            _listElements.Add(element);

            element.Q<Button>(ButtonViewTag);
            
            return element;
        }
    }
}