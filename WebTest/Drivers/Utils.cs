using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace WebTest.Drivers
{
    public class Utils
    {
        static public TestContext TestContext { set; get; }
        static public string Browser
        {
            get
            {
                return GetRunSettingOrFail("Browser");
            }
        }


        static public IWebDriver WebDriver { set; get; }
        static public void EnsureAllSeleniumDriversClosed()
        {
            foreach (var process in Process.GetProcessesByName("chromedriver.exe"))
            {
                process.Kill();
            }
        }

        static public void NewWebDriver()
        {
            switch (Browser.ToLower())
            {
                case "chrome":
                    WebDriver = new ChromeDriver();
                    break;
                case "edge":
                    WebDriver = new EdgeDriver();
                    break;
                case "firefox":
                    WebDriver = new FirefoxDriver();
                    break;
                default:
                    if (String.IsNullOrEmpty(Browser))
                    {
                        Assert.Fail("Browser not set!");
                    }
                    else
                    {
                        Assert.Fail("Browser [{0}] not supported.  Only Chrome, Edge or Firefox browsers supported");
                    }
                    break;
            }
        }

        static public void BrowseToHomePage()
        {
            WebDriver.Navigate().GoToUrl(GetRunSettingOrFail("HomeURL"));
        }
        static public void Arrse(bool useStub)
        {
            if (useStub)
            {
                WebDriver.Navigate().GoToUrl(GetRunSettingOrFail("APIStubHomeURL"));
            }
            else
            {
                WebDriver.Navigate().GoToUrl(GetRunSettingOrFail("APIHomeURL"));
            }
        }

        static public string GetRunSettingOrFail(string key)
        {
            if (!TestContext.Properties.Contains(key))
            {
                Assert.Fail("WebTest.runsettings file does not contain Parameter [\"{0}\"] in TestRunParameters.", key);
                return null;
            }
            try
            {
                return TestContext.Properties[key].ToString();
            }
            catch (Exception ex)
            {
                Assert.Fail("Error getting Run setting [{0}]: [{1}].", key,ex.Message);
                return null;
            }
        }


        static public ReadOnlyCollection<IWebElement> GetAllBandsShowing(TimeSpan timeout)
        {
            ReadOnlyCollection<IWebElement> bandsListElements = null;
            int lastCount = 0;
            int thisCount = 0;
            DateTime timeStart = DateTime.Now;
            TimeSpan listUpdateTimeout = timeout;

            var outerListElement = Utils.WebDriver.FindElement(By.XPath(@"//app-festivals/ol"));

            while ((DateTime.Now - timeStart) < listUpdateTimeout)
            {
                bandsListElements = outerListElement.FindElements(By.XPath("./li"));
                lastCount = thisCount;
                thisCount = bandsListElements.Count;
                if (lastCount != thisCount)
                {
                    timeStart = DateTime.Now;
                }
            }
            return bandsListElements;
        }

        static public void LoadTestData(string jsonString)
        {
            try
            {
                string stubURL = GetRunSettingOrFail("StubURL");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(stubURL);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Assert.Fail("Abort - Error loading test data [{0}]: {1}", jsonString, ex.Message);
            }
        }


        static public string GetElementTextWithoutChildTexts(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Utils.WebDriver; 
            var jsInjection = "if(arguments[0].hasChildNodes()){var r='';var C=arguments[0].childNodes;for(var n=0;n<C.length;n++){if(C[n].nodeType==Node.TEXT_NODE){r+=' '+C[n].nodeValue}}return r.trim()}else{return arguments[0].innerText}";

            try
            {
                return (string)js.ExecuteScript(jsInjection, element);
            }
            catch (Exception ex)
            {
                Assert.Fail("Abort - Error executing Javascript injection to get element text: {0}", ex.Message);
                return null;
            }
        }

        static public void WriteLine(string text, params object[] args)
        {
            TestContext.WriteLine(text, args);
        }


        static public void TakeScreenshot(string title)
        {
            Screenshot ss = ((ITakesScreenshot)WebDriver).GetScreenshot();
            string Runname = title + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            string screenshotfilename = ".\\screenshots\\" + Runname + ".jpg";
            ss.SaveAsFile(screenshotfilename, ScreenshotImageFormat.Jpeg);
        }

        static public void MyElementClickedHandler(object sender, WebElementEventArgs e)
        {
            Console.WriteLine("Clicked");
        }
    }
}
