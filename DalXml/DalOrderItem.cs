using DO;

namespace Dal;
using DalApi;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    const string s_orderitem = $"OrderItem";

    public DO.OrderItem? CreateOrderItemFromXElement(XElement element)
    {
        return new OrderItem
        {
            ID = element.ToIntNullable("ID") ?? throw new FormatException("Id"),
            ProductID = element.ToIntNullable("ProductID") ?? throw new FormatException("Product Id"),
            OrderID = element.ToIntNullable("OrderID") ?? throw new FormatException("Order Id"),
            Price = element.ToDoubleNullable("Price") ?? throw new FormatException("Price"),
            Amount = element.ToIntNullable("Amount") ?? throw new FormatException("Amount"),
        };
    }
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
    /// <param name="orderItem"></param>
    /// <exception cref="DO.NotExistsException"></exception>
    public void Update(DO.OrderItem orderItem)
    {
        List<DO.OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        var o = (from item in orderItems
                 where (item?.ID == orderItem.ID)
                 select item).FirstOrDefault();

        if (o != null)
        {
            int i = orderItems.IndexOf(o);
            orderItems.Remove(o);
            orderItems.Insert(i, orderItem);
            XMLTools.SaveListToXNLserializer<DO.OrderItem>(orderItems, s_orderitem);
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
    public DO.OrderItem? GetEntity(Func<DO.OrderItem?, bool>? func)
    {
        XElement? element = XMLTools.LoadListToXMLElement(s_orderitem);

        return (from item in element.Elements()
            let dOrderItem = CreateOrderItemFromXElement(item)
            where func!(dOrderItem)
            select (DO.OrderItem?)dOrderItem).FirstOrDefault() ?? throw new DO.NotExistsException("Does not exist");
    }

    /// <summary>
    /// returns all order items in the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func)
    {
        IEnumerable<DO.OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        if (func == null)
        {
            orderItems = from item in orderItems
                     select item;
            return orderItems;
        }
        else
        {
            orderItems = from item in orderItems
                     where (func!(item))
                     select item;
            return orderItems;
        }
    }
}

