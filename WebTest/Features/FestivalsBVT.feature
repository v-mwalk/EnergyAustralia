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

@Regression
@Stubbed
Scenario: Single Band with no festivals
Given Festivals API data is "[{"bands":[{"name":"Critter Girls","recordLabel":"ACR"}]}]"
And i browse to the Festivals page
When the "Festivals" page is viewed
Then the page is populated with exactly 1 band/s
And on the page Band "Critter Girls" is playing at no festivals

@Regression
@Stubbed
Scenario Outline:Single Band with Single Festival - boundary tests (positive)
Note. There are no boundary requirements for this coding test so we will assume the following;
1. Band name shall be minimum one character, maximum 255 characters.  All alphanumerics (UTF-8/ASCII  only), space, punctuations
2. Festival name shall be minimum 3 characters, maximum 255 chars.  All alphanumerics (UTF-8/ASCII  only), space, punctuations
Given Festivals API data is "[{"name":<FestivalName>,"bands":[{"name":<BandName>,"recordLabel":"Doesnt matter"}]}]"
And i browse to the Festivals page
When the "Festivals" page is viewed
Then on the page Band <BandName> is playing at <FestivalName> festival
Examples:
| Test Description                                    | BandName                                                                                                                                                                                                                                                          | FestivalName                                                                                                                                                                                                                                                      |
| Festival Name - All printable ASCII chars and space | "MyBand"                                                                                                                                                                                                                                                          | "! \"#$%&\\\'()*+,-./0123456789:;<=>?@[]^_`{}~\|"                                                                                                                                                                                                                 |
| Band Name - Minimum Length                          | "A"                                                                                                                                                                                                                                                               | "MyFestival"                                                                                                                                                                                                                                                      |
| Band Name - Maximum Length                          | "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" | "MyFestival"                                                                                                                                                                                                                                                      |
| Band Name - All Lowercase                           | "abcdefghijklmnopqrstuvwxyz"                                                                                                                                                                                                                                      | "MyFestival"                                                                                                                                                                                                                                                      |
| Band Name - All Uppercase                           | "ABCDEFGHIJKLMNOPQRSTUVWXYZ"                                                                                                                                                                                                                                      | "MyFestival"                                                                                                                                                                                                                                                      |
| Band Name - All printable ASCII chars and space     | "! \"#$%&\\\'()*+,-./0123456789:;<=>?@[]^_`{}~\|"                                                                                                                                                                                                                 | "MyFestival"                                                                                                                                                                                                                                                      |
| Festival Name - Minimum Length                      | "MyBand"                                                                                                                                                                                                                                                          | "ABC"                                                                                                                                                                                                                                                             |
| Festival Name - Maximum Length                      | "MyBand"                                                                                                                                                                                                                                                          | "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" |
| Festival Name - All Lowercase                       | "MyBand"                                                                                                                                                                                                                                                          | "abcdefghijklmnopqrstuvwxyz"                                                                                                                                                                                                                                      |
| Festival Name - All Uppercase                       | "MyBand"                                                                                                                                                                                                                                                          | "ABCDEFGHIJKLMNOPQRSTUVWXYZ"                                                                                                                                                                                                                                      |

@Regression
@Stubbed
Scenario Outline: No data shown for various API supplied data
Note. There is no specification for the API so we will expect API can supply data in the formats we try
Given Festivals API data is <Raw API Json>
And i browse to the Festivals page
When the "Festivals" page is viewed
Then on the page no Bands are shown
Examples:
| Test description                                            | Raw API Json                                           |
| Minimum empty Json                                          | "[{}]"                                                 |
| No festival and Empty Bands array                           | "[{"bands":[{}]}]"                                     |
| No festival and single band with no name but a record label | "[{"bands":[{"recordLabel":"ACR"}]}]"                  |
| Full single festival one band but empty data                | "[{"name":"","bands":[{"name":"","recordLabel":""}]}]" |
