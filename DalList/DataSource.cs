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
        private static int IdOfOrder = 0;//Resets the order code to 0
        private static int IdOfOrderItem = 100000;//Resets the order item code to 100000

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
    internal static List<Product> Products;
    internal static List<Order> Orders;
    internal static List<OrderItem> OrderItems;

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
                CustomerName = Names[i],
                CustomerEmail = Emails[i],
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

            Orders.Add(order);
        }
   }
    /// <summary>
    ///  Order item creator
    /// </summary>
    private static void CreateOrderItems()
    {
        OrderItem orderItem = new OrderItem();

        for (int i = 0; i < Orders.Count;i++)
        {
            for (int j = 0; j < Rand.Next(1,5);j++)
            {
                orderItem.ID = Config.GetOrderItemID();
                orderItem.OrderID = Orders[i].ID;
                
                Product product = Products[Rand.Next(0,Products.Count)];
                orderItem.ProductID = product.ID;
                orderItem.Price = product.Price;
                orderItem.Amount = Rand.Next(1,10);
                OrderItems.Add(orderItem);
            }
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

        Category[] Categories =
        {
            Category.Suit, Category.Suit, Category.Pants,
            Category.Pants,Category.Shirts, Category.Shirts,
            Category.Ties, Category.Ties, Category.Cufflinks, Category.Cufflinks
        };

        double[] Price = { 800, 450, 220, 300, 120, 115, 80, 60, 180, 350 };

        int[] InStock = { 10, 15, 28, 12, 13, 18, 21, 22, 23, 24 };

        for(int i = 0; i < 10; i++) //creats 10 products
        {
            Product product = new Product
            { 
                ID = Rand.Next(100000, 99999999),
                Name = Names[i],
                Category = Categories[i],
                Price = Price[i],
                InStock = (i < 9) ? InStock[i] : 0 //Makes sure that 5 percent of the products are out of stock
            };
              Products.Add(product) ;
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
