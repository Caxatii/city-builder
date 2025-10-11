using ContractsInterfaces.Repositories;
using Presentation.Gameplay.Views.Buildings;
using VContainer;

namespace Infrastructure.Installers
{
    public class BuildingFactoryInstaller : InstallerCommand
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterFactory<IBuildingView, BuildingView>(view =>
            {
                return buildingView => Instantiate(buildingView as BuildingView);
            }, Lifetime.Singleton);
        }
    }
}