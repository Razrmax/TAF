using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Core.Pages;
using TAF.Tests;

namespace Tests
{
    [AllureNUnit]
    public class PageTests : BaseTest
    {
        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureStory("Can click items")]
        public async Task VerifyPurchaseClicks()
        {
            using (var coffeePage = new CoffeeCartPage(Page))
            {
                int ordersCount = 0;

                await coffeePage.Goto();
                await coffeePage.AddToCart("Espresso");
                ordersCount++;
                await coffeePage.AddToCart("Espresso Macchiato");
                ordersCount++;
                await coffeePage.AddToCart("Cappuccino");
                ordersCount++;

                Assert.IsTrue(await coffeePage.CartContainsOrdersCount(ordersCount));
            }
        }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureStory("Can checkout purchase")]
        [TestCase("Jack", "1@coffee.com")]
        public async Task VerifyCheckoutSuccess(string name, string email)
        {
            using (var coffeePage = new CoffeeCartPage(Page))
            {
                await coffeePage.Goto();
                await coffeePage.AddToCart("Espresso");
                await coffeePage.AddToCart("Espresso Macchiato");
                await coffeePage.AddToCart("Cappuccino");

                await coffeePage.PayForOrder(name, email);
                Assert.IsTrue(await coffeePage.PaymentSuccesful());
            }
        }
    }
}
