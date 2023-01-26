using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// calls the order from DO and builds BO entity
    /// <summary>
    /// gets order 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Order GetOrder(int orderId);

    /// <summary>
    /// gets a list of orders from DO to BO
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetOrderForList(Func<BO.OrderForList?, bool>? func = null);

    /// <summary>
    /// updates the shipping date of an order 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Order UpdateOrderShipping(int orderId);

    /// <summary>
    /// updates the delivery date of an order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Order UpdateOrderDelivery(int orderId);

    /// <summary>
    /// a function for admin to track order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public OrderTracking TrackOrder(int orderId);

    /// <summary>
    /// returns the oldest order's id
    /// </summary>
    /// <returns></returns>
    public int? OldestOrder();
}


