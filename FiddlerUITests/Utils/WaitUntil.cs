using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;



namespace FiddlerUITests.Utils
{
    class WaitUntil
    {
        private static readonly int timeout = 10;

        public static void VisibilityOfElement(IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        public static void ElementIsClickable(IWebDriver driver, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static void InvisibilityOfElement(IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public static void LoaderDisappears(IWebDriver driver)
        {
            InvisibilityOfElement(driver, By.CssSelector("div.loading-indicator"));
        }
    }
}
