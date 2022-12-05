using BlApi;
using BlImplementation;
using BO;
namespace BlTest;

internal class Program
{
    private static IBl test = new Bl();
    private static Cart cart = new Cart();

    static int Main(string[] args)
    {
        cart.Items = new List<OrderItem>();
        int choice;

        do
        {
            PrintStartMenu();
            int.TryParse(Console.ReadLine(), out choice);
            StartChoose option = (StartChoose)choice;

            switch (option)
            {
                case StartChoose.Exit:
                    Console.WriteLine("bye");
                    break;

                case StartChoose.Cart:
                    MenuOfCart();
                    break;

                case StartChoose.Product:
                    MenuOfProduct();
                    break;
                case StartChoose.Order:
                    MenuOfOrder();
                    break;

                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        } while (choice != 0);

        return 0;
    }

    public static void MenuOfCart()
    {
        Console.WriteLine("enter your choice:\n" +
                          " a. Add product to the cart.\n" +
                          " b. Update the product in the cart.\n" +
                          " c. Confirm Order.");

        char choice;
        char.TryParse(Console.ReadLine(), out choice);

        switch (choice)
        {
            case 'a':
                Console.Write("enter ID of product: ");
                int idProduct;
                int.TryParse(Console.ReadLine(), out idProduct);
                try
                {
                    test.Cart.AddPToCart(cart, idProduct);
                }
                catch (NotExistsException e) { Console.WriteLine(e); }
                break;

            case 'b':
                int productId, newAmount;
                Console.Write("product ID: ");

                int.TryParse(Console.ReadLine(), out productId);
                OrderItem orderitem = new OrderItem();
                orderitem = cart.Items.Find(it => it.ProductID == productId);
                Console.WriteLine(orderitem);
                Console.Write("enter the amount of products to update: ");

                int.TryParse(Console.ReadLine(), out newAmount);
                test.Cart.UpdateAmount(cart, productId, newAmount);
                break;

            case 'c':
                Console.Write("enter your name: ");
                cart.Name = Console.ReadLine();

                Console.Write("enter your email: ");
                cart.Email = Console.ReadLine();

                Console.Write("enter your home address: ");
                cart.Address = Console.ReadLine();
                test.Cart.ConfirmationOrder(cart);
                break;

            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
    public static void MenuOfProduct()
    {
        Console.WriteLine("enter your choice:\n" +
                          " a. Get product list.\n" +
                          " b. Get product for admin.\n" +
                          " c. Get product for customer.\n" +
                          " d. Add new product.\n" +
                          " e. Delete product.\n" +
                          " f. Update product.");

        int ichoice;
        int ID;
        char choice;
        char.TryParse(Console.ReadLine(), out choice);

        switch (choice)
        {
            case 'a':
                List<BO.ProductForList> products = new List<BO.ProductForList>();
                products = test.Product.GetProductForList().ToList();
                products.ForEach(product => Console.WriteLine(product));
                break;

            case 'b':
                Console.Write("enter product ID: ");
                int.TryParse(Console.ReadLine(), out ID);
                try
                {
                    Console.WriteLine(test.Product.GetProduct(ID));
                }
                catch (DO.DoesNotExistException e) { Console.WriteLine(e); }
                break;

            case 'c':
                Console.Write("enter product ID: ");
                int.TryParse(Console.ReadLine(), out ID);
                try
                {
                    Console.WriteLine(test.Product.GetProductCustomer(ID, cart));
                }
                catch (DO.DoesNotExistException e) { Console.WriteLine(e); }
                break;

            case 'd':
                Product product = new Product();
                Console.Write("enter product ID: ");
                product.ID = int.Parse(Console.ReadLine());

                Console.Write("enter name of product: ");
                product.Name = Console.ReadLine();

                Console.Write("enter price of a product: ");
                product.Price = int.Parse(Console.ReadLine());

                Console.WriteLine("select a product category" +
                                  "\n 0. Suits." +
                                  "\n 1. Pants." +
                                  "\n 2. Shirts." +
                                  "\n 3. Ties." +
                                  "\n 4. Cufflinks.");

                int.TryParse(Console.ReadLine(), out ichoice);
                product.Category = (Category)ichoice;

                Console.Write("enter the quantity of the product in stock: ");
                product.InStock = int.Parse(Console.ReadLine());
                try
                {
                    test.Product.AddProductAdmin(product);
                }
                catch (InvalidEmailException e) { Console.WriteLine(e); }
                break;

            case 'e':
                Console.Write("enter ID of product to delete: ");
                int.TryParse(Console.ReadLine(), out ID);
                try
                {
                    test.Product.DeleteProductAdmin(ID);
                    Console.WriteLine("success");
                }
                catch (NotExistsException e) { Console.WriteLine(e); }
                break;

            case 'f':
                product = new Product();
                Console.Write("enter ID of product to update: ");
                int.TryParse(Console.ReadLine(), out ID);
                try
                {
                    Console.WriteLine(test.Product.GetProduct(ID));
                }
                catch (NotExistsException e) { Console.WriteLine(e); }

                Console.WriteLine("enter the details of the product to update:");
                product.ID = ID;

                Console.Write("enter name of product: ");
                product.Name = Console.ReadLine();

                Console.Write("enter price of a product: ");
                product.Price = int.Parse(Console.ReadLine());

                Console.WriteLine("select a category" +
                                  "\n 0. Suit." +
                                  "\n 1. Pants." +
                                  "\n 2. Shirts." +
                                  "\n 3. Ties." +
                                  "\n 4. Cufflinks.");

                int.TryParse(Console.ReadLine(), out ichoice);
                product.Category = (Category)ichoice;

                Console.Write("enter the quantity of the product in stock: ");
                product.InStock = int.Parse(Console.ReadLine());
                try
                {
                    test.Product.UpdateProductAdmin(product);
                }
                catch (NotExistsException e) { Console.WriteLine(e); }
                break;

            default:
                Console.WriteLine("ERROR");
                break;

        }
    }
    public static void MenuOfOrder()
    {
        Console.WriteLine(" a. Get order list.\n" +
                          " b. Get order.\n" +
                          " c. Update order shipping.\n" +
                          " d. Update order delivery.\n" +
                          " e. Tracking order.\n");

        int orderId;
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        
        switch (choice)
        {
            case 'a':
                List<OrderForList> orders = test.Order.GetOrderForList().ToList();
                orders.ForEach(order => Console.WriteLine(order));
                break;

            case 'b':
                Console.Write("enter order ID: ");
                int.TryParse(Console.ReadLine(), out orderId);
                try
                {
                    Console.WriteLine(test.Order.GetOrder(orderId));
                }
                catch (NotExistsException e) { Console.WriteLine(e); }

                break;

            case 'c':
                Console.Write("enter order ID: ");
                int.TryParse(Console.ReadLine(), out orderId);
                Console.WriteLine(test.Order.UpdateOrderShipping(orderId));
                break;

            case 'd':
                Console.Write("enter order ID: ");
                int.TryParse(Console.ReadLine(), out orderId);
                Console.WriteLine(test.Order.UpdateOrderDelivery(orderId));
                break;

            case 'e':
                Console.Write("enter order ID: ");
                int.TryParse(Console.ReadLine(), out orderId);
                Console.WriteLine(test.Order.TrackOrder(orderId));
                break;

            default:
                Console.WriteLine("ERROR");
                break;
        }
    }

    public static void PrintStartMenu()
    {
        Console.WriteLine("\nenter your choice:" +
                          "\n 0. Exit. " +
                          "\n 1. Cart. " +
                          "\n 2. Product. " +
                          "\n 3. Order");
    }
}