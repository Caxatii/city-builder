using ContractsInterfaces.FactoriesApplication;
using Domain.Gameplay.Models.Currency;

namespace ContractsInterfaces.Repositories
{
    public interface ICurrencyFactory : IModelFactory<CurrencyModel, ICurrencyRepository> { }
}