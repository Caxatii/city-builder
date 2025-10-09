using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Presentation.Gameplay.Views.Buildings
{
    public class BuildingGhostView : MonoBehaviour
    {
        private List<MeshRenderer> _renderers;
        private Dictionary<MeshRenderer, List<Material>> _originalMaterials;
        
        private void Awake()
        {
            _originalMaterials = GetComponentsInChildren<MeshRenderer>().
                ToDictionary(key => key, value => value.materials.ToList());

            _renderers = _originalMaterials.Keys.ToList();
        }

        private void OnDisable()
        {
            foreach (var keyValuePair in _originalMaterials)
            {
                keyValuePair.Key.SetMaterials(keyValuePair.Value);
            }
        }

        public void SetMaterial(Material ghostMaterial)
        {
            foreach (MeshRenderer meshRenderer in _renderers)
                meshRenderer.SetMaterials(CreateMaterialsList(ghostMaterial, meshRenderer));
        }

        public void SetColor(Color color)
        {
            foreach (MeshRenderer meshRenderer in _renderers)
                foreach (Material material in meshRenderer.materials) 
                    material.color = color;
        }

        private List<Material> CreateMaterialsList(Material material, MeshRenderer meshRenderer)
        {
            int capacity = _originalMaterials[meshRenderer].Count;
            var materials = new List<Material>(capacity);

            for (int i = 0; i < capacity; i++) 
                materials[i] = material;
            
            return materials;
        }
    }
}