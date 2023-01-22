namespace Dal;
using DalApi;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;

internal class DalProduct : IProduct
{
    const string s_product = @"Product";

    /// <summary>
    /// add a orderItem to xml file
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>


    public int Add(DO.Product product)
    {
        List<DO.Product?> products = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
        if (products.FirstOrDefault(x => x?.ID == product.ID) != null )
            throw new DO.AlreadyExistsException("orderItem alraedy exist");
        products.Add(product);
        XMLTools.SaveListToXNLserializer(products, s_product);
        return product.ID;
    }

    /// <summary>
    /// delete a orderItem from xml file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistsException"></exception>
    public void Delete(int id)
    {
        List<DO.Product?> products = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
        if (products.RemoveAll(x => x?.ID == id) == 0)
            throw new DO.NotExistsException("orderItem do not exist to remove");

        XMLTools.SaveListToXNLserializer(products, s_product);
    }

    public void Update(DO.Product orderItem)
    {
       Delete(orderItem.ID);
       Add(orderItem);
    }

    /// <summary>
    /// receives a orderItem by the id from the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistsException"></exception>
    public DO.Product? GetEntity(Func<DO.Product?, bool>? func)
    {
        List<DO.Product?> products = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
        return products.FirstOrDefault(func!) ?? throw new DO.NotExistsException("not exist");
    }

    /// <summary>
    /// returns all products in the xml file
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func)
    {
        List<DO.Product?> products = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
        if (func == null)
        {
           var list = from item in products
                     select item;
            return list;
        }
        else
        {
            var list = from item in products
                     where (func(item))
                     select item;
            return list;
        }
    }

}
