
using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrder : IOrder
{
    private IDal Dal = new DalList();
    public IEnumerable<BO.OrderForList> GetOrderForLists()
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

		}
	
    }

}
