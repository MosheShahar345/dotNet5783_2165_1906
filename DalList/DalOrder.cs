using DalApi;
using DO;
using System.Linq;

namespace Dal;
/// <summary>
/// A class with basic orders functions
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// Adding an order to the order array
    /// </summary>
    public int Add(Order order)
    {
        for (int i = 0;i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i].ID == order.ID)// A check uf the order is already exists
                throw new AlreadyExistsException("order already exist");
        }
        DataSource.Orders.Add(order);//if it does not exist, it is inserted into the array
        return order.ID;
    }
    /// <summary>
    /// deletes an existing order
    /// </summary>
    public void Delete(int id)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (id == DataSource.Orders[i].ID)//Checks if such a order exists according to ID
            {
                DataSource.Orders.RemoveAt(i);
                flag = true; // deleting successfully don't throw an exception
            }
        }
        if (!flag)
            throw new NotExistsException("order dose not exist");
    }
    /// <summary>
    /// updateing an existing order
    /// </summary>
    public void Update(Order order)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i].ID == order.ID)//Searching by id which order to update
            {
                DataSource.Orders[i] = order;
                flag = true;
            }
        }
        if (!flag)
            throw new NotExistsException("order dose not exist");
    }
    /// <summary>
    /// receives a id and returns his order
    /// </summary>
    public Order GetById(int orderID)
    {
        foreach (var item in DataSource.Orders)
        {
            if (item.ID == orderID)
                return item;
        }
        throw new NotExistsException("order dose not exist");//Throws an exception if the order does not exist
    }
    /// <summary>
    /// returns the array of orders
    /// </summary>
    public IEnumerable<Order> GetAll()
    {
        List<Order> orders = new List<Order>();
        foreach (var item in DataSource.Orders)
        {
            orders.Add(item);
        }
        return orders;
    }
}
