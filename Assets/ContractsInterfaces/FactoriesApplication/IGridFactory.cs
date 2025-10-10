using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Grid;
using Infrastructure.Repositories.Gameplay.Grid;

namespace ContractsInterfaces.FactoriesApplication
{
    public interface IGridFactory : IModelFactory<GridModel, IGridRepository> { }
}