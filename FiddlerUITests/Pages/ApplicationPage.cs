using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using FiddlerUITests.Utils;
using System.Linq;

namespace FiddlerUITests.Pages
{
    class ApplicationPage: BasePage
    {
        public ApplicationPage(IWebDriver driver): base(driver)
        {
        }
 
        [FindsBy(How = How.XPath, Using = "//fdl-card[@label='Subscriptions']//thead//th")]
        public IList<IWebElement> TableHeaders { get; set; }


        [FindsBy(How = How.XPath, Using = "//fdl-card[@label='Subscriptions']//tbody//td")]
        public IList<IWebElement> TableValues { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Manage Subscription']")]
        private IWebElement ManageSubscriptionButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//kendo-tabstrip//ul[@role='tablist']")]
        private IWebElement Navigation { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[title=\"UPGRADE TO PRO NOW\"]")]
        private IWebElement UpgradeToProButton { get; set; }

        public bool isTabActive(string tabText)
        {
            return Navigation
                    .FindElements(By.TagName("li"))
                    .First(listItem => listItem.FindElement(By.TagName("a")).Text == tabText)
                    .GetAttribute("class")
                    .Split(" ")
                    .Any(className => className == "k-state-active");
        }

        public void GoToSubscription()
        {
            ManageSubscriptionButton.Click();
            WaitUntil.VisibilityOfElement(driver, By.CssSelector("app-view-subscription"));
            WaitUntil.LoaderDisappears(driver);
        }

        public void UpgradeToPro()
        {
            UpgradeToProButton.Click();
            WaitUntil.VisibilityOfElement(driver, By.CssSelector("app-process-order"));
            WaitUntil.LoaderDisappears(driver);
        }
    }
}
