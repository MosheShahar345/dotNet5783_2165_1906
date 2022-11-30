using BO;
namespace BlApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList> GetOrderForList();
    public Order GetOrder(int orderId);
    public Order UpdateOrderShipping(int orderId);
    public Order UpdateOrderDelivery(int orderId);
    public OrderTracking TrackOrder(int orderId);
}


