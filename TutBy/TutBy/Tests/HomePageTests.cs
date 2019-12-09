﻿using TutBy.Pages;
using TutBy.Pages.Popups;
using NUnit.Framework;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;
using OpenQA.Selenium.Chrome;

namespace TutBy.Tests
{
    [TestFixture]
    public class HomePageTests : BaseTests
    {
        [Test]
        [AllureTag("TC-2")]
        [AllureSeverity(SeverityLevel.blocker)]
        [AllureOwner("Ramantsevich Dzmitry")]
        [AllureSuite("HomePageTests")]
        public void StartTutBy_IsLogin()
        {
            HomePage page = new HomePage(this.driver);

            this.authorizeFormPopup = homePage.OpenLoginForm();
            authorizeFormPopup.SetLogin(login);
            authorizeFormPopup.SetPassword(password);

            this.homePage = authorizeFormPopup.LoginButtonClick();

            MakeScreenshotWhenFail(page.TopBar, () =>
            {
                Assert.IsTrue(homePage.IsLoggedInAccount());
            });
        }

        [Test]
        [AllureTag("TC-3")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureOwner("Ramantsevich Dzmitry")]
        [AllureSuite("HomePageTests")]
        public void OpenTopBarPanel_IsOpenTopBarPanel()
        {
            TopBarPanel topBarPanel = homePage.OpenTopBarPanel();

            MakeScreenshotWhenFail(topBarPanel.TopBarMore, () =>
            {
                Assert.IsTrue(topBarPanel.IsTopBarPanelOpen());
            });
        }

        [Test]
        [AllureTag("TC-4")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Ramantsevich Dzmitry")]
        [AllureSuite("HomePageTests")]
        public void OpenFinancePage_IsOpenFinancePage()
        {
            FinancePage financePage = homePage.OpenFinancePage();

            MakeScreenshotWhenFail(financePage.WidgetsDiv, () =>
            {
                Assert.IsTrue(financePage.IsDispayed());
            });
        }
    }
}
