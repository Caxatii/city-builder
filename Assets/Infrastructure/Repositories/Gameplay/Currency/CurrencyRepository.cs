using System.Drawing;
using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Infrastructure.Repositories.Gameplay.Currency
{
    [CreateAssetMenu(fileName = "CurrencyRepository", 
        menuName = "Gameplay/Currency Repository")]
    public class CurrencyRepository : ScriptableObject, ICurrencyRepository
    {
        [SerializeField] private int _startValue;
        
        public int StartValue => _startValue;
    }
}