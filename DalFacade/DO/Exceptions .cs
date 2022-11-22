namespace DO;

public class NotExistException : Exception
{
    public NotExistException(string message) : base(message) { }
}

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string message) : base(message) { }
}

