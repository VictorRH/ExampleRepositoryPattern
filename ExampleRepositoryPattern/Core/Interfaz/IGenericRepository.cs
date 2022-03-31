namespace ExampleRepositoryPattern.Core.Interfaz
{
    public interface IGenericRepository<T> where T : ClassBase
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
    }
}
