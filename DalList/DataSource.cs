//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Runtime.CompilerServices;
using DO;

namespace Dal;

internal static class DataSource
{
    internal class Config
    {
        internal static int SizeOfProduct = 0;
        internal static int SizeOfOrder = 0;
        internal static int SizeOfOrderItem = 0;
        private static int MinNum = 100000;
        private static int MaxNum = 999999;
        Random Date = new Random(DateTime.Now.Millisecond);
        static Random Rand = new Random();
        internal static int Num = Rand.Next(MinNum, MaxNum);
    }

    static readonly Random Rand = new Random();
    internal static Product[] MyProduct = new Product[50];
    internal static Order[] MyOrder = new Order[100];
    internal static OrderItem[] MyOrderItem = new OrderItem[200];

    private static void AddProduct(Product product)
    {
        product.ID = Config.Num;
        
        MyProduct[Config.SizeOfProduct++] = product;
    }

    private static void AddOrder(Order order)
    {
        for (int i = 0; i < 10; i++)
        {
            order.ID = Config.Num;

            MyOrder[Config.SizeOfOrder++] = order;
        }
    }

    private static void AddOrderItem(OrderItem orderItem)
    {
        MyOrderItem[Config.SizeOfOrderItem++] = orderItem;
    }

    private static void s_Initialize()
    {
        Product product = new Product()
        {

        };
        AddProduct(product);

        Order order = new Order()
        {

        };
        AddOrder(order);

        OrderItem orderItem = new OrderItem()
        {

        };
        AddOrderItem(orderItem);
    }
}