using Application.Services;
using ContractsInterfaces.ServicesApplication;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class InputServiceInstaller : InstallerCommand
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputReaderService>().As<IInputReaderService>();
        }
    }
}