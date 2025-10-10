using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using Domain.Gameplay.Models.Currency;
using R3;
using VContainer.Unity;

namespace Application.UseCases.UI
{
    public class CurrencyService : IService, IPostInitializable
    {
        private ICurrencyRepository _currencyRepository;
        
        private ISaveLoadService _saveLoadService;
        private CurrencyModel _currencyModel;
        
        public void Initialize()
        {
            
        }

        public void PostInitialize()
        {
            _currencyModel =
                _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(CurrencyType.Gold, _currencyRepository);

            _currencyModel.Changed += OnChanged;
        }

        private void OnChanged(int obj)
        {
            
        }

        public void Dispose() { }
    }
}