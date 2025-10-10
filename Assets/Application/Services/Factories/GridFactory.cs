using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Grid;
using Infrastructure.Repositories.Gameplay.Grid;

namespace Application.Services.Factories
{
    public class GridFactory : IGridFactory
    {
        public GridModel Create(IGridRepository config)
        {
            return new GridModel(config.GridSize.x, config.GridSize.y);
        }
    }
}