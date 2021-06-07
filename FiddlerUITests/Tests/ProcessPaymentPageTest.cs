using FiddlerUITests.Pages;
using NUnit.Framework;

namespace FiddlerUITests.Tests
{
    class ProcessPaymentPageTest: BaseTest
    {
        [Test]
        public void PayAnnuallyIsDefaulSelectedOnUpdateToPro()
        {
            ApplicationPage applicationPage = new ApplicationPage(driver);
            ProcessOrderPage processOrderPage = new ProcessOrderPage(driver);

            applicationPage.UpgradeToPro();

            Assert.IsTrue(processOrderPage.IsProcessOrderPagePresent());
            Assert.IsTrue(processOrderPage.IsRadioButtonActive("Pay Annually"));
        }

        public void VerifyPaymentDetailsDisplayCorrectly()
        {
            ApplicationPage applicationPage = new ApplicationPage(driver);
            ProcessOrderPage processOrderPage = new ProcessOrderPage(driver);

            applicationPage.UpgradeToPro();
            processOrderPage.SelectPaymentPeriod("Pay Monthly");

            Assert.IsTrue(processOrderPage.IsRadioButtonActive("Pay Monthly"));

            processOrderPage.IncreaseSeats(3);
            processOrderPage.ClickNextToGoToPaymentDetails();

            Assert.IsTrue(processOrderPage.IsPaymentDetailsPresent());
            Assert.IsTrue(processOrderPage.HeaderAmountMatches("$36.00"));
            Assert.IsTrue(processOrderPage.ButtonAmountMatches("$36"));
        }

    }
}
