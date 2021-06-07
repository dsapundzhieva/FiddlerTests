using FiddlerUITests.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace FiddlerUITests.Tests
{
    class BaseTest
    {
        string testUrl = "https://dashboard.getfiddler.com/";

        protected ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Dev\FiddlerTests\localDependancies");
            driver.Manage().Window.Maximize();
            driver.Url = testUrl;

            LoginPage loginPage = new LoginPage(driver);
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            ApplicationPage applicationPage = new ApplicationPage(driver);

            createAccountPage.AcceptCoockiesButton();
            createAccountPage.GoToSignIn();
            loginPage.EmailField.SendKeys("");
            loginPage.PassworField.SendKeys("");
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
