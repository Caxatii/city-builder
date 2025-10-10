using System.Collections.Generic;
using Application.Services.Factories;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using Newtonsoft.Json;
using UnityEngine;
using VContainer;

namespace Application.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string SaveKey = "DATA";

        [Inject] private IModelFactoryService _factoryService;
        
        private Dictionary<object, object> _data;
        
        public TResult Load<TResult, TConfig>(object key, TConfig config) where TConfig : IRepository
        {
            if (_data.TryGetValue(key, out var value))
                return (TResult)value;

            TResult newModel = _factoryService.Create<TResult, TConfig>(config);
            _data.Add(key, newModel);
            
            return newModel;
        }

        public void Save()
        {
            string serializedData = JsonConvert.SerializeObject(_data);
            PlayerPrefs.SetString(SaveKey, serializedData);
        }

        public void Initialize()
        {
            _data = JsonConvert.DeserializeObject<Dictionary<object, object>>(PlayerPrefs.GetString(SaveKey)) ?? 
                    new Dictionary<object, object>();
        }

        public void Dispose()
        {
            Save();
        }
    }
}