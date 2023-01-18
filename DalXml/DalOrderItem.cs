namespace Dal;
using DalApi;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    const string s_orderitem = $"OrderItem";

    /// <summary>
    /// add a Order item to xml file
    /// </summary>
    /// <param name="orderitem"></param>
    /// <returns></returns>
    public int Add(DO.OrderItem orderitem)
    {
        orderitem.ID = XMLTools.LoadConfig().ToIntNullable("OrderItemID")!.Value + 1;
        XMLTools.SaveConfigXml("OrderItemID", orderitem.ID);
        List<DO.OrderItem?> orderitems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        orderitems.Add(orderitem);
        XMLTools.SaveListToXNLserializer(orderitems, s_orderitem);
        return orderitem.ID;
    }

    /// <summary>
    /// delete a order item from xml file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistsException"></exception>
    public void Delete(int id)
    {
        List<DO.OrderItem?> orderitems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        if (orderitems.RemoveAll(x => x?.ID == id) == 0)
            throw new DO.NotExistsException("not exist");

        XMLTools.SaveListToXNLserializer(orderitems, s_orderitem);
    }

    /// <summary>
    /// update a order item from xml file
    /// </summary>
    /// <param name="order"></param>
    public void Update(DO.OrderItem orderitem)
    {
        List<DO.OrderItem?> orderitems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        var o = (from item in orderitems
                 where (item?.ID == orderitem.ID)
                 select item).FirstOrDefault();

        if (o != null)
        {
            int i = orderitems.IndexOf(o);
            orderitems.Remove(o);
            orderitems.Insert(i, orderitem);
            XMLTools.SaveListToXNLserializer<DO.OrderItem>(orderitems, s_orderitem);
            return;
        }
        throw new DO.NotExistsException("not found order to update");
    }

    /// <summary>
    /// receives a order item by the id from the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistsException"></exception>
    public DO.OrderItem GetEntity(Func<DO.OrderItem?, bool>? func)
    {
        IEnumerable<DO.OrderItem?> orderitems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        var orderitem = orderitems.FirstOrDefault(func!);

        if (orderitem != null)
        {
            return (DO.OrderItem)orderitem;
        }
        throw new DO.NotExistsException("not exist");
    }

    /// <summary>
    /// returns all order items in the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func)
    {
        IEnumerable<DO.OrderItem?> orderitems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        if (func == null)
        {
            orderitems = from item in orderitems
                     select item;
            return orderitems;
        }
        else
        {
            orderitems = from item in orderitems
                     where (func!(item))
                     select item;
            return orderitems;
        }
    }
}

