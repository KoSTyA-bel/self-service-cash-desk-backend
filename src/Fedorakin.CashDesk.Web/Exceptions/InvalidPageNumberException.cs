namespace Fedorakin.CashDesk.Web.Exceptions;

public class InvalidPageNumberException : Exception
{
	public InvalidPageNumberException(string message = "Invalid page number")
		: base(message)
	{
	}
}
