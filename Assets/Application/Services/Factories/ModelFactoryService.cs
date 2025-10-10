using System;
using System.Collections.Generic;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;

namespace Application.Services.Factories
{
    public class ModelFactoryService : IModelFactoryService
    {
        private Dictionary<Type, IModelFactory> _factories = new();

        public void Add<TConfig, TFactory>(TFactory factory) 
            where TFactory : IModelFactory<TConfig> 
            where TConfig : IRepository
        {
            _factories[typeof(TConfig)] = factory;
        }
        
        public TResult Create<TResult, TConfig>(TConfig config) where TConfig : IRepository
        {
            return (TResult)_factories[typeof(TConfig)].Create(config);
        }

        public TResult Create<TResult>() where TResult : new()
        {
            return new TResult();
        }
    }
}