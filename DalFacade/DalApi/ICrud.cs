using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{
    public int Add(T item);
    public void Update(T item);
    public void Delete(int id);
    public T? GetById(int id);
   // public T? Get(Func<T?, bool> func);
    public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);
}