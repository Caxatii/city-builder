using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Buildings;

namespace ContractsInterfaces.FactoriesApplication
{
    public interface IBuildingFactory : IModelFactory<Building, IBuildingRepository> { }
}