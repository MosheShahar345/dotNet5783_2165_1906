namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class DalOrder : IOrder
{
    const string s_Order = $"Order";

  
    /// <summary>
    /// add a order to xml file
    /// </summary>
    ///

    public int Add(Order order)
    {
        List<Order?> orders = XMLTools.LoadListFromXMLSerializer<Order>(s_Order);

        if (orders.FirstOrDefault(it => it?.ID == order.ID) != null)
            throw new Exception("order already exist");

        orders.Add(order);

        XMLTools.SaveListToXMLSerializer(orders, s_Order);

        return order.ID;
    }

    public void Update(Order item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Order? GetEntity(Func<Order?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        throw new NotImplementedException();
    }
}
