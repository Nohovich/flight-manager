using System;
using System.Runtime.Serialization;

namespace Main_Project.Exceptions
{
    /// <summary>
    /// your are trying to update info of a different account
    /// </summary>
    [Serializable]
    public class UserNameIDAndSecondTableIDNotMatchException : Exception
    {
        public UserNameIDAndSecondTableIDNotMatchException()
        {
        }

        public UserNameIDAndSecondTableIDNotMatchException(string message) : base(message)
        {
        }

        public UserNameIDAndSecondTableIDNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNameIDAndSecondTableIDNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}