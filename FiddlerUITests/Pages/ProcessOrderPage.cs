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

    }
}
