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
            "86th St Bridge", "145th St Bridge", "Abingdon Square", "Abraham Kazan St",
            "Abraham PI", "Absecon Rd", "Beach St", "Bear Rd", "Beaver St", "Beekman PI",
            "Bennett Ave", "Bond St", "Broad St", "Canal St", "Cannon St",
            "Canal St", "Carder Rd", "Cherry St", "Cliff St", "Cinton St"
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
       string[] names =
       {
           "Silk suit", "Kids suit", "Cotton pants", "Leather pants",
           "Shirts with cuffs", "Long shirts", "Silk tie", "Floral tie",
           "Silver cufflinks", "Diamond cunfflinks"
       };

        Enums.Category[] dd = {Enums.Category};
    
}
