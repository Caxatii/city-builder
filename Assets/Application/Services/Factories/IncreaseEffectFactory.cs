using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Buildings.Effects;

namespace Application.Services.Factories
{
    public class IncreaseEffectFactory : IIncreaseEffectFactory
    {
        public IncreaseGoldEffect Create(IIncreaseGoldEffectRepository config)
        {
            return new IncreaseGoldEffect(config.Value);
        }
    }
}