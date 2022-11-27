using DO;

namespace DalApi
{
   public interface ICrud<T> where T : struct
    {
        int Add(T item);
        void Update(T item);
        void Delete(int ID);
        T GetById(int id);
        IEnumerable<T>? GetAll();
    }
}
