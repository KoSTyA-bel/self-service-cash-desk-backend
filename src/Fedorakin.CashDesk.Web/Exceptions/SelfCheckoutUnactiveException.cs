namespace Fedorakin.CashDesk.Web.Exceptions;

public class SelfCheckoutUnactiveException : Exception
{
    public SelfCheckoutUnactiveException(string message = "Self checkout is unactive") 
        : base(message)
    {
    }
}
