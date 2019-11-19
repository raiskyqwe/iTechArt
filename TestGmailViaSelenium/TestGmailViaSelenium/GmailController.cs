﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace TestGmailViaSelenium
{
    public class GmailController
    {
        private IWebDriver currentDriver;
        DefaultWait<IWebDriver> fluentWait;
        IWebElement messageBody;
        IWebElement attachFile;
        IWebElement writeMessageButton;

        public void SetDriver(IWebDriver driver)
        {
            this.currentDriver = driver;
        }

        public GmailController(IWebDriver driver)
        {
            this.currentDriver = driver;
        }

        private Dictionary<string, string> GetDictionaryAccounts()
        {
            string path = "../../../Account.txt";

            Dictionary<string, string> accounts = Account.GetAccounts(path);

            return accounts;
        }

        public string GetFirstMail()
        {
            string firstMail;

            Dictionary<string, string> accounts = GetDictionaryAccounts();

            firstMail = accounts.Keys.ElementAt(0);

            return firstMail;
        }

        public string GetFirstPassword()
        {
            string firstPassword;

            Dictionary<string, string> accounts = GetDictionaryAccounts();

            firstPassword = accounts[accounts.Keys.ElementAt(0)];

            return firstPassword;
        }

        public void StartGmail(string mail, string password)
        {
            IWebElement emailField;
            IWebElement emailNextButton;
            IWebElement passwordField;
            IWebElement passwordNextButton;
            string webSiteUrl = "https://gmail.com/";

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            this.currentDriver.Navigate().GoToUrl(webSiteUrl);

            emailField = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Name("identifier")));
            emailField.SendKeys(mail);

            emailNextButton = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Id("identifierNext")));
            emailNextButton.Click();

            passwordField = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Name("password")));
            passwordField.SendKeys(password);

            passwordNextButton = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Id("passwordNext")));
            passwordNextButton.Click();
        }

        public void QuitFromAccount()
        {
            IWebElement account;
            IWebElement logOutFromAccount;

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            account = fluentWait.Until(x => x.FindElement(By.XPath("//a[@class='gb_D gb_Fa gb_i']")));
            account.Click();

            logOutFromAccount = fluentWait.Until(x => x.FindElement(By.XPath("//a[@id='gb_71']")));
            logOutFromAccount.Click();
        }

        public void SerachMessageByTheme(string themeOfMessage)
        {
            IWebElement searchInMailField;
            IWebElement searchButton;
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            searchInMailField = fluentWait.Until(x => x.FindElement(By.XPath("//input[@placeholder='Search mail']")));
            searchInMailField.SendKeys(themeOfMessage);

            searchButton = fluentWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Search Mail']")));
            searchButton.Click();
        }

        public void GetAddOns()
        {
            IWebElement getAddOns;

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            getAddOns = fluentWait.Until(x => x.FindElement(By.XPath("//div[@id='p2DdMb']//div[@class='aT5-aOt-I-JX-Jw']")));
            getAddOns.Click();
        }

        public void OpenNewMessageForm()
        {
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            writeMessageButton = fluentWait.Until(x => x.FindElement(By.XPath("//div[@class='T-I J-J5-Ji T-I-KE L3']")));
            writeMessageButton.Click();
        }

        public void SetRecipientOfMessage(string email)
        {
            IWebElement writeTo;

            writeTo = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Name("to")));
            writeTo.SendKeys(email);
        }

        public void SetThemeOfMessage(string themeOfMessage)
        {
            IWebElement theme;

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            theme = fluentWait.Until(x => x.FindElement(By.Name("subjectbox")));
            theme.SendKeys(themeOfMessage);
        }

        public void SetMessageBody(string messageBody)
        {
            IWebElement body;

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            body = fluentWait.Until(x => x.FindElement(By.XPath("//td[@class='Ap']/div[2]/div[1]")));
            body.SendKeys(messageBody);
        }

        public void SentMessageButton()
        {
            this.messageBody = fluentWait.Until(x => x.FindElement(By.XPath("//td[@class='Ap']/div[2]/div[1]")));
            this.messageBody.SendKeys(Keys.Control + Keys.Enter);
        }

        public void SentMessage(string email, string themeOfMessage,string messageBody)
        {
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            OpenNewMessageForm();
            SetRecipientOfMessage(email);
            SetThemeOfMessage(themeOfMessage);
            SetMessageBody(messageBody);

            SentMessageButton();
        }

        public void SentMessageWithAttachedFile(string email, string themeOfMessage, string messageBody, string pathFile)
        {
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            OpenNewMessageForm();
            SetRecipientOfMessage(email);
            SetThemeOfMessage(themeOfMessage);
            SetMessageBody(messageBody);

            attachFile = fluentWait.Until(x => x.FindElement(By.XPath("//input[@name='Filedata']")));
            attachFile.SendKeys(pathFile);

            SentMessageButton();
        }

        public void SentEmptyMessageForm()
        {
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            OpenNewMessageForm();

            SentMessageButton();
        }

        public void DeleteSentMessagesFrom(string email)
        {
            IWebElement deleteSelectedMessagesButton;
            IList<IWebElement> checkboxes = GetAllMessageCheckbox();
            int countOfMessages;

            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            countOfMessages = GetMessagesFrom(email).Count;

            for (int i = 0; i < countOfMessages; i++)
            {
                checkboxes.ElementAt(i).Click();
            }

            deleteSelectedMessagesButton = currentDriver.FindElement(By.XPath("//div[@class='T-I J-J5-Ji nX T-I-ax7 T-I-Js-Gs mA']"));

            if (deleteSelectedMessagesButton.Displayed)
            {
                deleteSelectedMessagesButton = fluentWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='T-I J-J5-Ji nX T-I-ax7 T-I-Js-Gs mA']")));
                deleteSelectedMessagesButton.Click();
            }
            else
            {
                Console.WriteLine("deleteSelectedMessagesButton is not displayed");
            }
        }

        public IList<IWebElement> GetMessagesFrom(string email)
        {
            //List<IWebElement> listOfFoundMessages = new List<IWebElement>();
            //IList<IWebElement> tempFoundMessages;

            //try
            //{
            //    fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            //    //tempFoundMessages = fluentWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath($"//div[2]/span[@class='bA4']/span[@email='{email}']")));
            //    tempFoundMessages = currentDriver.FindElements(By.XPath($"//div[2]/span[@class='bA4']/span[@email='{email}']"));

            //    foreach (IWebElement message in tempFoundMessages)
            //    {
            //        listOfFoundMessages.Add(message);
            //    }

            //    return listOfFoundMessages;
            //}
            //catch (OpenQA.Selenium.WebDriverTimeoutException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return listOfFoundMessages;
            //}
            IList<IWebElement> listOfFoundMessages;

            listOfFoundMessages = currentDriver.FindElements(By.XPath($"//div[2]/span[@class='bA4']/span[@email='{email}']"));

            return listOfFoundMessages;
        }

        public IList<IWebElement> GetAllMessageCheckbox()
        {
            IList<IWebElement> checkboxes;
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            checkboxes = fluentWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='oZ-jc T-Jo J-J5-Ji ']")));

            return checkboxes;
        }

        public void CloseGmail()
        {
            this.currentDriver.Close();
        }

        public string GetFilePath(string fileName)
        {
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string fileCurrentPath = $@"..\..\..\{fileName}";
            string path = Path.GetFullPath(Path.Combine(currentPath, fileCurrentPath));

            return path;
        }

        public bool IsElementPresent(By by)
        {
            fluentWait = FluentWait.GetFluentWait(this.currentDriver);

            try
            {
                fluentWait.Until(x => x.FindElements(by));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}