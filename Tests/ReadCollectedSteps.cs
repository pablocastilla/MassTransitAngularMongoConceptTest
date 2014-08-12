using System;
using TechTalk.SpecFlow;

namespace Tests
{
    [Binding]
    public class ReadCollectedSteps
    {
        [Given(@"a read has entered in the system")]
public void GivenAReadHasEnteredInTheSystem()
{
    ScenarioContext.Current.Pending();
}

        [Then(@"the read has been stored in database")]
public void ThenTheReadHasBeenStoredInDatabase()
{
    ScenarioContext.Current.Pending();
}
    }
}
