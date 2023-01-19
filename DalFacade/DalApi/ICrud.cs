namespace DalApi;

public interface ICrud<T> where T : struct
{
    public int Add(T item);
    public void Update(T orderItem);
    public void Delete(int id);
    public T? GetEntity(Func<T?, bool>? func);
    public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);
}