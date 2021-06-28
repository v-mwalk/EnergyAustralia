Collection of projects for the Energy Australia 'Festivals' test project.  See individual Projects Readme for details.

**FestivalsAPI**
This is direct copy (fork) of the EnergyAustralia project at https://github.com/EATechnology/ea-coding-test-samples/tree/main/web
Have copied this to enable addition of Stubbed/Connected proxy files.  This allows the FestivalsStub API mock to be used when executing 'stubbed' tests of the website..

**FestivalsStub**
Mock of the 'production' Festivals API server to enable testing of the 'target' project isolated from the integration environment.  This allows simple and quick test data injection for project functional/regression testing as well as API interface testing.

**WebTest**
Suite of Functional Regression tests to verify functionality of Festivals project.  Contains BVT tests as well as full functional tests.
BVT tests can be executed with project API Stubbed or connected to API.
Functional tests execute with project API Stubbed to ensure high test coverage and simple execution.
