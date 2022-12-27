namespace DO;

[Serializable]
public class NotExistsException : Exception
{
    public NotExistsException(string msg) : base(msg) { }
    public NotExistsException(string msg, Exception ex) : base(msg, ex) { }
}

[Serializable]
public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string msg) : base(msg) { }
    public AlreadyExistsException(string msg, Exception ex) : base(msg, ex) { }
}


[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}