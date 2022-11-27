namespace DO;

public class DoesNotExistException : Exception
{
    public DoesNotExistException(string message) : base(message) { }
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message) { }
}

