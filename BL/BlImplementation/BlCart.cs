using System.ComponentModel.DataAnnotations;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class BlCart : BlApi.ICart
{
    private IDal Dal = new DalList(); // to access DO entities

    public BO.Cart AddPToCart(BO.Cart cart, int productId)
    {
        DO.Product product = new DO.Product();
        List<DO.OrderItem> orderitems = Dal.OrderItem.GetAll().ToList();

        try
        {
            product = Dal.Product.GetById(productId);
        }
        catch (Exception e)
        {
            throw new BO.NotExistsException("", e);
        }

        if (cart.Items != null)
        {
            foreach (var item in cart.Items)
            {
                if (productId == item.ProductID)
                {
                    if (product.InStock > item.Amount)
                    {
                        item.Amount++;
                        item.TotalPrice += item.Price;
                        cart.TotalPrice += item.Price;
                        return cart;
                    }
                }
            }
        }
        if (product.InStock > 0)
        {
            foreach (var item in orderitems)
            {
                if (productId == item.ProductID)
                {
                    BO.OrderItem orderitem = new BO.OrderItem
                    {
                        ID = item.ID,
                        Name = product.Name,
                        ProductID = item.ProductID,
                        Price = item.Price,
                        Amount = 1,
                        TotalPrice = item.Price
                    };
                    cart.Items.Add(orderitem);
                    break;
                }
            }
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
            CustomerAddress = cart.Address,
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