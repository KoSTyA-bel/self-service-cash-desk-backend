namespace Fedorakin.CashDesk.Web.Exceptions;

public class CartEmptyException : Exception
{
	public CartEmptyException(string message = "Cart is empty")
		: base(message)
	{
	}
}
