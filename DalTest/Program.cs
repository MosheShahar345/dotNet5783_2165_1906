using System;
using Dal;
using DO;

namespace Dal
{
   public class Program
    {
        private static DalProduct dal_product = new DalProduct();
        private static DalOrder dal_order = new DalOrder();
        private static DalOrderItem dal_order_item = new DalOrderItem();

        enum ITEMS
        {
            PRODUCT = 1,
            ORDER,
            ORDERITEM
        }

        enum OPTIONS
        {
            ADD = 1,
            DELETE,
            CHECK,
            UPDATE,
            PRINT
        }
      public  static int Main(string[] args)
        {
            int choice = int.Parse(Console.ReadLine());
            PrintStartMenu();

            do
            {
                switch (choice)
                {
                    case (int)ITEMS.PRODUCT:
                        MenuOfPruduct();
                        break;

                    case (int)ITEMS.ORDER:
                        MenuOfOrder();
                        break;

                    case (int)ITEMS.ORDERITEM:
                        MenuOfOrderItem();
                        break;

                    default:
                        choice = 0;
                        break;
                }
            } while (choice != 0);

            return 0;
        }

        public static void MenuOfOrderItem()
        {
            Console.WriteLine("add new order item - 1 \n" +
                             "delete order item - 2 \n" +
                             "check order item - 3 \n" +
                             "update order item - 4 \n" +
                             "print order item - 5 \n");

            int choice = int.Parse(Console.ReadLine());
            OrderItem orderitem = new OrderItem();
            int ID;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter: product ID, order ID, price, amount ");
                    orderitem.ProductID = int.Parse(Console.ReadLine());
                    orderitem.OrderID = int.Parse(Console.ReadLine());
                    orderitem.Price = double.Parse(Console.ReadLine());
                    orderitem.Amount = int.Parse(Console.ReadLine());     
                    try
                    {
                        dal_product.AddOrder(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.DELETE:
                    Console.WriteLine("enter order item ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        dal_order_item.DeleteOrderItem(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.CHECK:
                    Console.WriteLine("enter order item ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        orderitem = DalOrderItem.Get(ID);
                        Console.WriteLine(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter order ID");
                    orderitem.OrderID = int.Parse(Console.ReadLine());
                    try
                    {
                        orderitem = DalOrder.Update(orderitem);
                        Console.WriteLine(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    Console.WriteLine("enter:  name, email, adress");
                    order.CustomerName = Console.ReadLine();
                    order.CustomerEmail = Console.ReadLine(); ;
                    order.CustomerAdress = Console.ReadLine(); ;
                    dal_order.AddOrder(order);
                    break;

                case (int)OPTIONS.PRINT:
                    Product[] products = DalProduct.Get();
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

        public static void MenuOfOrder()
        {
            Console.WriteLine("add new order - 1 \n" +
                              "delete order - 2 \n" +
                              "check order - 3 \n" +
                              "update order - 4 \n" +
                              "print order - 5 \n");

            int choice = int.Parse(Console.ReadLine());
            Order order = new Order();
            int ID;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter: customerName, cstomerEmail, customerAdress");
                    order.CustomerName = Console.ReadLine();
                    order.CustomerEmail = Console.ReadLine();
                    order.CustomerAdress = Console.ReadLine();
                    try
                    {
                        dal_product.AddOrder(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.DELETE:
                    Console.WriteLine("enter order ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        DalOrder.DeleteOrder(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.CHECK:
                    Console.WriteLine("enter order ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        order = DalOrder.Get(ID);
                        Console.WriteLine(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter order ID");
                    order.ID = int.Parse(Console.ReadLine());
                    try
                    {
                        order = DalOrder.Get(order.ID);
                        dal_order.Update(order);
                        Console.WriteLine(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    Console.WriteLine("enter:  name, email, adress");
                    order.CustomerName = Console.ReadLine();
                    order.CustomerEmail = Console.ReadLine(); ;
                    order.CustomerAdress = Console.ReadLine(); ;
                    dal_order.AddOrder(order);
                    break;

                case (int)OPTIONS.PRINT:
                    Order[] orders = dal_order.Get();
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

        public static void MenuOfPruduct()
        {
            Console.WriteLine("add new product - 1 \n" +
                              "delete product - 2 \n" +
                              "check product - 3 \n" +
                              "update product - 4 \n" +
                              "print ptoduct - 5 \n");

            int choice = int.Parse(Console.ReadLine());
            Product product = new Product();
            int ID;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter: name, email, adress");
                    product.Name = Console.ReadLine();
                    product.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                    product.Price = double.Parse(Console.ReadLine());
                    product.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        dal_product.AddProduct(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.DELETE:
                    Console.WriteLine("enter product ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        DalProduct.DeleteProduct(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.CHECK:
                    Console.WriteLine("enter product ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = DalProduct.Get(ID);
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter product ID");
                    product.ID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = DalProduct.Get(product.ID);
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    Console.WriteLine("enter: name, category, price, in-stock");
                    product.Name = Console.ReadLine();
                    product.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                    product.Price = double.Parse(Console.ReadLine());
                    product.InStock = int.Parse(Console.ReadLine());
                    dal_product.AddProduct(product);
                    break;

                case (int)OPTIONS.PRINT:
                    Product[] products = dal_product.Get();
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
            Console.Write("click 0 to exit \n" +
                          "click 1 to view product \n" +
                          "click 2 to view order \n" +
                          "click 3 to view item \n");
        }
    }
}
