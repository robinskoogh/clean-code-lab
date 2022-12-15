namespace Application.Exceptions
{
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
    }
}
