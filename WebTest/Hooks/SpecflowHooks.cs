using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void AfterScenario(TestContext testContext)
        {
            //
            // If running in debug mode we may not want browser to be killed as we may be debugging.  However in a run-mode we want to kill so that we dont have a tonne of
            // browsers left open!!  Note that in reality we would also be taking screenshots and storing them along with the results! (Passed tests as well as failed for non-repudiation
            // as well to assist in possible future issue investigation (IE. What was this when tested 3 months ago? is a not uncomment type of question....)
            //
#if !DEBUG
            foreach (var process in Process.GetProcessesByName("Chrome"))
            {
                process.Kill();
            }
#endif
        }

    }
}
