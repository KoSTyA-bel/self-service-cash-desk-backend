namespace Fedorakin.CashDesk.Web.Exceptions;

public class ElementNotFoundException : Exception
{
	public ElementNotFoundException(string message = "Can`t find element(s)")
		: base(message)
	{
	}
}
