using FiddlerUITests.Utils;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FiddlerUITests.Pages
{
    class CreateAccountPage: BasePage
    {
        public CreateAccountPage(IWebDriver driver): base(driver)
        {
        }

        [FindsBy(How = How.XPath, Using = "//button/span[text()='Existing User?']")]
        private IWebElement GoToSignInButton { get; set; }

        [FindsBy(How = How.Id, Using = "onetrust-accept-btn-handler")]
        public IWebElement AcceptCoockies { get; set; }

        public void GoToSignIn()
        {
            GoToSignInButton.Click();
        }

        public void AcceptCoockiesButton()
        {
            WaitUntil.ElementIsClickable(driver, AcceptCoockies);
            AcceptCoockies.Click();
        }
    }
}
