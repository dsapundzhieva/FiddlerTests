using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FiddlerUITests.Utils;
using FiddlerUITests.Pages;

namespace FiddlerUITests
{
    public class Tests
    {

        string testUrl = "https://dashboard.getfiddler.com/";

        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Dev\FiddlerTests\localDependancies");
            driver.Manage().Window.Maximize();
            driver.Url = testUrl;

            System.Threading.Thread.Sleep(2000);

            LoginPage loginPage = new LoginPage(driver);
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);

            //Accepts the cookies
            driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            createAccountPage.GoToSignIn();
            loginPage.EmailField.SendKeys("");
            loginPage.PassworField.SendKeys("");
            loginPage.SubmitSignInForm();
        }

        [Test]
        public void Test1()
        {
            ApplicationPage overviewPage = new ApplicationPage(driver);
            var tableHeaders = overviewPage.TableHeaders;
            var tableValues = overviewPage.TableValues;

            //Verify parameters describing user’s license plan details are displayed correctly
            Assert.AreEqual(tableHeaders.Count, 5);
            Assert.AreEqual(tableHeaders[0].Text, "Product");
            Assert.AreEqual(tableHeaders[1].Text, "Current Plan");
            Assert.AreEqual(tableHeaders[2].Text, "Total Seats");
            Assert.AreEqual(tableHeaders[3].Text, "Cost");
            Assert.AreEqual(tableHeaders[4].Text, "");

            Assert.AreEqual(tableValues.Count, 5);
            Assert.AreEqual(tableValues[0].Text, "Fiddler Everywhere");
            Assert.AreEqual(tableValues[1].Text, "FREE");
            Assert.AreEqual(tableValues[2].Text, "1");
            Assert.AreEqual(tableValues[3].Text, "-");
        }

        [Test]
        public void Test2()
        {
            ApplicationPage applicationPage = new ApplicationPage(driver);
            SubscriptionPage subscriptionPage = new SubscriptionPage(driver);

            applicationPage.GoToSubscription();
            Assert.IsTrue(subscriptionPage.IsSubscriptionPagePresent());

            subscriptionPage.AddPayment();
            Assert.IsTrue(applicationPage.isTabActive("Saved Cards"));
        }

        [Test]
        public void Test3()
        {
            ApplicationPage applicationPage = new ApplicationPage(driver);
            ProcessOrderPage processOrderPage = new ProcessOrderPage(driver);

            applicationPage.UpgradeToPro();

            Assert.IsTrue(processOrderPage.IsProcessOrderPagePresent());
            Assert.IsTrue(processOrderPage.IsRadioButtonActive("Pay Annually"));

            processOrderPage.SelectPaymentPeriod("Pay Monthly");

            Assert.IsTrue(processOrderPage.IsRadioButtonActive("Pay Monthly"));

            var choosePlan = driver
                .FindElement(By.CssSelector("app-choose-plan"));
            var increaseArrow = choosePlan
                .FindElement(By.CssSelector("form"))
                .FindElement(By.CssSelector("kendo-numerictextbox"))
                .FindElement(By.CssSelector("span[title=\"Increase value\"]"));
            increaseArrow.Click();
            increaseArrow.Click();

            //Clicks Next Button
            driver
                .FindElement(By.XPath("//button[text()='Next']")).Click();

            WaitUntil.LoaderDisappears(driver);

            bool isCardDetailsFormPresent = driver
                .FindElement(By.CssSelector("fdl-card"))
                .FindElement(By.CssSelector("app-pay-order"))
                .FindElement(By.CssSelector("div.k-vbox.payment-details"))
                .FindElements(By.CssSelector("div.k-vbox.details-container")).Count == 1;

            //Verify the card details form is opened
            Assert.IsTrue(isCardDetailsFormPresent);

            //Verify the total charge amount is correct
            var totalChargeAmount = driver
                .FindElement(By.XPath("//h3[text()='Payment Details ']"));

            //Verify the total charge amount is correct
            Assert.AreEqual(totalChargeAmount.Text, "Payment Details (Total Charge: $36.00)");

            var payButtons = driver
                .FindElements(By.XPath("//button//span[text()='Pay $36']"));

            //Verify the total charge amount is correct
            Assert.AreEqual(payButtons.Count, 1);
        }
    }
}