//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Dal;
using System.Runtime.CompilerServices;
using DO;

namespace Dal;

internal static class DataSource
{
    /// <summary>
    ///  Initializes all entities in order
    /// </summary>
    static DataSource()
    {
        s_Initialize(); 
    }
    internal class Config
    {
        internal static int SizeOfProduct = 0;
        internal static int SizeOfOrder = 0;
        internal static int SizeOfOrderItem = 0;
        private static int IdOfOrder = 0;//Resets the order code to 0
        private static int IdOfOrderItem = 0;//Resets the order item code to 0

        /// <summary>
        ///  Increases the order code by one
        /// </summary>
        public static int GetOrderID()
        {
            return IdOfOrder++;
        }
        /// <summary>
        ///  Increases the order item code by one
        /// </summary>
        public static int GetOrderItemID()
        {
            return IdOfOrderItem++;
        }

        //static Random Rand = new(DateTime.Now.Millisecond);
        //internal static int Num = Rand.Next(100000, 999999);
    }

    static readonly Random Rand = new(DateTime.Now.Millisecond);
    internal static Product[] Products = new Product[50];
    internal static Order[] Orders = new Order[100];
    internal static OrderItem[] OrderItems = new OrderItem[200];

    /// <summary>
    ///  Order creator
    /// </summary>
    private static void CreateOrders()
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

        for(int i = 0; i < 20; i++)//A loop that runs 20 times and fills in all the details of the customer
        {
            Order order = new Order()
            {
                ID = Config.GetOrderID(),
                CustomerEmail = Emails[i],
                CustomerName = Names[i],
                CustomerAdress = Adresses[i],
                OrderDate = DateTime.Now - new TimeSpan(Rand.Next(20, 25), 0, 0, 0)//Makes sure that the creation of the order is before the creation of the program
            };

            if(i < 16)//Makes sure that 80 percent of the orders the date will be after the date of Create order
            {
                order.ShipDate = order.OrderDate + new TimeSpan(Rand.Next(3, 5), 0, 0, 0);
            }

            if(i < 10)//Makes sure that 60 percent that about 60% of orders shipped will have a delivery date
            {
                order.DeliveryDate = order.ShipDate + new TimeSpan(Rand.Next(1,2), 0, 0, 0);
            }

            Orders[Config.SizeOfOrder++] = order;
        }
   }
    /// <summary>
    ///  Order item creator
    /// </summary>
    private static void CreateOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            int ordindex = Rand.Next(Config.GetOrderItemID(), Orders.Length + Config.GetOrderItemID());//Generates a random number from the last number of an order code to the size of the array + the same number
            int prodindex = Rand.Next(Config.SizeOfProduct);//Generates a random number from the number of the size of the array of products

            OrderItem orderItem = new OrderItem
            {
                OrderID = Orders[ordindex].ID,
                ProductID = Products[prodindex].ID,
                Price = Products[prodindex].Price,
                Amount = Math.Min(Rand.Next(1,5), Products[prodindex].InStock)//Accepts no more than 4 orders
            };
            Products[prodindex].InStock -= orderItem.Amount;//Updates the quantity in stock
            OrderItems[Config.SizeOfOrderItem++] = orderItem;
        }
    }
    /// <summary>
    ///  Product creator
    /// </summary>
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
            Enums.Category.Suit, Enums.Category.Suit, Enums.Category.Pants,
            Enums.Category.Pants, Enums.Category.Shirts, Enums.Category.Shirts,
            Enums.Category.Ties, Enums.Category.Ties, Enums.Category.Cufflinks, Enums.Category.Cufflinks
        };

        double[] Price = { 800, 450, 220, 300, 120, 115, 80, 60, 180, 350 };

        int[] InStock = { 10, 15, 28, 12, 13, 18, 21, 22, 23, 24 };

        for(int i = 0; i < 10; i++) //creats 10 products
        {
            Product product = new Product
            {
                ID = Rand.Next(100000,99999999),
                Name = Names[i],
                Category = Categories[i],
                Price = Price[i],
                InStock = (i < 9) ? InStock[i] : 0 //Makes sure that 5 percent of the products are out of stock
            };
            Products[Config.SizeOfProduct++] = product;
        }
    }
    /// <summary>
    ///  The s_Initialize method will schedule the method of adding objects
    ///  to the entity arrays in the correct order according to the dependencies
    ///  between the entities
    /// </summary>
    private static void s_Initialize() 
    {
        CreateProducts();
        CreateOrders();
        CreateOrderItems();
    }
}
