using System.ComponentModel.DataAnnotations;
using ContractsInterfaces.Installers;
using ContractsInterfaces.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Gameplay.Buildings;
using UnityEngine;
using VContainer;

namespace Infrastructure.Installers
{
    public class RepositoriesInstaller : InstallerCommand, IRepositoriesInstaller
    {
        [SerializeField, Required] private GameplayBuildingsRepository _buildingRepositories;

        [SerializeField] private RepositoryBase[] _repositories;
        
        public override void Configure(IContainerBuilder builder)
        {
            // builder.RegisterInstance(_cellViewPrefab);
            // builder.RegisterInstance(_gridView);
            //
            // builder.RegisterInstance<ICellColorizeRepository>(_cellColorizeRepository);
            // builder.RegisterInstance<ICurrencyRepository>(_currencyRepository);
            // builder.RegisterInstance<IGridRepository>(_gridRepository);
            
            foreach (RepositoryBase repository in _repositories) 
                builder.RegisterInstance(repository).AsImplementedInterfaces();
            
            builder.RegisterInstance<IGameplayBuildingsRepository>(
                new GameplayBindedBuildingsRepository(_buildingRepositories));
        }
    }
}