using UnityEngine;

namespace ContractsInterfaces.Repositories
{
    public interface IGridRepository : IRepository
    {
        public float CellSize { get; }

        public float CellDistance { get; }
        public Vector2Int GridSize { get; }
    }
}