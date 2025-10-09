using System;
using TriInspector;
using UnityEngine;

namespace Editor
{
    public class GridPlacer : MonoBehaviour
    {
        [SerializeField, Range(0, 180)] private float _rotationValue;
        [SerializeField] private float _placeDistance;
        [SerializeField] private Vector3 _placeDirection;
        [SerializeField] private Color _gizmosColor;
        
        [Title("Actions")]
        
        [Button]
        private void PlaceChildren()
        {
            int iteration = 0;
            
            ApplyForChildren(Place);

            void Place(Transform child) => child.position = _placeDirection * _placeDistance * iteration++;
        }

        [Button]
        private void RotateChildren()
        {
            ApplyForChildren(Rotate);

            void Rotate(Transform child) => 
                child.rotation = Quaternion.Euler(child.rotation.eulerAngles + new Vector3(0, _rotationValue, 0));
        }

        [Button]
        private void ResetRotationChildren()
        {
            ApplyForChildren(ResetRotation);

            void ResetRotation(Transform child) => child.rotation = Quaternion.identity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;

            foreach (Transform child in transform)
            {
                DrawCube(child.position);
            }
        }

        private void DrawCube(Vector2 position)
        {
            Vector3 size = new Vector3(_placeDistance, 100, _placeDistance);
            position.y += size.y / 2;
            
            Gizmos.DrawCube(position, size);
        }

        private void ApplyForChildren(Action<Transform> action)
        {
            foreach (Transform child in transform) 
                action(child);
        }
    }
}