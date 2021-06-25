using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using WebTest.Drivers;

namespace WebTest.Hooks
{
    [Binding]
    public sealed class SpecflowHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun]
        static public void BeforeTestRun(TestContext testContext)
        {
            Utils.TestContext = testContext;
            Utils.EnsureAllSeleniumDriversClosed();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }

    }
}
