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
        internal static int MinNum = 100000;
        internal static int MaxNum = 999999;
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

    private static void CreatOrder(Order order)
    {
        string[] Names =
        {
            "Moshe", "Aharon", "Sara", "Caroline", "John", "Laura", "Michael",
            "Lily", "Julia", "Ben", "Grace", "James", "Nick", "Ross", "Monica",
            "Rachel", "Joey", "Chandler", "Phoebe", "Luca"
        };

        string[] Emails =
        {
            "Moshe@.com", "Aharon@.com", "Sara@.com", "Caroline@.com", "John@.com",
            "Laura@.com", "Michael@.com", "Lily@.com", "Julia@.com", "Ben@.com",
            "Grace@.com", "James@.com", "Nick@.com", "Ross@.com", "Monica@.com",
            "Rachel@.com", "Joey@.com", "Chandler@.com", "Phoebe@.com", "Luca@.com"
        };

        string[] Adresses =
        {

        };

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
    private static void CreateProducts()
    {
        string[] Names =
        {
            "Silk suit", "kids suit", "Cotton pants", "Leather pants",
            "shirts with cuffs", "Long shirts", "Silk tie", "Floral tie",
            "Silver cufflinks", "Diamond cunfflinks"
        };

        Enums.Category[] Categories =
        {
            Enums.Category.Suit,Enums.Category.Suit,Enums.Category.Pants,
            Enums.Category.Pants,Enums.Category.Shirts,Enums.Category.Shirts,
            Enums.Category.Ties,Enums.Category.Ties,Enums.Category.Cufflinks,Enums.Category.Cufflinks
        };
        double[] Price = { 800, 450, 220, 300, 120, 115, 80, 60, 180, 350 };

        int[] InStock = { 10, 15, 28, 12, 13, 18, 21, 22, 23, 24 };
    }
}
