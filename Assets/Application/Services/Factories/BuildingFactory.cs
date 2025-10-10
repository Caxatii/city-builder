using System;
using System.Collections.Generic;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Buildings.Effects;

namespace Application.Services.Factories
{
    public class BuildingFactory : IBuildingFactory
    {
        private Dictionary<Type, IModelFactory> _effectsFactories = new();

        public void Add<T>(IModelFactory factory)
        {
            _effectsFactories[typeof(T)] = factory;
        }
        
        public Building Create(IBuildingRepository config)
        {
            return new Building(config.Level, config.Name, CreateEffect(config));
        }

        private EffectBase CreateEffect(IBuildingRepository config)
        {
            return (EffectBase)_effectsFactories[config.Effect.GetType()].Create(config.Effect);
        }
    }
}