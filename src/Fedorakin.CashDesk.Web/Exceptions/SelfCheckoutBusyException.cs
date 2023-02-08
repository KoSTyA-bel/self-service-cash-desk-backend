namespace Fedorakin.CashDesk.Web.Exceptions; 

public class SelfCheckoutBusyException : Exception 
{
    public SelfCheckoutBusyException(string message = "Self checkout is busy")
        : base(message)
    {
    }
}