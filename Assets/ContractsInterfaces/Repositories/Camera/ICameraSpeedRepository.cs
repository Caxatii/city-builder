namespace ContractsInterfaces.Repositories
{
    public interface ICameraSpeedRepository : IRepository
    {
        public float MinSpeed { get; }
        public float MaxSpeed { get; }
    }
}