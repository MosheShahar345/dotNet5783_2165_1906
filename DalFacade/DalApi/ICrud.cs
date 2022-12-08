using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{
    int? Add(T? item);
    void Update(T? item);
    void Delete(int id);
    T? GetById(int? id);
    IEnumerable<T?> GetAll(Func<T?, bool>? func);
}