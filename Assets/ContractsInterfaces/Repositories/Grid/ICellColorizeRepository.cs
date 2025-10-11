using UnityEngine;

namespace ContractsInterfaces.Repositories
{
    public interface ICellColorizeRepository : IRepository
    {
        public Color Accept { get; }
        public Color Discard { get; }
    }
}