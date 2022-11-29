
using BlApi;


namespace BlImplementation;

sealed public class Bl : IBl
{
    public ICart Cart { get; set;} 
    public IOrder Order { get; set; }
    public IProduct Product { get; set;}

}

