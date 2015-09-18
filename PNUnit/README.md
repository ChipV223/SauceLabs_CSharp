## Running Tests in Parallel with C# Using PNUnit on Sauce Labs:

PNUnit, which stands for “Parallel NUnit,” is an extension of NUnit that allows NUnit tests to run in parallel using a special .conf configuration file that specifies the tests to be executed and where they should run, whether on the same machine or on another machine on the network. For more information and documentation about the framework, as well as how to use it in your testing, you can visit the [official PNUnit website](http://www.nunit.org/index.php?p=pnunit&r=2.5).

### Prerequisites

You’ll need to have these components installed to set up PNUnit testing on Sauce with C# .NET:

  1) Visual Studio
  
  2) Selenium DLLs for .NET installed and referenced by your project
  
  3) NUnit + PNUnit Bundle

####Create the Visual Studio Project:

1) Open a new project in Visual Studio.
NOTE: To get PNUnit to work for your project, you will need to set the .Net Framework version to 3.5. Click the list of framework versions at the top of the New Project dialog and select .NET Framework 3.5.

2) Select a C# class library template.

3) Give the project a name and click OK.

####Install the  Selenium DLLs:

After you’ve set up your project in Visual Studio, you need to make sure that it references the required Selenium DLLs for .NET.

1) Download the Selenium DLLs for .NET from http://selenium-release.storage.googleapis.com/index.html?path=2.47/

2) In the Solutions Explorer, select the project and right-click References.

3) Click Add Reference.

4) Click Browse and navigate to the net35 folder of the directory where you saved the Selenium .NET DLLs.

5) Add all four .DLL references to your project.

#####Install NUnit + PNUnit and Import the Libraries into Your Project:

1) Download the current stable release of NUnit(2.6.4) from http://www.nunit.org/index.php?p=download.

2) In the Solutions Explorer, select the project and right-click References.

3) Click Add Reference.

4) Click Browse and navigate to the bin of the directory where you saved NUnit.

5)Add the nunit.framework.dll and pnunit.framework.dll reference to your project.

### Code Example

####SaucePNUnit_Test.cs:

Now let’s take a look at a simple C# .Net project. This example test opens Google, verifies that “Google” is the title of the page, and then searches for Sauce Labs.

####Constants.cs:

Use this class to set your Sauce Labs account name and access key as environment variables, as shown in the example test. You can find these in the User Profile menu of your Sauce Labs dashboard, under User Settings.
NOTE: If you prefer to hard-code your access credentials into your test, you would do so in the lines
 		caps.SetCapability("username", Constants.SAUCE_LABS_ACCOUNT_NAME);
 		caps.SetCapability("accessKey", Constants.SAUCE_LABS_ACCOUNT_KEY);

However, Sauce Labs recommends using environment variables for authentication as a best practice. This adds a layer of security to your tests, and allows other members of your team to share authentication credentials. 

####sauce_test.conf:

Use this file to set up the configuration for your PNUnit testing. 
NOTE: If you want to do mobile testing, you will need to add additional strings like deviceName and device-orientation inside the TestParams section, and you will also need to add those as DesiredCapabilities in your C# test file. 

### Run the Test

1) Build the project by going Build | Build Solution, or use the CTRL-SHIFT-B shortcut.

2) Change the sauce_test.conf file to specify your project .dll and tests, and the browsers you want to run the tests against. 

3) Add the .dll file of your project, sauce_test.conf, and any other referenced .dll files like the Selenium .DLL files, into the bin of the directory where you saved NUnit.

4) Start the PNUnit agent.
  * Open an Administrator command prompt.
  * Go to NUnit | Bin directory.
  * Enter start pnunit-agent.exe agent.conf.
  * This will open up a new PNUnit agent command prompt.
  NOTE: In the agent.conf file you can specify the port on which the PNUnit agent runs. By default, the port is 8080.
	
5) Start the PNUnit Launcher, which will launch your tests to the Sauce Labs dashboard. 
  * In the same Administrator command prompt, enter pnunit-launcher.exe sauce_test.conf.
  * When the tests begin to run, you will be able to see them in your Sauce Labs dashboard. 
