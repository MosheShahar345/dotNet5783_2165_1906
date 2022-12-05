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
        DO.Product product;
        List<DO.OrderItem> orderitems = new();

        try
        {
            product = Dal.Product.GetById(productId);
        }
        catch (Exception e)
        {
            throw new BO.NotExistsException("", e);
        }

        if (cart.Items == null)
        {
            cart.Items = new();
        }

        foreach (var item in cart.Items)
        {
            if (productId == item.ProductID)
            {
                if (product.InStock > item.Amount + 1)
                {
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;

                    return cart;
                }

                throw new BO.NotEnoughRoomInStockException(product.Name);
            }
        }

        if (product.InStock > 0)
        {
            cart.Items.Add(new BO.OrderItem
            {   
                ID = 0,
                Name = product.Name,
                ProductID = product.ID,
                Price = product.Price,
                Amount = 1,
                TotalPrice = product.Price
            });

            return cart;
        }

        throw new BO.NotEnoughRoomInStockException(
            $"product with name: {product.Name} not enough in stock");
    }

    public BO.Cart UpdateAmount(BO.Cart cart, int productId, int nAmount)
    {
        if (productId <= 0 || nAmount < 0)
            throw new InvalidInputException();

        DO.Product product = new DO.Product();
        BO.OrderItem orderItem;

        try
        {
            orderItem = cart.Items.Find(it => it.ID == productId);
        }
        catch (Exception e)
        {
            throw new BO.NotExistsException("", e);
        }

        if (orderItem?.Amount == nAmount) { return cart;}

        try
        {
            product = Dal.Product.GetById(productId);
        }
        catch (Exception e)
        {
            throw new BO.NotExistsException("", e);
        }

        if (orderItem != null)
        {

            if (nAmount > orderItem?.Amount)
            {
                AddPToCart(cart, productId);
            }

            else if (nAmount < orderItem?.Amount)
            {
                orderItem.Amount = nAmount;
                cart.TotalPrice -= orderItem.TotalPrice;
                orderItem.TotalPrice = orderItem.Price * nAmount;
                cart.TotalPrice += orderItem.TotalPrice;
            }

            else if (nAmount == 0)
            {
                cart.TotalPrice -= orderItem.TotalPrice;
                cart.Items.Remove(orderItem);
            }
        }

        return cart;
    }
    public void ConfirmationOrder(BO.Cart cart)
    {
        if (cart.Items == null || cart.Name == null || cart.Address == null)
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