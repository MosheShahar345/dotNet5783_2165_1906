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
public class OrderHasNotShippedException : Exception
{
    public OrderHasNotShippedException() : base("the order has not shipped") { }
    public OrderHasNotShippedException(string message) : base(message) { }
    public OrderHasNotShippedException(string message, Exception inner) : base(message, inner) { }
    protected OrderHasNotShippedException(
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

[Serializable]
public class InvalidNameException : Exception
{
    public InvalidNameException() : base("invalid name") { }
    public InvalidNameException(string message) : base(message) { }
    public InvalidNameException(string message, Exception inner) : base(message, inner) { }
    protected InvalidNameException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class InvalidPriceException : Exception
{
    public InvalidPriceException() : base("invalid price") { }
    public InvalidPriceException(string message) : base(message) { }
    public InvalidPriceException(string message, Exception inner) : base(message, inner) { }
    protected InvalidPriceException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class InvalidInStockException : Exception
{
    public InvalidInStockException() : base("invalid in stock") { }
    public InvalidInStockException(string message) : base(message) { }
    public InvalidInStockException(string message, Exception inner) : base(message, inner) { }
    protected InvalidInStockException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}


[Serializable]
public class InvalidAddressException : Exception
{
    public InvalidAddressException() : base("invalid address") { }
    public InvalidAddressException(string message) : base(message) { }
    public InvalidAddressException(string message, Exception inner) : base(message, inner) { }
    protected InvalidAddressException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class CanNotDeleteProductException : Exception
{
    public CanNotDeleteProductException() : base("can not delete product") { }
    public CanNotDeleteProductException(string message) : base(message) { }
    public CanNotDeleteProductException(string message, Exception inner) : base(message, inner) { }
    protected CanNotDeleteProductException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class FieldIsEmptyException : Exception
{
    public FieldIsEmptyException() : base("field is empty") { }
    public FieldIsEmptyException(string message) : base(message) { }
    public FieldIsEmptyException(string message, Exception inner) : base(message, inner) { }
    protected FieldIsEmptyException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}