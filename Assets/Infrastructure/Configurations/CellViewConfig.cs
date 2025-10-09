using UnityEngine;

namespace Infrastructure.Configurations
{
    public readonly struct CellViewConfig
    {
        public readonly Vector2Int Position;
        public readonly Vector3 Scale;
        public readonly Vector3 SpawnPosition;

        public CellViewConfig(Vector2Int position, Vector3 spawnPosition, Vector3 scale)
        {
            Scale = scale;
            SpawnPosition = spawnPosition;
            Position = position;
        }
    }
}