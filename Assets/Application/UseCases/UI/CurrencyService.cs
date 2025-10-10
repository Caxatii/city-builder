using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using Domain.Gameplay.Models.Currency;
using Presentation.Gameplay.Views.UI;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.UI
{
    public class CurrencyService : IService, IPostInitializable
    {
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private CurrencyView _view;

        private CurrencyModel _currencyModel;

        public void Initialize()
        {
            _view.Initialize();
        }

        public void PostInitialize()
        {
            _currencyModel = _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(nameof(CurrencyType.Gold));

            _currencyModel.Changed += OnChanged;
            _view.Text = _currencyModel.Value.ToString();
        }

        private void OnChanged(int value)
        {
            _view.Text = value.ToString();
        }

        public void Dispose()
        {
            _currencyModel.Changed -= OnChanged;
        }
    }
}