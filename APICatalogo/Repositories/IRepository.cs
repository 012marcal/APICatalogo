namespace APICatalogo.Repositories
{
    public interface IRepository<T>
    {

        IEnumerable<T> GetAll();   
        T GetId(int id);
        T Create(T entity);
        T Update(T entity);
        T Delete(int id); 


    }
}
