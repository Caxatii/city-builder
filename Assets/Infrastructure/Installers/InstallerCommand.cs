using UnityEngine;
using VContainer;

namespace Infrastructure.Installers
{
    public abstract class InstallerCommand : MonoBehaviour
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}