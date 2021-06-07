using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using FiddlerUITests.Utils;

namespace FiddlerUITests.Pages
{
    class SubscriptionPage: BasePage
    {
        public SubscriptionPage(IWebDriver driver): base(driver)
        {

        }

        [FindsBy(How = How.TagName, Using = "app-view-subscription")]
        private IList<IWebElement> AppSubscriptionElements { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Add']")]
        private IWebElement AddPaymentButton { get; set; }

        public bool IsSubscriptionPagePresent() => AppSubscriptionElements.Count == 1;

        public void AddPayment()
        {
            WaitUntil.ElementIsClickable(driver, AddPaymentButton);
            AddPaymentButton.Click();
        }
    }
}
