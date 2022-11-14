using System;
using System.Diagnostics;
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
        static int Main(string[] args)
        {
            int choice;

            do
            {
                PrintStartMenu();
                choice = int.Parse(Console.ReadLine());

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
            int choice;
            int orderItemId;
            int productID;
            int orderID;
            double price;
            int amount;

            bool status = int.TryParse(Console.ReadLine(), out choice);

            //int choice = int.Parse(Console.ReadLine());
            OrderItem orderitem;
            int ID;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter:order item ID, product ID, order ID, price, amount ");
                    orderItemId = int.Parse(Console.ReadLine());
                    productID = int.Parse(Console.ReadLine());
                    orderID = int.Parse(Console.ReadLine());
                    price = double.Parse(Console.ReadLine());
                    amount = int.Parse(Console.ReadLine());
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
                        dal_order_item.addOrderItem(orderitem);
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
                        dal_order_item.deleteOrderItem(ID);
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
                        orderitem = dal_order_item.get(ID);
                        Console.WriteLine(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter:order item ID");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        orderitem = dal_order_item.get(ID);
                        Console.WriteLine(orderitem);
                        Console.WriteLine("enter: price, amount ");
                        orderitem.Price = int.Parse(Console.ReadLine());
                        orderitem.Amount = int.Parse(Console.ReadLine());
                        dal_order_item.update(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case (int)OPTIONS.PRINT:
                    OrderItem[] orderItems = dal_order_item.get();
                    foreach (var item in orderItems)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                default:
                    Console.WriteLine("try harder, i did");
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
                    Console.WriteLine("enter: order ID, customer name, cstomer email, customer adress");
                    order.ID = int.Parse(Console.ReadLine());
                    order.CustomerName = Console.ReadLine();
                    order.CustomerEmail = Console.ReadLine();
                    order.CustomerAdress = Console.ReadLine();
                    try
                    {
                        dal_order.addOrder(order);
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
                        dal_order.deleteOrder(ID);
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
                        order = dal_order.get(ID);
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
                        order = dal_order.get(order.ID);
                        Console.WriteLine(order);
                        Console.WriteLine("enter customer name, cstomer email, customer adress ");
                        order.CustomerName = Console.ReadLine();
                        order.CustomerEmail = Console.ReadLine();
                        order.CustomerAdress = Console.ReadLine();
                        dal_order.update(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    break;

                case (int)OPTIONS.PRINT:
                    Order[] orders = dal_order.get();
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
                    Console.WriteLine("enter: product ID, name, category, price, in stock ");
                    product.ID = int.Parse(Console.ReadLine()); ;
                    product.Name = Console.ReadLine();
                    product.Category = (Category)Convert.ToInt32(Console.ReadLine());
                    product.Price = double.Parse(Console.ReadLine());

                    product.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        dal_product.addProduct(product);
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
                        dal_product.deleteProduct(ID);
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
                        product = dal_product.get(ID);
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
                        product = dal_product.get(product.ID);
                        Console.WriteLine(product);
                        Console.WriteLine("enter: name, category, price, in-stock");
                        product.Name = Console.ReadLine();
                        product.Category = (Category)Convert.ToInt32(Console.ReadLine());
                        product.Price = double.Parse(Console.ReadLine());
                        product.InStock = int.Parse(Console.ReadLine());
                        dal_product.update(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    //dal_product.addProduct(product);
                    break;

                case (int)OPTIONS.PRINT:
                    Product[] products = dal_product.get();
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
            Console.WriteLine("click 0 to exit \n" +
                          "click 1 to view product \n" +
                          "click 2 to view order \n" +
                          "click 3 to view order item \n");
        }
    }
}