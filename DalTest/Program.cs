using Dal;
using DO;

namespace DalTest
{
    internal class Program
    {
        private static DalApi.IDal? test = DalApi.Factory.Get();

        static int Main(string[] args)
        {
            int choice;

            do
            {
                PrintStartMenu();
                choice = int.Parse(Console.ReadLine()!);
                StartChoose option = (StartChoose)choice;

                switch (option)
                {
                    case StartChoose.Exit:
                        Console.WriteLine("bye");
                        break;

                    case StartChoose.Product:
                        MenuOfProduct();
                        break;

                    case StartChoose.Order:
                        MenuOfOrder();
                        break;

                    case StartChoose.OrderItem:
                        MenuOfOrderItem();
                        break;

                    case StartChoose.XML:
                        XMLTools.SaveListToXNLserializer(test!.Product.GetAll().ToList(), "Product");
                        XMLTools.SaveListToXNLserializer(test.Order.GetAll().ToList(), "Order");
                        XMLTools.SaveListToXNLserializer(test.OrderItem.GetAll().ToList(), "OrderItem");
                        XMLTools.SaveConfigXml("OrderID", test.Order.GetAll().Last()?.ID ?? 0);
                        XMLTools.SaveConfigXml("OrderItemID", test.OrderItem.GetAll().Last()?.ID ?? 0);
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            } while (choice != 0);

            return 0;
        }

        public static void MenuOfOrderItem()
        {
            Console.WriteLine("enter your choice:\n" +
                              " a. add new order item\n" +
                              " b. get order item\n" +
                              " c. print all order items\n" +
                              " d. update order item\n" +
                              " e. delete order item\n");

            OrderItem orderitem;
            int ID;
            char choice;
            int orderItemId;
            int productID;
            int orderID;
            double price;
            int amount;
            char.TryParse(Console.ReadLine(), out choice);
            
            switch (choice)
            {
                case 'a':
                    Console.WriteLine("enter: order item ID, " +
                                      "product ID, order ID, " +
                                      "price, amount ");

                    int.TryParse(Console.ReadLine(), out orderItemId);
                    int.TryParse(Console.ReadLine(), out productID);
                    int.TryParse(Console.ReadLine(), out orderID);
                    double.TryParse(Console.ReadLine(), out price);
                    int.TryParse(Console.ReadLine(), out amount);

                    try
                    {
                        orderitem = new OrderItem
                        {
                            ID = orderItemId,
                            ProductID = productID,
                            OrderID = orderID,
                            Price = price,
                            Amount = amount
                        };
                        test?.OrderItem.Add(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'b':
                    Console.WriteLine("enter order item ID:");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        orderitem = (OrderItem)test?.OrderItem.GetEntity(it => it?.ID == ID)!;
                        Console.WriteLine(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'c':
                    IEnumerable<DO.OrderItem?> orderItems = test?.OrderItem.GetAll()!;
                    foreach (var item in orderItems)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 'd':
                    Console.WriteLine("enter: order item ID");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        orderitem = (OrderItem)test?.OrderItem.GetEntity(it => it?.ID == ID)!;
                        Console.WriteLine(orderitem);
                        Console.WriteLine("enter: price, amount ");
                        orderitem.Price = int.Parse(Console.ReadLine()!);
                        orderitem.Amount = int.Parse(Console.ReadLine()!);
                        test.OrderItem.Update(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'e':
                    Console.WriteLine("enter order item ID:");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        test?.OrderItem.Delete(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                default:
                    Console.WriteLine("try harder");
                    break;
            }
        }

        public static void MenuOfOrder()
        {
            Console.WriteLine("enter your choice:\n" +
                              " a. add new order\n" +
                              " b. get order\n" +
                              " c. print all orders\n" +
                              " d. update order\n" +
                              " e. delete order\n");

            Order order = new Order();
            int ID;
            char choice;
            char.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 'a':
                    Console.WriteLine("enter: order ID, " +
                                      "customer name, " +
                                      "customer email, " +
                                      "customer address");

                    order.ID = int.Parse(Console.ReadLine()!);
                    order.CustomerName = Console.ReadLine();
                    order.CustomerEmail = Console.ReadLine();
                    order.CustomerAddress = Console.ReadLine();
                    try
                    {
                        test?.Order.Add(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'e':
                    Console.WriteLine("enter order ID:");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        test?.Order.Delete(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'b':
                    Console.WriteLine("enter order ID:");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        order = (Order)test?.Order.GetEntity(it => it?.ID == ID)!;
                        Console.WriteLine(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'd':
                    Console.WriteLine("enter order ID");
                    order.ID = int.Parse(Console.ReadLine()!);
                    try
                    {
                        order = (Order)test?.Order.GetEntity(it => it?.ID == order.ID)!;
                        Console.WriteLine(order);
                        Console.WriteLine("enter: customer name, " +
                                          "customer email, " +
                                          "customer address ");

                        order.CustomerName = Console.ReadLine();
                        order.CustomerEmail = Console.ReadLine();
                        order.CustomerAddress = Console.ReadLine();
                        test.Order.Update(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    break;

                case 'c':
                    IEnumerable<Order?> orders = test?.Order.GetAll()!;
                    foreach (var item in orders)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                default:
                    Console.WriteLine("try harder");
                    break;
            }
        }
        public static void MenuOfProduct()
        {
            Console.WriteLine("enter your choice:\n" +
                              " a. add new product\n" +
                              " b. get product\n" +
                              " c. print all products\n" +
                              " d. update product\n" +
                              " e. delete product\n");

            Product product = new Product();
            int ID;
            char choice;
            char.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 'a':
                    Console.WriteLine("enter: product ID, name, " +
                                      "category, price, in stock ");

                    product.ID = int.Parse(Console.ReadLine()!); ;
                    product.Name = Console.ReadLine();
                    product.Category = (Category)Convert.ToInt32(Console.ReadLine());
                    product.Price = double.Parse(Console.ReadLine()!);
                    product.InStock = int.Parse(Console.ReadLine()!);
                    try
                    {
                        test?.Product.Add(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'e':
                    Console.WriteLine("enter product ID:");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        test?.Product.Delete(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'b':
                    Console.WriteLine("enter product ID:");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        product = (Product)test?.Product.GetEntity(it => it?.ID == ID)!;
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'd':
                    Console.WriteLine("enter product ID");
                    product.ID = int.Parse(Console.ReadLine()!);
                    try
                    {
                        product = (Product)test?.Product.GetEntity(x => x?.ID == product.ID)!;
                        Console.WriteLine(product);
                        Console.WriteLine("enter: name, category, price, in-stock");
                        product.Name = Console.ReadLine();
                        product.Category = (Category)Convert.ToInt32(Console.ReadLine());
                        product.Price = double.Parse(Console.ReadLine()!);
                        product.InStock = int.Parse(Console.ReadLine()!);
                        test.Product.Update(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    break;

                case 'c':
                    IEnumerable<Product?> products = test?.Product.GetAll()!;
           
                    foreach (var item in products)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                default:
                    Console.WriteLine("try harder");
                    break;
            }
        }
        public static void PrintStartMenu()
        {
            Console.WriteLine("\nenter your choice:" +
                              "\n 0. Exit. " +
                              "\n 1. Product. " +
                              "\n 2. Order. " +
                              "\n 3. Order Item" +
                              "\n 4. XML");
        }
    }
}