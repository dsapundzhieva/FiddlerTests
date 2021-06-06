using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace FiddlerUITests
{
    public class Tests
    {

        string testUrl = "https://dashboard.getfiddler.com/";

        IWebDriver driver;
        IWebElement subscriptionCard;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Dev\FiddlerTests\localDependancies");
            driver.Manage().Window.Maximize();

            driver.Url = testUrl;

            System.Threading.Thread.Sleep(2000);

            //Accepts the cookies
            driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            //Clicks the Sing In button
            driver.FindElement(By.XPath("//button/span[text()='Existing User?']")).Click();
            //Fills the email
            driver.FindElement(By.Id("usernameField")).SendKeys("desislava.sapundzhieva@gmail.com");
            //Fills the password
            driver.FindElement(By.Id("passwordField")).SendKeys("Password@123");
            //Submits the form
            driver.FindElement(By.XPath("//button[text()='Sign In']")).Submit();
            //Waits for Dashboard screen to load
            System.Threading.Thread.Sleep(10000);

            subscriptionCard = driver
                .FindElement(By.CssSelector("fdl-card[label=Subscriptions]"));
        }

        [Test]
        public void Test1()
        {
            var tableHeaders = subscriptionCard
                .FindElement(By.TagName("thead"))
                .FindElements(By.TagName("th"));

            Assert.AreEqual(tableHeaders.Count, 5);
            Assert.AreEqual(tableHeaders[0].Text, "Product");
            Assert.AreEqual(tableHeaders[1].Text, "Current Plan");
            Assert.AreEqual(tableHeaders[2].Text, "Total Seats");
            Assert.AreEqual(tableHeaders[3].Text, "Cost");
            Assert.AreEqual(tableHeaders[4].Text, "");

            var tableValues = subscriptionCard.FindElement(By.TagName("tbody")).FindElements(By.TagName("td"));

            Assert.AreEqual(tableValues.Count, 5);
            Assert.AreEqual(tableValues[0].Text, "Fiddler Everywhere");
            Assert.AreEqual(tableValues[1].Text, "FREE");
            Assert.AreEqual(tableValues[2].Text, "1");
            Assert.AreEqual(tableValues[3].Text, "-");
            Assert.AreEqual(tableValues[4].FindElement(By.TagName("a")).Text, "Manage Subscription");
        }

        [Test]
        public void Test2()
        {
            subscriptionCard.FindElement(By.TagName("tbody")).FindElements(By.TagName("td"))[4].FindElement(By.TagName("a")).Click();

            //Waits for Dashboard screen to load
            System.Threading.Thread.Sleep(7000);
            bool isSubPagePresent = driver.FindElements(By.TagName("app-view-subscription")).Count == 1;

            Assert.IsTrue(isSubPagePresent);

            driver
                .FindElement(By.XPath("/html/body/app-root/div/div/div/app-view-subscription/div/fdl-card/div[2]/div[3]/div[2]/a"))
                .Click();

            //Waits for Dashboard screen to load
            System.Threading.Thread.Sleep(7000);

            bool isSaveCardsTabActive = driver
                .FindElement(By.XPath("/html/body/app-root/div/fdl-tabstrip/kendo-tabstrip/ul/li[4]"))
                .GetAttribute("class")
                .Split(" ")
                .Any(className => className == "k-state-active");
            Assert.IsTrue(isSaveCardsTabActive);
        }

        [Test]
        public void Test3()
        {
            //Click Upgrade to Pro now
            driver.FindElement(By.CssSelector("button[title=\"UPGRADE TO PRO NOW\"]")).Click();
            System.Threading.Thread.Sleep(7000);
            bool isProcessOrderPagePresent = driver.FindElements(By.TagName("app-process-order")).Count == 1;
            Assert.IsTrue(isProcessOrderPagePresent);

            var annualPaymentButton = driver
                .FindElement(By.CssSelector("div.period-radio-btn.selected-plan"))
                .FindElement(By.CssSelector("div.plan-details"))
                .FindElements(By.CssSelector("div"))[0].Text;
            Assert.IsTrue(annualPaymentButton.Contains("Pay Annually"));

            //Clicks Pay Monthly 
            var montlyPaymentButton = driver.FindElement(By.CssSelector("div.period-radio-btn.unselected-plan"));                      
            montlyPaymentButton.Click();

            var montlyPaymentButtonSelected = driver
                .FindElement(By.CssSelector("div.period-radio-btn.selected-plan"))
                .FindElement(By.CssSelector("div.plan-details"))
                .FindElements(By.CssSelector("div"))[0].Text;

            Assert.IsTrue(montlyPaymentButtonSelected.Contains("Pay Monthly"));

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

            System.Threading.Thread.Sleep(7000);

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