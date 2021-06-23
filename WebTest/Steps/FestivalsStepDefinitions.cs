using TechTalk.SpecFlow;

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
            _scenarioContext.Pending();
        }

        /// <summary>
        /// Verify that the page contains data.  The data, formatting etc are NOT verified.  Verification is to to verify that at least one festival is shown.
        /// </summary>
        [Then("the page is populated with some festivals")]
        public void TheSomeFestivalsAreShown()
        {
            _scenarioContext.Pending();
        }
    }
}
