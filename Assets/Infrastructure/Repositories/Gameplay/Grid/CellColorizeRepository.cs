using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Infrastructure.Repositories.Gameplay.Grid
{
    [CreateAssetMenu(fileName = "CellColorizeRepository", 
        menuName = "Gameplay/Cell Colorize Repository")]
    public class CellColorizeRepository : ScriptableObject, ICellColorizeRepository
    {
        [SerializeField] private Color _accept;
        [SerializeField] private Color _discard;

        public Color Accept => _accept;
        public Color Discard => _discard;
    }
}