namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class DalOrder : IOrder
{
    const string s_Order = $"Order";

    #region Add Order
    /// <summary>
    /// add a order to xml file
    /// </summary>
    ///

    public int Add(Order order)
    {
        List<Order?> orders = XMLTools.LoadListFromXMLSerializer<Order>(s_Order);

        if (orders.FirstOrDefault(it => it?.ID == order.ID) != null)
            throw new Exception("order olready exist");

        orders.Add(order);

        XMLTools.SaveListToXMLSerializer(orders, s_Order);

        return order.ID;
    }





}
