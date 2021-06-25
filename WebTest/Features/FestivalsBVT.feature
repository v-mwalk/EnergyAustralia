Feature: FestivalsBVT
	Basic Validation Tests to verify Festivals page is functioning.  Correctness of functioning is NOT tested.
	***Background***: **[Original definition for creation of this test suite](https://eacp.energyaustralia.com.au/codingtest/testers**

Background: Browser is launched and Festivals page browsed to

@BVT
Scenario: Page is populated with at least one band
Given i browse to the Festivals page
When the "Festivals" page is viewed
Then the page is populated with at least one band

@BVT
@Stubbed
Scenario: Single Band with one festival shows correctly
Given Festivals API data is "[{"name":"Reading festival","bands":[{"name":"Critter Girls","recordLabel":"ACR"}]}]"
And i browse to the Festivals page
When the "Festivals" page is viewed
Then the page is populated with exactly 1 band/s
And on the page Band 1 is named "Critter Girls"
And on the page Band "Critter Girls" is playing at "Reading festival" festival

