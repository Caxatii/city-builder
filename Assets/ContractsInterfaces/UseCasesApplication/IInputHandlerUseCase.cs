using Domain.Gameplay.MessagesDTO;
using MessagePipe;

namespace ContractsInterfaces.UseCasesApplication
{
    public interface ICameraMoveUseCase : IUseCase, IMessageHandler<RawMoveInputDTO> { }
}