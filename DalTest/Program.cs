using System;
using System.Diagnostics;
using DalApi;
using DO;
using Dal;


namespace DalTest
{
    internal class Program
    {
        private static IDal test = new DalList();
        
        static int Main(string[] args)
        {
            int choice;

            do
            {
                PrintStartMenu();
                choice = int.Parse(Console.ReadLine());
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

            char choice = char.Parse(Console.ReadLine());
            int orderItemId;
            int productID;
            int orderID;
            double price;
            int amount;

            //bool status = int.TryParse(Console.ReadLine(), out choice);

            OrderItem orderitem;
            int ID;
            switch (choice)
            {
                case 'a':
                    Console.WriteLine("enter: order item ID, " +
                                      "product ID, order ID, " +
                                      "price, amount ");

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
                        test.OrderItem.Add(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'b':
                    Console.WriteLine("enter order item ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        orderitem = test.OrderItem.GetById(ID);
                        Console.WriteLine(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'c':
                    IEnumerable<DO.OrderItem> orderItems = test.OrderItem.GetAll();
                    foreach (var item in orderItems)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 'd':
                    Console.WriteLine("enter:order item ID");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        orderitem = test.OrderItem.GetById(ID);
                        Console.WriteLine(orderitem);
                        Console.WriteLine("enter: price, amount ");
                        orderitem.Price = int.Parse(Console.ReadLine());
                        orderitem.Amount = int.Parse(Console.ReadLine());
                        test.OrderItem.Update(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'e':
                    Console.WriteLine("enter order item ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        test.OrderItem.Delete(ID);
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
                              " d. update order item\n" +
                              " e. delete order item\n");

            char choice = char.Parse(Console.ReadLine());
            Order order = new Order();
            int ID;

            switch (choice)
            {
                case 'a':
                    Console.WriteLine("enter: order ID, " +
                                      "customer name, " +
                                      "customer email, " +
                                      "customer address");

                    order.ID = int.Parse(Console.ReadLine());
                    order.CustomerName = Console.ReadLine();
                    order.CustomerEmail = Console.ReadLine();
                    order.CustomerAddress = Console.ReadLine();
                    try
                    {
                        test.Order.Add(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'e':
                    Console.WriteLine("enter order ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        test.Order.Delete(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'b':
                    Console.WriteLine("enter order ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        order = test.Order.GetById(ID);
                        Console.WriteLine(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'd':
                    Console.WriteLine("enter order ID");
                    order.ID = int.Parse(Console.ReadLine());
                    try
                    {
                        order = test.Order.GetById(order.ID);
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
                    IEnumerable<Order> orders = test.Order.GetAll();
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
                              " a. add new order\n" +
                              " b. get order\n" +
                              " c. print all orders\n" +
                              " d. update order item\n" +
                              " e. delete order item\n");

            char choice = char.Parse(Console.ReadLine());
            Product product = new Product();
            int ID;

            switch (choice)
            {
                case 'a':
                    Console.WriteLine("enter: product ID, name, " +
                                      "category, price, in stock ");

                    product.ID = int.Parse(Console.ReadLine()); ;
                    product.Name = Console.ReadLine();
                    product.Category = (Category)Convert.ToInt32(Console.ReadLine());
                    product.Price = double.Parse(Console.ReadLine());
                    product.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        test.Product.Add(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'e':
                    Console.WriteLine("enter product ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        test.Product.Delete(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'b':
                    Console.WriteLine("enter product ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = test.Product.GetById(ID);
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case 'd':
                    Console.WriteLine("enter product ID");
                    product.ID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = test.Product.GetById(product.ID);
                        Console.WriteLine(product);
                        Console.WriteLine("enter: name, category, price, in-stock");
                        product.Name = Console.ReadLine();
                        product.Category = (Category)Convert.ToInt32(Console.ReadLine());
                        product.Price = double.Parse(Console.ReadLine());
                        product.InStock = int.Parse(Console.ReadLine());
                        test.Product.Update(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    break;

                case 'c':
                    IEnumerable<Product> products = test.Product.GetAll();
           
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