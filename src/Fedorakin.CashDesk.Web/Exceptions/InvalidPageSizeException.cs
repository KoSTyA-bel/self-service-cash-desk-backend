namespace Fedorakin.CashDesk.Web.Exceptions;

public class InvalidPageSizeException : Exception
{
    public InvalidPageSizeException(string message = "Invalid page size")
        : base(message)
    {
    }
}
