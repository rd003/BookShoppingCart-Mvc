namespace BookShoppingCartMvcUI.Repositories
{
    public interface ICheckoutRepository
    {
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
