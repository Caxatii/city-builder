using System.Collections.Generic;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class GameplayInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();

            HashSet<InstallerCommand> installers = new ();
            
            foreach (InstallerCommand command in GetComponents<InstallerCommand>()) 
                installers.Add(command);
            
            foreach (InstallerCommand command in GetComponentsInChildren<InstallerCommand>()) 
                installers.Add(command);
            
            foreach (InstallerCommand command in installers) 
                command.Configure(builder);
        }
    }
}