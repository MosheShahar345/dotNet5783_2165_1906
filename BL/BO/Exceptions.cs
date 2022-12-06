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
public class NotEnoughInStockException : Exception
{
    public NotEnoughInStockException() : base("not enough in stock") { }

    public NotEnoughInStockException(string message) : base(message) { }
    public NotEnoughInStockException(string message, Exception inner) : base(message, inner) { }
    protected NotEnoughInStockException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
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
public class OrderIsAlreadyDeliveredException : Exception
{
    public OrderIsAlreadyDeliveredException() : base("the order has already delivered") { }
    public OrderIsAlreadyDeliveredException(string message) : base(message) { }
    public OrderIsAlreadyDeliveredException(string message, Exception inner) : base(message, inner) { }
    protected OrderIsAlreadyDeliveredException(
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
    public InvalidEmailException() : base("invalid email") { }
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

[Serializable]
public class InvalidAmountException : Exception
{
    public InvalidAmountException() : base("invalid amount") { }
    public InvalidAmountException(string message) : base(message) { }
    public InvalidAmountException(string message, Exception inner) : base(message, inner) { }
    protected InvalidAmountException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}