namespace Fedorakin.CashDesk.Web.Exceptions;

public class ProductOutOfStockException : Exception
{
	public ProductOutOfStockException(string message = "Product out of stock")
		: base(message)
	{
	}
}
