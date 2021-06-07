using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiddlerUITests.Pages
{
    class PaymentDetailsPage: BasePage
    {
        public PaymentDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "div.k-vbox.details-container")]
        private IList<IWebElement> PaymentDetailElements { get; set; }

        [FindsBy(How = How.XPath, Using = "//h3[text()='Payment Details ']")]
        private IWebElement HeaderChargeAmount { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@title, 'Pay')]")]
        private IWebElement ButtonPay { get; set; }

        public bool IsPaymentDetailPagePresent() => PaymentDetailElements.Count == 1;

        public bool HeaderAmountMatches(string totalChargeAmount) => HeaderChargeAmount.FindElement(By.CssSelector("span>span")).Text == totalChargeAmount;

        public bool ButtonAmountMatches(string totalChargeAmount) => ButtonPay.Text.Split(" ")[1] == totalChargeAmount;
        
    }
}
