using FiddlerUITests.Utils;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace FiddlerUITests.Pages
{
    class ProcessOrderPage : BasePage
    {
        public ProcessOrderPage(IWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.TagName, Using = "app-process-order")]
        private IList<IWebElement> ProcessOrderElements { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.select-period")]
        private IWebElement PaymentPeriod { get; set; }

        [FindsBy(How = How.CssSelector, Using = "span[title=\"Increase value\"]")]
        private IWebElement IncreaseButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Next']")]
        private IWebElement NextButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.k-vbox.details-container")]
        private IList<IWebElement> PaymentDetailElements { get; set; }

        [FindsBy(How = How.XPath, Using = "//h3[text()='Payment Details ']")]
        private IWebElement HeaderChargeAmount { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@title, 'Pay')]")]
        private IWebElement ButtonPay { get; set; }

        public bool IsProcessOrderPagePresent() => ProcessOrderElements.Count == 1;

        public bool IsRadioButtonActive(string text) =>
            FindPaymentPeriodRadioByText(text)
                    .GetAttribute("class")
                    .Split(" ")
                    .Any(className => className == "selected-plan");

        public void SelectPaymentPeriod(string text)
        {
            FindPaymentPeriodRadioByText(text).Click();
        }

        private IWebElement FindPaymentPeriodRadioByText(string text) =>
            PaymentPeriod
                    .FindElements(By.CssSelector("div.period-radio-btn"))
                    .First(radio =>
                            radio.FindElement(By.CssSelector("div.plan-details"))
                                 .FindElements(By.CssSelector("div"))[0].Text.Contains(text));

        public void IncreaseSeats(int numberOfSeats)
        {
            for (int i = 1; i < numberOfSeats; i++)
            {
                IncreaseButton.Click();
            }
        }

        public void ClickNextToGoToPaymentDetails()
        {
            NextButton.Click();
            WaitUntil.LoaderDisappears(driver);
        }

        public bool IsPaymentDetailsPresent() => PaymentDetailElements.Count == 1;

        public bool HeaderAmountMatches(string totalChargeAmount) => HeaderChargeAmount.FindElement(By.CssSelector("span>span")).Text == totalChargeAmount;

        public bool ButtonAmountMatches(string totalChargeAmount) => ButtonPay.Text.Split(" ")[1] == totalChargeAmount;
    }
}
