using FiddlerUITests.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace FiddlerUITests.Tests
{
    class BaseTest
    {
        private string testUrl = "https://dashboard.getfiddler.com/";
        private string email = "testfiddler@yahoo.com";
        private string password = "testfiddler@123";

        protected ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = testUrl;

            LoginPage loginPage = new LoginPage(driver);
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            ApplicationPage applicationPage = new ApplicationPage(driver);

            createAccountPage.AcceptCoockiesButton();
            createAccountPage.GoToSignIn();
            loginPage.EmailField.SendKeys(email);
            loginPage.PassworField.SendKeys(password);
            loginPage.SubmitSignInForm();
        }

        [TearDown]
        public void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
