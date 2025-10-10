using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Buildings.Effects;

namespace ContractsInterfaces.FactoriesApplication
{
    public interface IIncreaseEffectFactory : IModelFactory<IncreaseGoldEffect, IIncreaseGoldEffectRepository> { }
}