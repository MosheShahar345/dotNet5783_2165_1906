using BO;
namespace BlApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList> GetOrderForList();
    public Order GetOrder(int orderId);
    public Order UpdateOrderShipping(int orderId, DateTime shippingDate);
    public Order UpdateOrderDelivery(int orderId, DateTime deliveryDate);
    public OrderTracking TrackOrder(int orderId);
}


