using Application.UseCases.Grid;
using ContractsInterfaces.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    [RequireComponent(typeof(IRepositoriesInstaller))]
    [RequireComponent(typeof(IViewsInstaller))]
    public class GameplayGridUseCasesInstaller : InstallerCommand
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GridInitializeUseCase>();
            builder.RegisterEntryPoint<GridSelectionUseCase>();
            builder.RegisterEntryPoint<GridColorizeUseCase>();
            builder.RegisterEntryPoint<GridGhostBuildUseCase>();
            builder.RegisterEntryPoint<GridPlacerUseCase>();
            builder.RegisterEntryPoint<GridRemoveBuildUseCase>();
            
            builder.RegisterEntryPoint<AddGridGoldRewardUseCase>();
        }
    }
}