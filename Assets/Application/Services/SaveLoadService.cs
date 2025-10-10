using System;
using System.Collections.Generic;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Newtonsoft.Json;
using UnityEngine;
using VContainer;

namespace Application.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string SaveKey = "DATA";

        private IModelFactoryService _factoryService;
        private ISubscriber<SaveGameDTO> _subscriber;

        private JsonSerializerSettings _serializerSettings;
        
        private Dictionary<Type, IRepository> _defaultRepositories = new();
        private Dictionary<string, object> _data;

        public SaveLoadService(IModelFactoryService factoryService, ISubscriber<SaveGameDTO> subscriber)
        {
            _factoryService = factoryService;
            _subscriber = subscriber;
        }

        public void Initialize()
        {
            _subscriber.Subscribe(this);
            
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            
            _data = JsonConvert.DeserializeObject<Dictionary<string, object>>(PlayerPrefs.GetString(SaveKey), _serializerSettings) ?? 
                    new Dictionary<string, object>();
        }

        public TResult Load<TResult, TConfig>(TConfig config, string key) where TConfig : IRepository
        {
            key ??= typeof(TResult).Name;
            
            if (_data.TryGetValue(key, out object value))
                return (TResult)value;

            TResult newModel = _factoryService.Create<TResult, TConfig>(config);
            _data.Add(key, newModel);
            
            return newModel;
        }

        public TResult Load<TResult, TConfig>(string key) where TConfig : IRepository
        {
            return Load<TResult, TConfig>((TConfig)_defaultRepositories[typeof(TConfig)], key);
        }

        public TResult Load<TResult>(string key) where TResult : new()
        {
            key ??= typeof(TResult).Name;
            
            if (_data.TryGetValue(key, out object value))
                return (TResult)value;

            TResult newModel = _factoryService.Create<TResult>();
            _data.Add(key, newModel);
            
            return newModel;
        }

        public void AddConfig<TConfig>(TConfig configSample) where TConfig : IRepository
        {
            _defaultRepositories.Add(typeof(TConfig), configSample);
        }

        public void Save()
        {
            string serializedData = JsonConvert.SerializeObject(_data, Formatting.Indented, _serializerSettings);
            
            PlayerPrefs.SetString(SaveKey, serializedData);
        }

        public void Handle(SaveGameDTO message) => 
            Save();

        public void Dispose()
        {
            Save();
        }
    }
}