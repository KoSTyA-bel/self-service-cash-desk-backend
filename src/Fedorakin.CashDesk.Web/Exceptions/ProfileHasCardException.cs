namespace Fedorakin.CashDesk.Web.Exceptions;

public class ProfileHasCardException : Exception
{
	public ProfileHasCardException(string message = "Profile already has a card")
		: base(message)
	{
	}
}
