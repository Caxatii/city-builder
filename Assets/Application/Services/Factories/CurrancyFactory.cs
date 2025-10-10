using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Currency;

namespace Application.Services.Factories
{
    public class CurrencyFactory : ICurrencyFactory
    {
        public CurrencyModel Create(ICurrencyRepository config)
        {
            return new CurrencyModel(config.StartValue);
        }
    }
}