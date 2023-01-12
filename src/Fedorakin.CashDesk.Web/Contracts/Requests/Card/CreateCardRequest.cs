﻿namespace Fedorakin.CashDesk.Web.Contracts.Requests.Card;

public class CreateCardRequest
{
    public string Code { get; set; } = string.Empty;

    public double Total { get; set; }

    public double Discount { get; set; }

    public int ProfileId { get; set; }
}
