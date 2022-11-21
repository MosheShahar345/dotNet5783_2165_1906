using DO;

namespace DalApi
{
   internal interface IDal 
    {
        public Product Product { get; internal set;}
        public OrderItem OrderItem { get; internal set;}
        public Order Order {get; internal set;}
    }
}
