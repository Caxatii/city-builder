using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Grid;

namespace ContractsInterfaces.FactoriesApplication
{
    public interface IGridFactory : IModelFactory<GridModel, IGridRepository> { }
}