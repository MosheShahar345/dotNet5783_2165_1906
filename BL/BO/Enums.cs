namespace BO;

public enum Category
{
    Suit,
    Pants,
    Shirts,
    Ties,
    Cufflinks
}

public enum OrderStatus
{
    CONFIRMED,
    SENT,
    DELIVERED
}

public enum Availability
{
    INSTUCK,
    OUTOFSTUCK
}

public enum Entities
{
    Exit,
    Cart,
    Order,
    Product
}

public enum CartOpts
{
    Exit,
    Add,
    Update,
    Authorization
}