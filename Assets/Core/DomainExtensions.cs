using Domain.Gameplay.Models.Grid;
using UnityEngine;

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

        public static GridPosition AsDomain(this UnityEngine.Vector2Int vector2Int)
        {
            return new GridPosition(vector2Int.x, vector2Int.y);
        }

        public static UnityEngine.Vector2Int AsUnity(this GridPosition position)
        {
            return new Vector2Int(position.Width, position.Height);
        }
    }
}