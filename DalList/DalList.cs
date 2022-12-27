using DalApi;
namespace Dal;

internal sealed class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    public IProduct Product { get; }
    public IOrderItem OrderItem { get; }
    public IOrder Order { get; }

    private DalList()
    {
        Order = new DalOrder();
        OrderItem = new DalOrderItem();
        Product = new DalProduct();
    }
}
