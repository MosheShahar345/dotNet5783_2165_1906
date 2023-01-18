namespace Dal;
using DalApi;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;

internal class DalOrder : IOrder
{
    const string s_Order = $"Order";

    /// <summary>
    /// add a order to xml file
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>


    public int Add(DO.Order order)
    {
        order.ID = XMLTools.LoadConfig().ToIntNullable("OrderID")!.Value + 1;
        XMLTools.SaveConfigXml("OrderID", order.ID);
        List<DO.Order?> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        orders.Add(order);
        XMLTools.SaveListToXNLserializer(orders, s_Order);
        return order.ID;
    }

    /// <summary>
    /// delete a order from xml file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistsException"></exception>
    public void Delete(int id)
    {
        List<DO.Order?> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        if (orders.RemoveAll(x => x?.ID == id) == 0)
            throw new DO.NotExistsException("not exist");

        XMLTools.SaveListToXNLserializer(orders, s_Order);
    }

    /// <summary>
    /// update a order from xml file
    /// </summary>
    /// <param name="order"></param>

    public void Update(DO.Order order)
    {
        List<DO.Order?> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        var o = (from item in orders
                 where (item?.ID == order.ID)
                 select item).FirstOrDefault();

        if (o != null)
        {
            int i = orders.IndexOf(o);
            orders.Remove(o);
            orders.Insert(i, order);
            XMLTools.SaveListToXNLserializer<DO.Order>(orders, s_Order);
            return;
        }
        throw new DO.NotExistsException("not found order to update");
    }

    /// <summary>
    /// receives a order by the id from the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistsException"></exception>
    public DO.Order? GetEntity(Func<DO.Order?, bool>? func)
    {
        IEnumerable<DO.Order?> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        var order = orders.FirstOrDefault(func!);

        if (order != null)
        {
            return (DO.Order)order;
        }
        throw new DO.NotExistsException("not exist");
    }

    /// <summary>
    /// returns all orders in the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func)
    {
        IEnumerable<DO.Order?> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        if (func == null)
        {
            orders = from item in orders
                     select item;
            return orders;
        }
        else
        {
            orders = from item in orders
                     where (func!(item))
                     select item;
            return orders;
        }
    }
}

