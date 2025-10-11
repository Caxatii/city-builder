using Application.Services.Factories;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Installers;
using ContractsInterfaces.Repositories;
using Infrastructure.Repositories.Gameplay.Buildings.Effects;
using VContainer;

namespace Infrastructure.Installers
{
    public class ModelFactoryInstaller : InstallerCommand, IModelFactoryServiceInstaller
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IModelFactoryService, ModelFactoryService>(CreateModelFactory());
        }
        
        private IModelFactoryService CreateModelFactory()
        {
            IModelFactoryService factoryService = new ModelFactoryService();
            
            factoryService.Add<IBuildingRepository, IBuildingFactory>(CreateBuildingFactory());
            factoryService.Add<IGridRepository, IGridFactory>(new GridFactory());
            factoryService.Add<IIncreaseGoldEffectRepository, IIncreaseEffectFactory>(new IncreaseEffectFactory());
            factoryService.Add<ICameraSpeedRepository, ICameraSpeedFactory>(new CameraSpeedFactory());
            factoryService.Add<ICameraZoomRepository, ICameraZoomFactory>(new CameraZoomFactory());
            factoryService.Add<ICurrencyRepository, ICurrencyFactory>(new CurrencyFactory());

            return factoryService;
        }
        
        private IBuildingFactory CreateBuildingFactory()
        {
            BuildingFactory factory = new BuildingFactory();

            factory.Add<IncreaseGoldEffectRepository>(new IncreaseEffectFactory());
            
            return factory;
        }
    }
}