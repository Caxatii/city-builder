using System.ComponentModel.DataAnnotations;
using Application.UseCases.UI;
using ContractsInterfaces.Installers;
using Presentation.Gameplay.Views.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    [RequireComponent(typeof(ISaveLoadInstaller))]
    public class CurrencyInstaller : InstallerCommand
    {
        [SerializeField, Required] private CurrencyView _currencyView;
        
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_currencyView);

            builder.RegisterEntryPoint<CurrencyService>();
        }
    }
}