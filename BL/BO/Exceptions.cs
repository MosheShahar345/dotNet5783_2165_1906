namespace BO;

[Serializable]
public class IdIsLessThanZeroException : Exception
{
    public IdIsLessThanZeroException() : base("ID is less than zero") { }
    public IdIsLessThanZeroException(string message) : base(message) { }
    public IdIsLessThanZeroException(string message, Exception inner) : base(message, inner) { }
    protected IdIsLessThanZeroException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotEnoughRoomInStockException : Exception
{
    public NotEnoughRoomInStockException() : base("not enough product in stock") { }

    public NotEnoughRoomInStockException(string message) : base(message) { }
    public NotEnoughRoomInStockException(string message, Exception inner) : base(message, inner) { }
    protected NotEnoughRoomInStockException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]

public class InvalidInputException : Exception
{
    public InvalidInputException() : base("invalid input") { }
    public InvalidInputException(string message) : base(message) { }
    public InvalidInputException(string message, Exception inner) : base(message, inner) { }
    protected InvalidInputException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]

public class OrderIsAlreadyShippedException : Exception
{
    public OrderIsAlreadyShippedException() : base("the order has already shipped") { }
    public OrderIsAlreadyShippedException(string message) : base(message) { }
    public OrderIsAlreadyShippedException(string message, Exception inner) : base(message, inner) { }
    protected OrderIsAlreadyShippedException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}


[Serializable]
public class OrderIsAlreadyDeliverdException : Exception
{
    public OrderIsAlreadyDeliverdException() : base("the order has already deliverd") { }
    public OrderIsAlreadyDeliverdException(string message) : base(message) { }
    public OrderIsAlreadyDeliverdException(string message, Exception inner) : base(message, inner) { }
    protected OrderIsAlreadyDeliverdException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotExistsException : Exception
{
    public NotExistsException() : base("not exists") { }
    public NotExistsException(string message) : base(message) { }
    public NotExistsException(string message, Exception inner) : base(message, inner) { }
    protected NotExistsException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class InvalidEmailException : Exception
{
    public InvalidEmailException() : base("invalid email exception") { }
    public InvalidEmailException(string message) : base(message) { }
    public InvalidEmailException(string message, Exception inner) : base(message, inner) { }
    protected InvalidEmailException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class AlreadyExistsException : Exception
{
    public AlreadyExistsException() : base("already exists") { }
    public AlreadyExistsException(string message) : base(message) { }
    public AlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    protected AlreadyExistsException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}