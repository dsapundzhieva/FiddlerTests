using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using FiddlerUITests.Utils;

namespace FiddlerUITests.Pages
{
    class LoginPage: BasePage
    {
        public LoginPage(IWebDriver driver): base(driver)
        {
        }
        
        [FindsBy(How = How.Id, Using = "usernameField")]
        public IWebElement EmailField { get; set; }

        [FindsBy(How = How.Id, Using = "passwordField")]
        public IWebElement PassworField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Sign In']")]
        private IWebElement SignInButton { get; set; }

        public void SubmitSignInForm()
        {
            SignInButton.Submit();
            WaitUntil.VisibilityOfElement(driver, By.CssSelector("app-account-overview"));
            WaitUntil.LoaderDisappears(driver);
        }

    }
}
