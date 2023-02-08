namespace Fedorakin.CashDesk.Web.Exceptions;

public class SelfCheckoutFreeException : Exception
{
	public SelfCheckoutFreeException(string message = "Self checkout is free")
		: base(message)
	{
	}
}
