

namespace BO;

[Serializable]
public class NotEnoughRoomInStockException : Exception
{
    public NotEnoughRoomInStockException() : base("not enough product in stock") { }

    public NotEnoughRoomInStockException(string message) : base(message) { }
    public NotEnoughRoomInStockException(string message, Exception inner) : base(message, inner) { }
    protected NotEnoughRoomInStockException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [Serializable]

    public class InvalidInputException : Exception
    {
        public InvalidInputException() : base("invalid input") { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException (string message, Exception inner) : base(message, inner) { }
        protected InvalidInputException(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        [Serializable]

        public class OrderIsAlreadyShippedException : Exception
        {
            public OrderIsAlreadyShippedException() : base("the order has already been shipped") { }
            public OrderIsAlreadyShippedException(string message) : base(message) { }
            public OrderIsAlreadyShippedException (string message, Exception inner) : base(message, inner) { }
            protected OrderIsAlreadyShippedException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }


    [Serializable]
    public class OrderIsAlreadyDeliverdException : Exception
    {
        public OrderIsAlreadyDeliverdException() { }
        public OrderIsAlreadyDeliverdException(string message) : base(message) { }
        public OrderIsAlreadyDeliverdException(string message, Exception inner) : base(message, inner) { }
        protected OrderIsAlreadyDeliverdException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } 

}
