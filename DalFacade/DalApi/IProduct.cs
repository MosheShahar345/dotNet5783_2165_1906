using DO;

namespace DalApi
{
    public interface IProduct : ICrud<Product>
    {
        void Add(global::BO.Product product);
        void Add(global::BO.Product product);
    }
}
