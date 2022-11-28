using BO;
namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrderForList();
    public IOrder GetOrder(int orderId);
    public IOrder UpdateOrderShipping(int orderId, DateTime shippingDate);
    public IOrder UpdateOrderDelivery(int orderId, DateTime deliveryDate);
    public OrderTracking TrackOrder(int orderId);

}


