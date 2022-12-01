using System.ComponentModel.DataAnnotations;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlCart : BlApi.ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddPToCart(BO.Cart cart, int productId)
    {
        DO.Product product1 = new DO.Product();
        DO.OrderItem orderitem1 = new DO.OrderItem();

        try
        {
            product1 = Dal.Product.GetById(productId);
            orderitem1 = Dal.OrderItem.GetById(productId);
        }
        catch (Exception)
        {
            throw new BO.NotExistsException();
        }

        foreach (var item in cart.Items)
        {
            if (productId == item.ProductID)
            {
                if (product1.InStock > item.Amount)
                {
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;
                    return cart;
                }
            }
        }

        if (product1.InStock > 0)
        {
            BO.OrderItem orderitemBO = new BO.OrderItem
            {
                ID = orderitem1.OrderID,
                Name = product1.Name,
                ProductID = orderitem1.ProductID,
                Price = orderitem1.Price,
                Amount = 1,
                TotalPrice = orderitem1.Price
            };

            cart.Items.Add(orderitemBO);
        }

        return cart;
    }
    public BO.Cart UpdateAmount(BO.Cart cart, int productId, int Namount)
    {
        DO.Product product1 = new DO.Product();
        try
        {
            product1 = Dal.Product.GetById(productId);
        }
        catch (Exception)
        {
                throw new BO.NotExistsException();
        }

        foreach (var item in cart.Items)
        {
            if (productId == item.ProductID)
            {
                if (Namount > item.Amount)
                {
                    if (product1.InStock >= item.Amount + Namount)
                    {
                        item.Amount += Namount;
                        item.TotalPrice += item.Price;
                        cart.TotalPrice += item.Price;
                    }
                }
                else if (Namount != 0 && Namount < item.Amount)
                {
                    item.Amount -= Namount;
                    item.TotalPrice -= item.Price;
                    cart.TotalPrice -= item.Price;
                }
                else
                {
                    cart.Items.Remove(item);
                    cart.TotalPrice -= item.TotalPrice;
                }
            }
        }
        return cart;
    }
    public void ConfirmationOrder(BO.Cart cart)
    {
        if (!cart.Items.Any() || cart.Name == null || cart.Address == null)
            throw new BO.NotExistsException();

        if (!new EmailAddressAttribute().IsValid(cart.Email))
            throw new BO.InvalidEmailException();

        DO.Order dOrder = new DO.Order()
        {
            CustomerName = cart.Name,
            CustomerAdress = cart.Address,
            CustomerEmail = cart.Email,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue,
            DeliveryDate = DateTime.MinValue
        };

        int OrderId;

        try
        {
            OrderId = Dal.Order.Add(dOrder);
        }
        catch (Exception)
        {
            throw new BO.AlreadyExistsException();
        }
        
        foreach (var item in cart.Items)
        {
            DO.OrderItem dOrderItem = new DO.OrderItem()
            {
                ID = item.ID,
                ProductID = item.ProductID,
                OrderID = OrderId,
                Price = item.Price,
                Amount = item.Amount
            };

            Dal.OrderItem.Add(dOrderItem);
        }

        DO.Product product = new DO.Product();

        foreach (var item in cart.Items)
        {
            product = Dal.Product.GetById(item.ProductID);
            product.InStock -= item.Amount;
            Dal.Product.Add(product);
        }
    }
} 