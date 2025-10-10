using System.Collections.Generic;
using Application.Services;
using Application.Services.Factories;
using Application.UseCases.Camera;
using Application.UseCases.Grid;
using Application.UseCases.UI;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Infrastructure.Repositories.Application.Camera;
using Infrastructure.Repositories.Gameplay.Buildings;
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
        [SerializeField, Required] private CellView _cellViewPrefab;
        [SerializeField, Required] private CameraView _camera;
        [SerializeField, Required] private SidePanelView _sidePanelView;

        [SerializeField, Required] private CameraRepository _cameraRepository;
        [SerializeField, Required] private GridView _gridView;
        [SerializeField, Required] private GridRepository _gridRepository;

        [SerializeField] private GameplayBuildingsRepository _buildingRepositories;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
            
            RegisterRepositories(builder);
            RegisterServices(builder);
            
            builder.RegisterEntryPoint<CameraMoveUseCase>().As<ICameraMoveUseCase>();
            builder.RegisterEntryPoint<CameraZoomUseCase>();
            builder.RegisterEntryPoint<GridInitializeUseCase>();
            builder.RegisterEntryPoint<GridSelectionUseCase>();
            builder.RegisterEntryPoint<SidePanelInitializeUseCase>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<InputReaderService>().As<IInputReaderService>();
                pointsBuilder.Add<SaveLoadService>().As<ISaveLoadService>();
            });
            
            builder.RegisterInstance<IModelFactoryService, ModelFactoryService>(CreateModelFactory());
        }

        private void RegisterRepositories(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_cellViewPrefab);
            builder.RegisterInstance(_sidePanelView);
            builder.RegisterInstance<IGameplayBuildingsRepository>(_buildingRepositories);
            builder.RegisterInstance<ICameraSpeedRepository>(_cameraRepository);
            builder.RegisterInstance<ICameraZoomRepository>(_cameraRepository);
            builder.RegisterInstance<IGridRepository>(_gridRepository);
            builder.RegisterInstance(_gridView);
        }

        private IModelFactoryService CreateModelFactory()
        {
            IModelFactoryService factoryService = new ModelFactoryService();
            
            factoryService.Add<IBuildingRepository, IBuildingFactory>(CreateBuildingFactory());
            factoryService.Add<IGridRepository, IGridFactory>(new GridFactory());
            factoryService.Add<IIncreaseGoldEffectRepository, IIncreaseEffectFactory>(new IncreaseEffectFactory());
            factoryService.Add<ICameraSpeedRepository, ICameraSpeedFactory>(new CameraSpeedFactory());
            factoryService.Add<ICameraZoomRepository, ICameraZoomFactory>(new CameraZoomFactory());

            return factoryService;
        }

        private IBuildingFactory CreateBuildingFactory()
        {
            BuildingFactory factory = new BuildingFactory();

            factory.Add<IIncreaseEffectFactory>(new IncreaseEffectFactory());
            
            return factory;
        }
    }
}