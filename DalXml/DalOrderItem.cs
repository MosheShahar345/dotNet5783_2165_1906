using DO;

namespace Dal;
using DalApi;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    const string s_orderitem = $"OrderItem";

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
    public void Update(DO.OrderItem orderItem)
    {
        //List<DO.OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderitem);
        //var o = (from item in orderItems
        //         where (item?.ID == orderItem.ID)
        //         select item).FirstOrDefault();

        //if (o != null)
        //{
        //    int i = orderItems.IndexOf(o);
        //    orderItems.Remove(o);
        //    orderItems.Insert(i, orderItem);
        //    XMLTools.SaveListToXNLserializer<DO.OrderItem>(orderItems, s_orderitem);
        //    return;
        //}
        //throw new DO.NotExistsException("not found order to update");
       Delete(orderItem.ID);
       Add(orderItem);
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
        XElement? root = XMLTools.LoadListToXMLElement(s_orderitem);
        if (func != null)
        {
            return from item in root.Elements()
                   let tempItem = CreateOrderItemFromXElement(item)
                   where func(tempItem)
                   select (DO.OrderItem?)tempItem;
        }
        else
        {
            return from item in root.Elements()
                   select CreateOrderItemFromXElement(item);
        }
    }
}

