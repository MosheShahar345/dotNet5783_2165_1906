using DO;

namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        public OrderItem? GetByIds(int orderId , int productId);
        public List<OrderItem?>? GetOrderItems(int orderId);
    }
}
