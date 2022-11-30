using BlApi;
using Dal;
using DalApi;
using System.Reflection.Metadata.Ecma335;
using static BO.NotEnoughRoomInStockException;

namespace BlImplementation;

internal class BlOrder : IOrder
{
    private IDal Dal = new DalList();
    public IEnumerable<BO.OrderForList> GetOrderForList()
    {
        List<DO.Order> orders = new List<DO.Order>();
        List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();

		try
		{
			orders = Dal.Order.GetAll().ToList();
		}
		catch (Exception) { }

		foreach (var item in orders)
		{
			BO.OrderForList order = new BO.OrderForList()
			{
				ID = item.ID,
				CoustomerName = item.CustomerName,
				Status = GetStatus(item)
            };
			List<DO.OrderItem> orderitems = Dal.OrderItem.GetOrderItem(item.ID);
			foreach (var item2 in orderitems)
			{
				order.AmountOfItems += item2.Amount;
				order.TotalPrice += item2.Price * item2.Amount;
			}
			ordersForList.Add(order);
		}
		return ordersForList;
    }
	public BO.Order GetOrder(int orderId)
	{
        if (orderId <= 0)
            throw new InvalidInputException();

        DO.Order o;
        DO.OrderItem oI;

        try
        {
            o = Dal.Order.GetById(orderId);
            oI = Dal.OrderItem.GetById(orderId);
        }
        catch (Exception e)
        {
            throw new InvalidInputException();
        }

		BO.Order oB = new BO.Order()
		{
			ID=o.ID,

		};
        return oB;
    }
	private 
	
}
