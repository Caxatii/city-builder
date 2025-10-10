using Domain.Gameplay.Models.Grid;

namespace Core
{
    public static class DomainExtensions
    {
        public static Domain.Gameplay.Vector3 AsDomain(this UnityEngine.Vector3 vector)
        {
            return new Domain.Gameplay.Vector3 { X = vector.x, Y = vector.y, Z = vector.z };
        }

        public static Domain.Gameplay.Vector3 AsDomain(this UnityEngine.Vector2 vector)
        {
            return new Domain.Gameplay.Vector3(vector.x, vector.y);
        }

        public static UnityEngine.Vector3 AsUnity(this Domain.Gameplay.Vector3 vector)
        {
            return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Domain.Gameplay.Models.Grid.GridPosition AsDomain(this UnityEngine.Vector2Int vector2Int)
        {
            return new GridPosition(vector2Int.x, vector2Int.y);
        }
    }
}