using DalApi;
using DO;
using System.Security.Principal;

namespace Dal;

sealed public class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public IOrder Order => new DalOrder();
}
