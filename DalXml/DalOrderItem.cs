namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem item)
    {
        throw new NotImplementedException();
    }

    public void Update(OrderItem item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public OrderItem? GetEntity(Func<OrderItem?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        throw new NotImplementedException();
    }
}
