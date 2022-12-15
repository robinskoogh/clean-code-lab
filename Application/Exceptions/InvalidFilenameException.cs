using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class InvalidFilenameException : Exception
    {
        public InvalidFilenameException()
        {
        }

        public InvalidFilenameException(string? message) : base(message)
        {
        }

        public InvalidFilenameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidFilenameException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
