Festivals Stub is a mock of the Energy Australia 'Festivals' API.  The stub behaves exactly the same as the API at https://eacp.energyaustralia.com.au/codingtest/api-docs/ but enables Testers/automation to inject test data as required.

To use;

- Download Release/FestivalsStup.zip and copy to new folder.

- Execute FestivalsStub.exe

To use.

Configure Project to use exposed API (IE. Instead of https://eacp.energyaustralia.com.au/codingtest/api/v1/festivals use http://localhost:9000/codingtest/api/v1/festivals

Use Data Injection API (at http://localhost:9000/codingtest/api/v1/DataInjection ) to load data that .../festivals GET will return.

