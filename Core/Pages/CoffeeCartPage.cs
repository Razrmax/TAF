using Microsoft.Playwright;

namespace Core.Pages
{
    public class CoffeeCartPage : BasePage
    {
        private ILocator CoffeeLocator(string coffeeType) => page.GetByLabel(coffeeType, new PageGetByLabelOptions { Exact = true} );
        private readonly ILocator _cartButton;
        private readonly ILocator _checkOutBtn;
        private readonly ILocator _paymentDetails;
        private readonly ILocator _checkOutName;
        private readonly ILocator _checkOutEmail;
        private readonly ILocator _sumbitOrderBtn;
        private readonly ILocator _paymentNotification;
        

        public CoffeeCartPage(IPage page) : base(page) 
        {
            _cartButton = page.GetByLabel("Cart page");
            _checkOutBtn = page.GetByLabel("Proceed to checkout");
            _paymentDetails = page.GetByText("Payment details");
            _checkOutName = page.Locator("[id='name']");
            _checkOutEmail = page.Locator("[id='email']");
            _sumbitOrderBtn = page.GetByText("Submit");
            _paymentNotification = page.GetByText("Thanks for your purchase. Please check your email for payment.");
        }

        public async Task Goto()
        {
            await page.GotoAsync("/");
        }

        public async Task AddToCart(string coffeeTtype) => await CoffeeLocator(coffeeTtype).ClickAsync();

        public async Task GoToCheckout() => await _cartButton.ClickAsync();

        public async Task PayForOrder(string name, string email)
        {
            await _cartButton.ClickAsync();
            await _checkOutBtn.ClickAsync();
            await FillOrderCredentials(name, email);        

            await _sumbitOrderBtn.ClickAsync();
        }

        public async Task<bool> PaymentSuccesful() => await _paymentNotification.IsVisibleAsync();

        public async Task<bool> CartContainsOrdersCount(int requiredNumber)
        {
            var result = await _cartButton.TextContentAsync();
            return result.Contains(requiredNumber.ToString());
        }

        private async Task FillOrderCredentials(string name, string email)
        {
            await _checkOutName.FillAsync(name);
            await _checkOutEmail.FillAsync(email);
        }
    }
}
