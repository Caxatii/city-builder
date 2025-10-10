using System.Threading;
using ContractsInterfaces.ServicesApplication;
using Cysharp.Threading.Tasks;
using MessagePipe;

namespace Application.Services
{
    public class TimerService<TMessage> : IService where TMessage : new()
    {
        private IPublisher<TMessage> _publisher;

        private float _delay;
        private CancellationTokenSource _token = new();

        public TimerService(float delay, IPublisher<TMessage> publisher)
        {
            _delay = delay;
            _publisher = publisher;
        }
        
        public void Initialize()
        {
            StartCounter().Forget();
        }

        private async UniTaskVoid StartCounter()
        {
            while (_token.IsCancellationRequested == false)
            {
                await UniTask.WaitForSeconds(_delay, cancellationToken: _token.Token);
                _publisher.Publish(new TMessage());
            }
        }
        
        public void Dispose()
        {
            _token.Cancel();
        }
    }
}