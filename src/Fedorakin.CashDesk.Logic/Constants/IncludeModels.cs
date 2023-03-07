namespace Fedorakin.CashDesk.Logic.Constants;

public static class IncludeModels
{
    public static class CardNavigation
    {
        public const string Profile = nameof(Profile);
        public const string ProfileWithRole = nameof(ProfileWithRole);
    }

    public static class CartNavigation
    {
        public const string Products = nameof(Products);
    }

    public static class CheckNavigation
    {
        public const string SelfCheckout = nameof(SelfCheckout);
        public const string Card = nameof(Card);
        public const string CardWithProfile = nameof(CardWithProfile);
        public const string CardWithProfileWithRole = nameof(CardWithProfileWithRole);
    }

    public static class ProfileNavigation
    {
        public const string Role = nameof(Role);
    }

    public static class StockNavigation
    {
        public const string Product = nameof(Product);
    }
}
