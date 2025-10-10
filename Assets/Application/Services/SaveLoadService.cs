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

        [Inject] private IModelFactoryService _factoryService;
        [Inject] private ISubscriber<SaveGameDTO> _subscriber;

        private JsonSerializerSettings _serializerSettings;
        private Dictionary<string, object> _data;

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

        public TResult Load<TResult, TConfig>(string key, TConfig config) where TConfig : IRepository
        {
            if (_data.TryGetValue(key, out var value))
                return (TResult)value;

            TResult newModel = _factoryService.Create<TResult, TConfig>(config);
            _data.Add(key, newModel);
            
            return newModel;
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