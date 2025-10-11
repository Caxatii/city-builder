using System.Collections.Generic;

namespace ContractsInterfaces.Repositories
{
    public interface IGameplayBuildingsRepository : IRepository
    {
        public IReadOnlyList<IBuildingRepository> Repositories { get; }

        public IBuildingRepository Get(string name);
    }
}