using ContractsInterfaces.ServicesApplication;

namespace ContractsInterfaces.Installers
{
    public interface ISaveLoadConfigurator
    {
        public void Configure(ISaveLoadService service);
    }
}