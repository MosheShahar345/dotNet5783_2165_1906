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
            choice = int.Parse(Console.ReadLine());
            StartChoose option = (StartChoose)choice;

            switch (option)
            {
                case StartChoose.EXIT:
                    Console.WriteLine("bye");
                    break;

                case StartChoose.CART:
                    MenuOfCart();
                    break;

                case StartChoose.PRODUCT:
                    MenuOfProduct();
                    break;
                case StartChoose.ORDER:
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

        char choice = char.Parse(Console.ReadLine());

        switch (choice)
        {
            case 'a':
                Console.Write("enter id of product: ");
                int idProduct = int.Parse(Console.ReadLine());
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
                Console.WriteLine("To update please enter the following details:");
                Console.Write("Amount of product to update: ");

                newAmount = int.Parse(Console.ReadLine());
                test.Cart.UpdateAmount(cart, productId, newAmount);
                break;

            case 'c':
                Console.Write("enter your name: ");
                cart.Name = Console.ReadLine();

                Console.Write("enter tour email: ");
                cart.Email = Console.ReadLine();

                Console.Write("enter a your home address: ");
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

        char choice = char.Parse(Console.ReadLine());
        int ichoice;
        int ID;

        switch (choice)
        {
            case 'a':
                List<BO.ProductForList> products = new List<BO.ProductForList>();
                products = test.Product.GetProductForList().ToList();
                products.ForEach(product => Console.WriteLine(product));
                break;

            case 'b':
                Console.Write("Please enter id :");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(test.Product.GetProduct(ID));
                }
                catch (DO.DoesNotExistException e) { Console.WriteLine(e); }
                break;

            case 'c':
                Console.Write("enter ID: ");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine(test.Product.GetProductCustomer(ID, cart));
                break;

            case 'd':
                Product product = new Product();
                Console.Write("enter product ID: ");
                product.ID = int.Parse(Console.ReadLine());

                Console.Write("enter name of product: ");
                product.Name = Console.ReadLine();

                Console.Write("enter a product price: ");
                product.Price = int.Parse(Console.ReadLine());

                Console.WriteLine("select a product category" +
                                  "\n 0. Suits." +
                                  "\n 1. Pants." +
                                  "\n 2. Shirts." +
                                  "\n 3. Ties." +
                                  "\n 4. Cufflinks.");

                ichoice = int.Parse(Console.ReadLine());
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
                Console.Write("enter ID of prodcut to delete: ");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    test.Product.DeleteProductAdmin(ID);
                    Console.WriteLine("sucsses");
                }
                catch (NotExistsException e) { Console.WriteLine(e); }
                break;

            case 'f':
                product = new Product();
                Console.Write("enter ID of product to update: ");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(test.Product.GetProduct(ID));
                }
                catch (NotExistsException e) { Console.WriteLine(e); }

                Console.WriteLine("enter the details of the product to update:");

                Console.Write("enter ID of product number: ");
                product.ID = int.Parse(Console.ReadLine());

                Console.Write("enter name of product: ");
                product.Name = Console.ReadLine();

                Console.Write("enter price of product: ");
                product.Price = int.Parse(Console.ReadLine());

                Console.WriteLine("select a category" +
                                  "\n 0. Suit." +
                                  "\n 1. Pants." +
                                  "\n 2. Shirts." +
                                  "\n 3. Ties." +
                                  "\n 4. Cufflinks.");

                ichoice = int.Parse(Console.ReadLine());
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

        char choice = char.Parse(Console.ReadLine());
        int orderId;

        switch (choice)
        {
            case 'a':
                List<OrderForList> orders = test.Order.GetOrderForList().ToList();
                orders.ForEach(order => Console.WriteLine(order));
                break;

            case 'b':
                Console.Write("enter order ID: ");
                orderId = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(test.Order.GetOrder(orderId));
                }
                catch (NotExistsException e) { Console.WriteLine(e); }

                break;

            case 'c':
                Console.Write("enter order ID: ");
                orderId = int.Parse(Console.ReadLine());
                Console.WriteLine(test.Order.UpdateOrderShipping(orderId));
                break;

            case 'd':
                Console.Write("enter order ID: ");
                orderId = int.Parse(Console.ReadLine());
                Console.WriteLine(test.Order.UpdateOrderDelivery(orderId));
                break;

            case 'e':
                Console.Write("enter order ID: ");
                orderId = int.Parse(Console.ReadLine());
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