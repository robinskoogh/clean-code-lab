using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class FilenameNotSetException : Exception
    {
        public FilenameNotSetException()
        {
        }

        public FilenameNotSetException(string? message) : base(message)
        {
        }

        public FilenameNotSetException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FilenameNotSetException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
