using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings.Effects;
using Domain.Gameplay.Models.Currency;
using Domain.Gameplay.Models.Grid;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class AddGridGoldRewardUseCase : IUseCase, IPostInitializable, IMessageHandler<AccrueRemunerationDTO>
    {
        [Inject] private IGridRepository _gridRepository;
        [Inject] private ICurrencyRepository _currencyRepository;
        [Inject] private ISaveLoadService _saveLoadService;

        [Inject] private ISubscriber<AccrueRemunerationDTO> _subscriber;
        
        private GridModel _gridModel;
        private CurrencyModel _currencyModel;

        public void Initialize()
        {
            _subscriber.Subscribe(this);
        }

        public void PostInitialize()
        {
            _gridModel = _saveLoadService.Load<GridModel, IGridRepository>(nameof(GridModel));

            _currencyModel = _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(nameof(CurrencyType.Gold));
        }

        public void Handle(AccrueRemunerationDTO message)
        {
            foreach (var cell in _gridModel.EnumerateBuildings())
                if (cell.building.Effect is IncreaseGoldEffect effect) 
                    _currencyModel.Increase(effect.Value);
        }

        public void Dispose() { }
    }
}