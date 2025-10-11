using ContractsInterfaces.Installers;
using UnityEngine;
using VContainer;

namespace Infrastructure.Installers
{
    public class ViewsInstaller : InstallerCommand, IViewsInstaller
    {
        [SerializeField] private MonoBehaviour[] _views;
        
        public override void Configure(IContainerBuilder builder)
        {
            foreach (MonoBehaviour view in _views) 
                builder.RegisterInstance(view).AsImplementedInterfaces();
        }
    }
}