namespace Application.Exceptions
{
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
    }
}
