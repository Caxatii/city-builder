namespace ContractsInterfaces.Repositories
{
    public interface ICameraZoomRepository : IRepository
    {
        public float CurrentProjectionSize { get; }

        public float MaxProjectionSize { get; }

        public float MinProjectionSize { get; }
    }
}