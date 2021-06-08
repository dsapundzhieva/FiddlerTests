using FiddlerUITests.Pages;
using NUnit.Framework;

namespace FiddlerUITests.Tests
{
    class ApplicationPageTest: BaseTest
    {
        [Test]
        public void VerifyUserLincenseDetails()
        {
            ApplicationPage applicationPage = new ApplicationPage(driver);
            var tableHeaders = applicationPage.TableHeaders;
            var tableValues = applicationPage.TableValues;

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
        public void VerifySubscriptionPageOpensOnManageSubscriptions()
        {
            ApplicationPage applicationPage = new ApplicationPage(driver);
            SubscriptionPage subscriptionPage = new SubscriptionPage(driver);

            applicationPage.GoToSubscription();

            Assert.IsTrue(subscriptionPage.IsSubscriptionPagePresent());
        }

        [Test]
        public void VerifySaveCardTabIsActiveOnAddPayment()
        {
            ApplicationPage applicationPage = new ApplicationPage(driver);
            SubscriptionPage subscriptionPage = new SubscriptionPage(driver);

            applicationPage.GoToSubscription();
            subscriptionPage.AddPayment();

            Assert.IsTrue(applicationPage.isTabActive("Saved Cards"));
        }
    }
}
