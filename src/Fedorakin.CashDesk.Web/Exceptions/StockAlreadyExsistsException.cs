namespace Fedorakin.CashDesk.Web.Exceptions;

public class StockAlreadyExsistsException : Exception
{
	public StockAlreadyExsistsException(string message = "Stock already exisist")
		: base(message)
	{
	}
}
