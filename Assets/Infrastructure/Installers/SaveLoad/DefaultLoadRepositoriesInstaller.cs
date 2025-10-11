using System;
using System.Reflection;
using Application.Core.Attributes;
using ContractsInterfaces.Installers;
using ContractsInterfaces.ServicesApplication;
using Infrastructure.Repositories;
using UnityEngine;

namespace Infrastructure.Installers
{
    public class DefaultLoadRepositoriesConfigurator : MonoBehaviour, ISaveLoadConfigurator
    {
        [SerializeField] private RepositoryBase[] _repositories;
        
        public void Configure(ISaveLoadService service)
        {
            foreach (RepositoryBase repository in _repositories)
            {
                var attribute = repository.GetType().GetCustomAttribute<RepositoryTypeAttribute>();

                if (attribute == null)
                    throw new Exception($"{repository.GetType().Name} has not RepositoryType attribute");
                
                foreach (Type type in attribute.Types) 
                    service.AddConfig(repository, type);
            }
        }
    }
}