using Application.Services;
using Application.Services.Factories;
using Application.UseCases.Camera;
using Application.UseCases.Grid;
using Application.UseCases.UI;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Domain.Gameplay.MessagesDTO;
using Infrastructure.Repositories.Application.Camera;
using Infrastructure.Repositories.Gameplay.Buildings;
using Infrastructure.Repositories.Gameplay.Buildings.Effects;
using Infrastructure.Repositories.Gameplay.Currency;
using Infrastructure.Repositories.Gameplay.Grid;
using MessagePipe;
using Presentation.Gameplay.Views;
using Presentation.Gameplay.Views.Grid;
using Presentation.Gameplay.Views.UI;
using TriInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class GameplayInstaller : LifetimeScope
    {
        [SerializeField] private float _rewardDelay;
        [SerializeField] private float _autoSaveDelay;
        
        [SerializeField, Required] private CellView _cellViewPrefab;
        [SerializeField, Required] private CameraView _camera;
        [SerializeField, Required] private SidePanelView _sidePanelView;
        [SerializeField, Required] private CurrencyView _currencyView;

        [SerializeField, Required] private CameraRepository _cameraRepository;
        [SerializeField, Required] private GridView _gridView;
        [SerializeField, Required] private GridRepository _gridRepository;
        [SerializeField, Required] private CurrencyRepository _currencyRepository;

        [SerializeField] private GameplayBuildingsRepository _buildingRepositories;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
            
            RegisterRepositories(builder);
            RegisterServices(builder);
            
            RegisterUseCases(builder);
            
        }

        private void RegisterUseCases(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CameraMoveUseCase>().As<ICameraMoveUseCase>();
            builder.RegisterEntryPoint<CameraZoomUseCase>();
            
            builder.RegisterEntryPoint<GridInitializeUseCase>();
            builder.RegisterEntryPoint<GridSelectionUseCase>();
            builder.RegisterEntryPoint<GridGhostBuildUseCase>();
            builder.RegisterEntryPoint<GridPlacerUseCase>();
            
            builder.RegisterEntryPoint<SidePanelInitializeUseCase>();
            builder.RegisterEntryPoint<AddGridGoldRewardUseCase>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputReaderService>().As<IInputReaderService>();
            builder.RegisterEntryPoint<SaveLoadService>().As<ISaveLoadService>();
            builder.RegisterEntryPoint<CurrencyService>();
            
            RegisterTimer<AccrueRemunerationDTO>(builder, _rewardDelay);
            RegisterTimer<SaveGameDTO>(builder, _autoSaveDelay);
            
            builder.RegisterInstance<IModelFactoryService, ModelFactoryService>(CreateModelFactory());
        }

        private void RegisterRepositories(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_cellViewPrefab);
            builder.RegisterInstance(_sidePanelView);
            builder.RegisterInstance(_currencyView);
            builder.RegisterInstance<ICurrencyRepository>(_currencyRepository);
            builder.RegisterInstance<ICameraSpeedRepository>(_cameraRepository);
            builder.RegisterInstance<ICameraZoomRepository>(_cameraRepository);
            builder.RegisterInstance<IGridRepository>(_gridRepository);
            builder.RegisterInstance(_gridView);
            
            builder.RegisterInstance<IGameplayBuildingsRepository>(
                new GameplayBindedBuildingsRepository(_buildingRepositories));
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

        private void RegisterTimer<TMessage>(IContainerBuilder builder, float delay) where TMessage : new()
        {
            builder.RegisterEntryPoint(resolver => 
                new TimerService<TMessage>(
                    delay,
                    resolver.Resolve<IPublisher<TMessage>>()), Lifetime.Singleton);
        }
    }
}