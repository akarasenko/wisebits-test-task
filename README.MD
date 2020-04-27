INFO

The project contains 5 UI autotests for https://www.w3schools.com/sql/trysql.asp?filename=trysql_select_all.

SETUP

There are a way to run tests via Microsoft Visual Studio Comminity:

1. Download and install Visual Studio Community 2019 Version 16.5.4 https://visualstudio.microsoft.com/ru/vs/
2. Open test project: File -> Open -> Project / Solution -> choose file wisebits-test-task.sln in root directory 
3. Download chromedriver.exe https://chromedriver.chromium.org/downloads and add its directory to PATH variable or to \wisebits-test-task\wisebits-test-task\bin\Debug\netcoreapp3.1 folder (\bin folder is created after first build). Be sure Chrome Driver version fits version of your Chrome Browser
4. Setup path to configuration file test.runsettings: Test -> Configure run settings -> Set Solution Wide Runsettings file  -> choose \wisebits-test-task\wisebits-test-task\test.runsettings
5. Open Test Explorer: Test -> Test explorer
6. Run test by clicking on Run all tests (green play button)

Run results are shown in Test Explorer.

STRUCTURE DESCRIPTION

The project contains 5 UI autotests for https://www.w3schools.com/sql/trysql.asp?filename=trysql_select_all.
Tests are developed with C# / MS Test Framework / Selenium WebDriver.
Test are designed to run in Google Chrome.

Test Test5InvalidSqlQuery allows to check alert window apears when try to run invalid SQL query.

DbModel class is used to store DB data. It consists of 7 properties that describe fields of record. If field value is not set in DB corresponding properties has value string "null". If we select not all record fields (SQL query like "SELECT CustomerID, City, Country from Customers") properties that are not included in result record has value NULL.

Each test class inherits PageHelper class that contains methods to work with UI elements on page e.g. insert SQL query into field, click run button or read result table.

PageHelper class inherits DriverHelper class that contins mentods to interact with browser e.g. initialize chromedriver, close it after test finishes or swicth to alert window. 

Now tests can be run manually from Vusial Studio Community (or with teh help of vstest.console) and test results are shown in Test Exporer. Later some CI/CD systems e.g. Jenkins and some reporting servises e.g. Allure can be used to run tests automatically.