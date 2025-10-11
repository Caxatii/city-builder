using Application.Core.Attributes;
using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Infrastructure.Repositories.Gameplay.Currency
{
    [RepositoryType(typeof(ICurrencyRepository))]
    [CreateAssetMenu(fileName = "CurrencyRepository", 
        menuName = "Gameplay/Currency Repository")]
    public class CurrencyRepository : RepositoryBase, ICurrencyRepository
    {
        [SerializeField] private int _startValue;
        
        public int StartValue => _startValue;
    }
}