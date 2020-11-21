using System;
using System.Runtime.Serialization;

namespace Main_Project.Facade
{
    [Serializable]
    internal class FlightAlreadyDeparturException : Exception
    {
        public FlightAlreadyDeparturException()
        {
        }

        public FlightAlreadyDeparturException(string message) : base(message)
        {
        }

        public FlightAlreadyDeparturException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightAlreadyDeparturException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}