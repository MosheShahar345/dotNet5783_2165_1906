using BlApi;
using Dal;
using DalApi;
using static BO.NotEnoughRoomInStockException;

namespace BlImplementation;

internal class BlCart : ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddToCart(BO.Cart cart, int productId)
    {
        if (productId <= 0) 
			throw new InvalidInputException();

        DO.Product p;

		try
		{
			p = Dal.Product.GetById(productId);
		}
		catch (Exception e)
		{
			throw new InvalidInputException();
		}

		if (cart.Items.Exists()
		{

		}

    }
}
