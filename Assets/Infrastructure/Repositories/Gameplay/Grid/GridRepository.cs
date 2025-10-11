using Application.Core.Attributes;
using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Infrastructure.Repositories.Gameplay.Grid
{
    [RepositoryType(typeof(IGridRepository))]
    [CreateAssetMenu(fileName = "GridRepository", 
        menuName = "Gameplay/Grid Repository")]
    public class GridRepository : RepositoryBase, IGridRepository
    {
        [SerializeField] private float _cellSize;
        [SerializeField] private float _cellDistance;
        [SerializeField] private Vector2Int _gridSize;
        
        public Vector2Int GridSize => _gridSize;

        public float CellSize => _cellSize;

        public float CellDistance => _cellDistance;
    }
}