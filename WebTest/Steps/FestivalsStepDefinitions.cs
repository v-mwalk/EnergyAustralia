using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using WebTest.Drivers;

namespace WebTest.Steps
{
    [Binding]
    public sealed class FestivalsStepDefinitions
    {
 
        private readonly ScenarioContext _scenarioContext;

        public FestivalsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Required Festivals URL is entered in current Browser.
        /// </summary>
        /// <remarks>
        /// URL is obtained from the execution data as this may change depending on environment (IE. Test, Integration, Production, Stubbed, Connected etc....)
        /// </remarks>
        [Given("i browse to the Festivals page")]
        public void GivenPageIsBrowsedTo()
        {
            try
            {
                // Instantiate a new browser
                Utils.NewWebDriver();
                Utils.BrowseToHomePage();
                //                Array.Exists<string>(_scenarioContext.ScenarioInfo.Tags, x => x == "Stubbed")
            }
            catch (Exception ex)
            {
                Assert.Fail("Error browsing to Festivals Page: [{0}]", ex.Message);
            }
        }


        [Given(@"Festivals API data is ""(.*)""")]
        public void GivenFestivalsAPIDataIsNameAsdfBandsNameCritterGirlsRecordLabelACR(string apiJSON)
        {
            Utils.LoadTestData(apiJSON);

        }


        /// <summary>
        /// Waits until the required page is showing in the browser using whatever mechanism to identify that page
        /// </summary>
        /// <remarks>
        /// Method should NOT return until page is showing and is fully populated.  While the coding test 'Festivals' page is very simple, some may
        /// be very complex to wait until settled as an example, there may be graphics to be drawn, panels/textboxes to draw and populate etc.
        /// TeamControlium contains mechanisms to help code this (IE. waits until an element stops moving around/resizing etc....). 
        /// </remarks>
        /// <param name="requiredPage">Name of page to ensure is showing</param>
        [When(@"the ""(.*)"" page is viewed")]
        public void WhenThePageIsViewed(string requiredPage)
        {
            Regex sWhitespace = new Regex(@"\s+");
            switch (sWhitespace.Replace(requiredPage.ToLower(), ""))
            {
                case "festivals":
                    try
                    {
                        string xPath = $"//head/title[contains(translate(.,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'{Utils.GetRunSettingOrFail("FestivalsPageTitle").ToLower()}')]";
                        var w = new WebDriverWait(Utils.WebDriver, TimeSpan.FromSeconds(Int32.Parse(Utils.GetRunSettingOrFail("PageTimeoutSeconds"))));
                        w.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(xPath)));
                        Utils.WebDriver.FindElement(By.XPath(xPath));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("no such element"))
                        {
                            Assert.Fail("Page [{0}] not shown after {1} Seconds", requiredPage, Utils.GetRunSettingOrFail("PageTimeoutSeconds"));
                        }
                        else
                        {
                            Assert.Fail("Error waiting for page [{0}] to be shown: {0}", ex.Message);
                        }
                    }
                    break;
                default:
                    Assert.Fail("ABORT - Page [{0}] not known!",requiredPage);
                    break;
            }
        }



        /// <summary>
        /// Verify that the page contains data.  The data, formatting etc are NOT verified.  Verification is to to verify that at least one band is shown.
        /// </summary>
        [Then("the page is populated with at least one band")]
        public void ThenSomeBandsAreShown()
        {
            var allBandsShowing = Utils.GetAllBandsShowing(TimeSpan.FromMilliseconds(1000));
            Utils.WriteLine("Test - Number of Bands listed({0}) is greater than 0 (zero)", allBandsShowing.Count);
            Assert.IsTrue(allBandsShowing.Count>0,"Number of Bands listed ({0}) is greater than 0 (zero)", allBandsShowing.Count);
        }

        [Then(@"the page is populated with exactly (\d*) band/s")]
        public void ThenThePageIsPopulatedWithExactlyBands(int expectedNumberOfBands)
        {
            var allBandsShowing = Utils.GetAllBandsShowing(TimeSpan.FromMilliseconds(500));
            Utils.WriteLine("Test - Number of Bands listed({0}) is equal to [{1}]", allBandsShowing.Count, expectedNumberOfBands);
            Assert.IsTrue(allBandsShowing.Count == expectedNumberOfBands, "Number of Bands listed ({0}) is is equal to [{1}]", allBandsShowing.Count,expectedNumberOfBands);

        }

        [Then(@"on the page Band (\d*) is named ""(.*)""")]
        public void ThenOnThePageBandIsNamed(int bandIndex, string expectedBandName)
        {
            var allBandsShowing = Utils.GetAllBandsShowing(TimeSpan.FromMilliseconds(500));
            if (allBandsShowing.Count<bandIndex || bandIndex<1)
            {
                Assert.Fail("Abort - Number of bands is [{0}] but band index is [{1}]!!", allBandsShowing.Count, bandIndex);
            }
            else
            {
                string elementText = Utils.GetElementTextWithoutChildTexts(allBandsShowing[0]);
                Utils.WriteLine("Test - Festival[{0}] name({1}) is equal to [{2}]", bandIndex, elementText, expectedBandName);
                Assert.AreEqual(expectedBandName, elementText, "Verify Festival [{0}] name ({1}) is equal to [{2}]", bandIndex, elementText, expectedBandName);
            }
        }

        [Then(@"on the page Band ""(.*)"" is playing at ""(.*)"" festival")]
        public void ThenOnThePageBandIsPlayingAtFestival(string bandName, string expectedFestival)
        {
            string elementText=null;
            string unescapedBandName = Regex.Unescape(bandName);
            string unescapedFestivalName = Regex.Unescape(expectedFestival);
            IWebElement bandElementShowing = default(IWebElement);
            IWebElement festivalElementShowing = default(IWebElement);
            ReadOnlyCollection<IWebElement> festivals = default(ReadOnlyCollection<IWebElement>); ;

            // First find the band we are interested in...
            try
            {
                // Rather than using linq here (IE: bandElement = Utils.GetAllBandsShowing(TimeSpan.FromMilliseconds(500)).FirstOrDefault(band => Utils.GetElementTextWithoutChildTexts(band) == bandName);)
                // we will use a long winded schoolboy way.  Reason is, we want to be able to debug the element texts explicitly which linq wouldnt allow....
                var allBandsShowing = Utils.GetAllBandsShowing(TimeSpan.FromMilliseconds(500));
                foreach (IWebElement bandElement in allBandsShowing)
                {
                    // Get element text.  This is why we no linq.....
                    elementText = Utils.GetElementTextWithoutChildTexts(bandElement);
                    if (elementText== unescapedBandName)
                    {
                        bandElementShowing = bandElement;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Abort - Error finding band [{0}] listed on page: ",ex.Message);
                return;
            }
            
            if (bandElementShowing== default(IWebElement))
            {
                Assert.Fail("Abort - Band [{0}] not listed!",bandName);
                return;
            }

            // get all festivals that band is listed for....
            try
            {
                festivals = bandElementShowing.FindElements(By.XPath("ul/li"));
            }
            catch (Exception ex)
            {
                Assert.Fail("Abort - Error finding festivals band [{0}] is attending: ", ex.Message);
                return;
            }

            // see if festivals contains one we want...
            try
            {
                // Not using linq again, for same reason above....
                var allBandsShowing = Utils.GetAllBandsShowing(TimeSpan.FromMilliseconds(500));
                foreach (IWebElement festivalElement in festivals)
                {
                    // Get element text.  This is why we no linq.....
                    elementText = Utils.GetElementTextWithoutChildTexts(festivalElement);
                    if (elementText == unescapedFestivalName)
                    {
                        festivalElementShowing = festivalElement;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Assert.Fail("Abort - Error finding matching festival [{0}] is attending: ", ex.Message);
                return;
            }
            Utils.WriteLine("Test - Band [{0}] has festival [{1}] listed. Is attending [{2}]", bandName, expectedFestival,String.Join(",",festivals.Select(festival => Utils.GetElementTextWithoutChildTexts(festival))));
            Assert.IsTrue(festivalElementShowing != default(IWebElement), "Test - Band [{0}] has festival [{1}] listed", bandName, expectedFestival);
        }

        [Then(@"on the page Band ""(.*)"" is playing at no festivals")]
        public void ThenOnThePageBandIsPlayingAtNoFestivals(string bandName)
        {
            IWebElement bandElement = default(IWebElement);
            ReadOnlyCollection<IWebElement> festivals = default(ReadOnlyCollection<IWebElement>);

            // First find the band we are interested in...
            try
            {
                bandElement = Utils.GetAllBandsShowing(TimeSpan.FromMilliseconds(500)).FirstOrDefault(band => Utils.GetElementTextWithoutChildTexts(band) == bandName);
            }
            catch (Exception ex)
            {
                Assert.Fail("Abort - Error finding band [{0}] listed on page: ", ex.Message);
                return;
            }

            if (bandElement == default(IWebElement))
            {
                Assert.Fail("Abort - Band [{0}] not listed!");
                return;
            }

            // get all festivals that band is listed for....
            try
            {
                festivals = bandElement.FindElements(By.XPath("ul/li"));
            }
            catch (Exception ex)
            {
                Assert.Fail("Abort - Error finding festivals band [{0}] is attending: ", ex.Message);
                return;
            }


            Utils.WriteLine("Test - Number of Festivals for band ({0}) ({1}) is zero", bandName,festivals.Count);
            Assert.IsTrue(festivals.Count==0, "Number of Festivals for band({0}) ({1}) is zero", bandName,festivals.Count);
        }
    }
}
