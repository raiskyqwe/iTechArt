﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;

namespace TestGmailViaSelenium
{
    class GmailMessagesTests
    {
        private IWebElement foundMessage;
        private IWebDriver currentDriver;
        private GmailActionsController gmailController;
        DefaultWait<IWebDriver> fluentWait;
        string firstMail;
        string firstPassword;
        string themeOfMessage;

        [SetUp]
        public void SetUp()
        {

            this.currentDriver = new ChromeDriver();

            this.gmailController = new GmailActionsController(this.currentDriver);

            this.firstMail = gmailController.SetFirstMail();
            this.firstPassword = gmailController.SetFirstPassword();

            gmailController.StartGmail(firstMail, firstPassword);
        }

        [Test]
        public void SentMessage()
        {
            themeOfMessage = "SentMessage";
              
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            gmailController.SentMessage(this.firstMail, themeOfMessage);

            foundMessage = fluentWait.Until(x => x.FindElement(By.XPath($"//span[contains(text(),'{themeOfMessage}')]")));

            Assert.IsNotNull(foundMessage);
        }

        [Test]
        public void SentMessageWithAttachedFile_SentCorrectFileExtension()
        {
            themeOfMessage = "Message with attached file";
            string fileName = "Account.txt";
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string fileCurrentPath = $@"..\..\..\{fileName}";
            string path = Path.GetFullPath(Path.Combine(currentPath, fileCurrentPath));

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            gmailController.SentMessageWithAttachedFile(this.firstMail, themeOfMessage, path);

            foundMessage = fluentWait.Until(x => x.FindElement(By.XPath($"//span[contains(text(),'{fileName}')]")));

            Assert.IsNotNull(foundMessage);
        }

        [Test]
        public void SentMessageWithAttachedFile_SentIncorrectFileExtension()
        {
            themeOfMessage = "Message with incorrect file extension";
            string fileName = "iTechArt.7z";
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string fileCurrentPath = $@"..\..\..\{fileName}";
            string path = Path.GetFullPath(Path.Combine(currentPath, fileCurrentPath));

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            gmailController.SentMessageWithAttachedFile(this.firstMail, themeOfMessage, path);

            IAlert alert = fluentWait.Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();

            foundMessage = fluentWait.Until(x => x.FindElement(By.XPath($"//span[contains(text(),'{themeOfMessage}')]")));

            Assert.IsNotNull(foundMessage);
        }

        [Test]
        public void SentEmptyMessageForm_IsDisplayedErrorWindow()
        {
            IWebElement errorForm;

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            gmailController.SentEmptyMessageForm();

            errorForm = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='Kj-JD-K7 Kj-JD-K7-bsT']")));
            
            Assert.IsTrue(errorForm.Displayed);
        }

        [Test]
        public void DeleteSentMessagesFrom_DeleteAllMessagesFromGivenEmail()
        {
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            try
            {
                gmailController.DeleteSentMessagesFrom(this.firstMail);

                bool isFoundMessage = fluentWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath($"//div[2]/span[@class='bA4']/span[@email='{this.firstMail}']")));

                Thread.Sleep(1000);
                Assert.IsTrue(isFoundMessage);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "The operation has timed out.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            gmailController.CloseGmail();
        }
    }
}
