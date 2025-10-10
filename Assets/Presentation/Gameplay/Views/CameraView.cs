using TriInspector;
using UnityEngine;

namespace Presentation.Gameplay.Views
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField, Required] private Camera _camera;

        public Camera Camera => _camera;
    }
}