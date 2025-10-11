using Domain.Gameplay.Models.Grid;
using UnityEngine;

namespace Core
{
    public static class DomainExtensions
    {
        public static Domain.Gameplay.Vector3 AsDomain(this Vector3 vector)
        {
            return new Domain.Gameplay.Vector3 { X = vector.x, Y = vector.y, Z = vector.z };
        }

        public static Domain.Gameplay.Vector3 AsDomain(this Vector2 vector)
        {
            return new Domain.Gameplay.Vector3(vector.x, vector.y);
        }

        public static Vector3 AsUnity(this Domain.Gameplay.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static GridPosition AsDomain(this Vector2Int vector2Int)
        {
            return new GridPosition(vector2Int.x, vector2Int.y);
        }

        public static Vector2Int AsUnity(this GridPosition position)
        {
            return new Vector2Int(position.Width, position.Height);
        }
    }
}