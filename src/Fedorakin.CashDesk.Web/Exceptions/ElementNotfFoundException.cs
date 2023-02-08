namespace Fedorakin.CashDesk.Web.Exceptions;

public class ElementNotfFoundException : Exception
{
	public ElementNotfFoundException(string message = "Can`t find element(s)")
		: base(message)
	{
	}
}
