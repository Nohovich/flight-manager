using System;
using System.Runtime.Serialization;

namespace Main_Project.Exceptions
{ /// <summary>
/// there isn't enough tickets for the customer to purchase
/// </summary>
    [Serializable]
    public class FlightIsFullException : Exception
    {
        public FlightIsFullException()
        {
        }

        public FlightIsFullException(string message) : base(message)
        {
        }

        public FlightIsFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightIsFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}