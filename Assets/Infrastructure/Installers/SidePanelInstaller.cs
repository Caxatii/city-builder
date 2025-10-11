using Application.UseCases.UI;
using ContractsInterfaces.Installers;
using Presentation.Gameplay.Views.UI;
using TriInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    [RequireComponent(typeof(IRepositoriesInstaller))]
    public class SidePanelInstaller : InstallerCommand
    {
        [SerializeField, Required] private SidePanelView _sidePanelView;
        
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sidePanelView);
            
            builder.RegisterEntryPoint<SidePanelInitializeUseCase>();
        }
    }
}