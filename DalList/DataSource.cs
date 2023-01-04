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
        private static int IdOfOrder = 100000;//Resets the order code to 100000
        private static int IdOfOrderItem = 100000;//Resets the order item code to 100000

        /// <summary>
        ///  Increases the order code by one
        /// </summary>
        public static int GetOrderId() => IdOfOrder++;

        /// <summary>
        ///  Increases the order item code by one
        /// </summary>
        public static int GetOrderItemId() => IdOfOrderItem++;

        internal static Random Num = new Random();
    }

    static readonly Random Rand = new(DateTime.Now.Millisecond);
    internal static List<Product?> Products = new List<Product?>();
    internal static List<Order?> Orders = new List<Order?>();
    internal static List<OrderItem?> OrderItems = new List<OrderItem?>();

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

        string[] Addresses =
        {
            "86th St Bridge", "145th St Bridge", "Abingdon Square", "Abraham Kazan St",
            "Abraham PI", "Absecon Rd", "Beach St", "Bear Rd", "Beaver St", "Beekman PI",
            "Bennett Ave", "Bond St", "Broad St", "Canal St", "Cannon St",
            "Canal St", "Carder Rd", "Cherry St", "Cliff St", "Cinton St"
        };

        Enumerable.Range(0, 20).ToList().ForEach(i => 
        {
            Order order = new Order()
            {
                ID = Config.GetOrderId(),
                CustomerName = Names[i],
                CustomerEmail = Emails[i],
                CustomerAddress = Addresses[i],
                OrderDate = DateTime.Now - new TimeSpan(Rand.Next(20, 25), 0, 0, 0)
            };

            if (i < 16)
            {
                order.ShipDate = order.OrderDate + new TimeSpan(Rand.Next(3, 5), 0, 0, 0);
            }

            if (i >= 16)
                order.ShipDate = null;

            if (i < 10)
            {
                order.DeliveryDate = order.ShipDate + new TimeSpan(Rand.Next(1, 2), 0, 0, 0);
            }
            else
            {
                order.DeliveryDate = null;
            }

            Orders.Add(order);
        });
    }

    /// <summary>
    ///  Order item creator
    /// </summary>
    private static void CreateOrderItems()
    {
        OrderItem orderItem = new OrderItem();
        Orders.ToList().ForEach(o =>
        {
            Enumerable.Range(0, Rand.Next(1, 5)).ToList().ForEach(i =>
            {
                orderItem.ID = Config.GetOrderItemId();
                orderItem.OrderID = (int)(o?.ID)!;
                orderItem.ProductID = (int)(Products[Rand.Next(0, Products.Count)]?.ID)!;
                orderItem.Price = (double)(Products[Rand.Next(0, Products.Count)]?.Price)!;
                orderItem.Amount = Rand.Next(1, 10);
                OrderItems.Add(orderItem);
            });
        });
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
            "Silver cufflinks", "Diamond cufflinks"
        };

        Category[] Categories =
        {
            Category.Suit, Category.Suit, Category.Pants,
            Category.Pants,Category.Shirts, Category.Shirts,
            Category.Ties, Category.Ties, Category.Cufflinks, Category.Cufflinks
        };

        double[] Price = { 800, 450, 220, 300, 120, 115, 80, 60, 180, 350 };
        
        int[] InStock = { 10, 15, 28, 12, 13, 18, 21, 22, 23, 24 };
        
        Enumerable.Range(0, 10).ToList().ForEach(i =>
        {
            Product product = new Product 
            {
                ID = Rand.Next(100000, 99999999),
                Name = Names[i],
                Category = Categories[i],
                Price = Price[i],
                InStock = (i < 9) ? InStock[i] : 0
            };
            Products.Add(product);
       });
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