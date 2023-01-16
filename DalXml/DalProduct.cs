namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class DalProduct : IProduct
{
    public int Add(Product item)
    {
        throw new NotImplementedException();
    }

    public void Update(Product item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Product? GetEntity(Func<Product?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
    {
        throw new NotImplementedException();
    }
}
