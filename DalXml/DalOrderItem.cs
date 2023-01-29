using DO;

namespace Dal;
using DalApi;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    const string s_orderitem = $"OrderItem";

    [MethodImpl(MethodImplOptions.Synchronized)]
    static DO.OrderItem? CreateOrderItemFromXElement(XElement element)
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem orderitem)
    {
        orderitem.ID = XMLTools.LoadConfig().ToIntNullable("OrderItemID")!.Value + 1;
        XMLTools.SaveConfigXml("OrderItemID", orderitem.ID);
        XElement root = XMLTools.LoadListToXMLElement(s_orderitem);
        XElement orderItem = new XElement("orderItem",
                             new XElement("ID", orderitem.ID),
                             new XElement("ProductID", orderitem.ProductID),
                             new XElement("OrderID", orderitem.OrderID),
                             new XElement("ProductID", orderitem.ProductID),
                             new XElement("Price", orderitem.Price),
                             new XElement("Amount", orderitem.Amount));

        root.Add(orderItem);
        XMLTools.SaveListToXMLElement(root, s_orderitem);
        return orderitem.ID;
    }

    /// <summary>
    /// delete a order item from xml file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement root = XMLTools.LoadListToXMLElement(s_orderitem);
        XElement? orderItem = (from item in root.Elements()
                               where item.ToIntNullable("ID") == id
                               select item).FirstOrDefault() ?? throw new DO.NotExistsException("Not found a order item to delete");
        orderItem?.Remove();
        XMLTools.SaveListToXMLElement(root, s_orderitem);
    }

    /// <summary>
    /// update a order item from xml file
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="DO.NotExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem orderItem)
    { 
        Delete(orderItem.ID);
        Add(orderItem);
    }

    /// <summary>
    /// receives a order item by the id from the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func)
    {
        XElement? root = XMLTools.LoadListToXMLElement(s_orderitem);
        return func != null
            ? from item in root.Elements()
            let tempItem = CreateOrderItemFromXElement(item)
            where func(tempItem)
            select (DO.OrderItem?)tempItem
            : from item in root.Elements()
            select CreateOrderItemFromXElement(item);
    }
}

