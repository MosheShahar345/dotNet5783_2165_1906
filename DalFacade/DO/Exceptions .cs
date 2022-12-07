namespace DO;

public class NotExistsException : Exception
{
    public NotExistsException(string message) : base(message) { }
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message) { }
}