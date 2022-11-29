using DalApi;
namespace Dal;

public sealed class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public IOrder Order => new DalOrder();
}
