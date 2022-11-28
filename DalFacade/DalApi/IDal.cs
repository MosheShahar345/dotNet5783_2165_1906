using DO;

namespace DalApi
{
   public interface IDal 
    {
        public IProduct Product { get; internal set;}
        public IOrderItem OrderItem { get; internal set;}
        public IOrder Order {get; internal set;}
    }
}
