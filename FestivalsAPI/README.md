# Web App

These instructions and API code is a fork of the Energy Australia Coding Test website located at https://github.com/EATechnology/ea-coding-test-samples/tree/main/web
The fork adds the proxy.conf.stubbed/connected.js files to enable the site to work connected to the test API or to work using a local stub.

## Getting Started

These instructions will help in setting the project and running it up on your local machine for test automation purpose.

### Pre-requisite softwares

Below software should be installed before project setup,

* Node.js

### Project setup

* Open command prompt/powershell/bash/terminal and run the following commands in this folder,

```
npm install
npm install - g @angular/cli
ng serve
```

* Once, the project is built then open the url http://localhost:4200/festivals in browser


**Note** - These instructions will assist you with setup but you are free to explore tools, processes and techniques for setting up the project.

### Customisation for test
Set proxy.conf.js to point to stub or connected API (copy/rename proxy.conf.connected.js or proxy.conf.stubbed.js) as required in testing.  Restart **ng serve** when changing....