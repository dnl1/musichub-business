namespace MusicHubBusiness.Repository
{
    public interface IRepository<T>
    {
        T Create(T entity);
    }
}