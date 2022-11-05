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
    }

    static readonly Random Rand = new Random();
    internal static Product[] MyProduct = new Product[50];
    internal static Order[] MyOrder = new Order[100];
    internal static OrderItem[] MyOrderItem = new OrderItem[200];

    private static void AddProduct(Product product)
    {
        MyProduct[Config.SizeOfProduct++] = product;
    }

    private static void AddOrder(Order order)
    {
        MyOrder[Config.SizeOfOrder++] = order;
    }

    private static void AddOrderItem(OrderItem orderItem)
    {
        MyOrderItem[Config.SizeOfOrderItem++] = orderItem;
    }

    private static void s_Initialize()
    {

    }
}