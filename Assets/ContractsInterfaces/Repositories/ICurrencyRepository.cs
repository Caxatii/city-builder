namespace ContractsInterfaces.Repositories
{
    public interface ICurrencyRepository : IRepository
    {
        public int StartValue { get; }
    }
}