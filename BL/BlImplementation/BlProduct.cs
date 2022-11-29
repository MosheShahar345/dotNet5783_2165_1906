
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlProduct : IProduct
{
    private IDal Dal = new DalList();
}
