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
            driver.FindElement(By.XPath("//*[@id='second']/div/div[8]/div[4]/button/span[2]")).Click();
            //Fills the email
            driver.FindElement(By.Id("usernameField")).SendKeys("");
            //Fills the password
            driver.FindElement(By.Id("passwordField")).SendKeys("");
            //Submits the form
            driver.FindElement(By.XPath("//*[@id='second']/div/div[3]/div[1]/button")).Submit();
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
            System.Threading.Thread.Sleep(7000);
            bool isSubPagePresent = driver.FindElements(By.TagName("app-view-subscription")).Count == 1;

            Assert.IsTrue(isSubPagePresent);

            driver
                .FindElement(By.XPath("/html/body/app-root/div/div/div/app-view-subscription/div/fdl-card/div[2]/div[3]/div[2]/a"))
                .Click();
            System.Threading.Thread.Sleep(7000);

            bool isSaveCardsTabActive = driver
                .FindElement(By.XPath("/html/body/app-root/div/fdl-tabstrip/kendo-tabstrip/ul/li[4]"))
                .GetAttribute("class")
                .Split(" ")
                .Any(className => className == "k-state-active");
            Assert.IsTrue(isSaveCardsTabActive);
        }
    }
}
