using System.Collections.Generic;
using System.Linq;
using ContractsInterfaces.Repositories;

namespace Infrastructure.Repositories.Gameplay.Buildings
{
    public class GameplayBindedBuildingsRepository : IGameplayBuildingsRepository
    {
        private GameplayBuildingsRepository _original;

        private Dictionary<string, IBuildingRepository> _bind;

        public GameplayBindedBuildingsRepository(GameplayBuildingsRepository original)
        {
            _original = original;

            _bind = _original.Repositories.ToDictionary(k => k.Name, b => b);
        }
        
        public IReadOnlyList<IBuildingRepository> Repositories => _original.Repositories;

        public IBuildingRepository Get(string name) => _bind[name];
    }
}