using System.ComponentModel.DataAnnotations;
using DalApi;

namespace BlImplementation;

internal class BlCart : BlApi.ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get(); // to access DO entities

    /// <summary>
    /// gets cart and id adds a product that matches the id and returns the cart 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    /// <exception cref="BO.NotEnoughInStockException"></exception>
    public BO.Cart AddPToCart(BO.Cart cart, int productId)
    {
        if (productId < 0)
            throw new BO.IdIsLessThanZeroException();

        DO.Product? product;
        BO.OrderItem orderItem;
        
        if (cart.Items == null)
        {
            cart.Items = new();
        }

        orderItem = cart.Items.Find(it => it?.ProductID == productId)!;

        try
        {
            product = dal?.Product.GetEntity(it => it?.ID == productId);
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        if (orderItem != null)
        {
            if (product?.InStock > orderItem.Amount + 1)
            {
                orderItem.Amount++;
                orderItem.TotalPrice += orderItem.Price;
                cart.TotalPrice += orderItem.Price;
            }
        }
        else if (product?.InStock > 0)
        {
            cart.Items.Add(new BO.OrderItem
            {
                ID = 0,
                Name = product?.Name,
                ProductID = (int)(product?.ID)!,
                Price = (int)(product?.Price)!,
                Amount = 1,
                TotalPrice = (int)(product?.Price)! 
            });
        }
        else
        {
            throw new BO.NotEnoughInStockException(
                $"product with name: {product?.Name} not enough in stock");
        }

        return cart;
    }

    /// <summary>
    /// gets cart id and new amount updates a product that matches the id
    /// to have the new given amount and returns the cart 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="nAmount"></param>
    /// <returns></returns>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.InvalidAmountException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    /// <exception cref="BO.NotEnoughInStockException"></exception>
    public BO.Cart UpdateAmount(BO.Cart cart, int productId, int nAmount)
    {
        if (productId < 0)
            throw new BO.IdIsLessThanZeroException();

        if (nAmount < 0)
            throw new BO.InvalidAmountException();

        DO.Product? product = new DO.Product();
        BO.OrderItem orderItem;

        try
        {
            product = dal?.Product.GetEntity(it => it?.ID == productId);
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        orderItem = cart.Items?.Find(it => productId == it?.ProductID) ??
                    throw new BO.NotExistsException();

        if (nAmount == orderItem.Amount) { return cart; }

        if (nAmount > orderItem.Amount)
        {
            if (product?.InStock >= orderItem.Amount + nAmount)
            {
                orderItem.Amount += nAmount;
                orderItem.TotalPrice += orderItem.Price * nAmount;
                cart.TotalPrice += orderItem.TotalPrice;
            }
            else
            {
                throw new BO.NotEnoughInStockException(
                    $"product with name: {product?.Name} not enough in stock");
            }
        }

        else if (nAmount != 0 && nAmount < orderItem.Amount)
        {
            orderItem.Amount -= nAmount;
            orderItem.TotalPrice -= orderItem.Price * nAmount;
            cart.TotalPrice -= orderItem.TotalPrice;
        }

        else
        {
            cart.TotalPrice -= orderItem.TotalPrice;
            cart.Items.Remove(orderItem);
        }

        return cart;
    }

    /// <summary>
    /// confirm the order in the cart and finishes the purchase
    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="BO.NotExistsException"></exception>
    /// <exception cref="BO.InvalidAddressException"></exception>
    /// <exception cref="BO.InvalidNameException"></exception>
    /// <exception cref="BO.InvalidEmailException"></exception>
    /// <exception cref="BO.AlreadyExistsException"></exception>
    public void ConfirmationOrder(BO.Cart cart)
    {
        _ = cart?.Items ?? throw new BO.NotExistsException();

        if (cart.Address == null)
            throw new BO.InvalidAddressException();

        if (cart.Name == null)
            throw new BO.InvalidNameException();

        if (!new EmailAddressAttribute().IsValid(cart.Email))
            throw new BO.InvalidEmailException();

        DO.Order dOrder = new DO.Order()
        {
            CustomerName = cart.Name,
            CustomerAddress = cart.Address,
            CustomerEmail = cart.Email,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null
        };

        int OrderId;

        try
        {
            OrderId = dal!.Order.Add(dOrder);
        }
        catch (DO.AlreadyExistsException e)
        {
            throw new BO.AlreadyExistsException("", e);
        }

        foreach (var item in cart.Items)
        {
            DO.OrderItem dOrderItem = new DO.OrderItem()
            {
                ID = item!.ID,
                ProductID = item.ProductID,
                OrderID = OrderId,
                Price = item.Price,
                Amount = item.Amount
            };

            try
            {
                dal?.OrderItem.Add(dOrderItem);
            }
            catch (DO.AlreadyExistsException e)
            {
                throw new BO.AlreadyExistsException("", e);
            }
        }

        DO.Product product = new DO.Product();

        foreach (var item in cart.Items)
        {
            product = (DO.Product)dal?.Product.GetEntity(it => it?.ID == item!.ProductID)!;
            product.InStock -= item!.Amount;
            dal?.Product.Update(product);
        }
    }
}