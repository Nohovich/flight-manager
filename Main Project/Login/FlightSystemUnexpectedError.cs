using System;
using System.Runtime.Serialization;

namespace Main_Project.Login
{
    [Serializable]
    public class FlightSystemUnexpectedError : Exception
    {
        public FlightSystemUnexpectedError()
        {
        }

        public FlightSystemUnexpectedError(string message) : base(message)
        {
        }

        public FlightSystemUnexpectedError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightSystemUnexpectedError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}