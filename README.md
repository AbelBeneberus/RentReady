# RentReady
RentReady is a demonstration appliction for using microsoft azure function to create a dataverse
entity record.
In this project I have used microsoft dynamics msmsdyn_timeentry table to demonstrate the overall flow of the data.

The Function application is capable of providing a swagger UI with auto generated sample payload so that it makes easy to test the TimeEntry function.

Taken Assumptions. 
 
    As the duration field is a mandatory filed from dataverse side I have computed it from
    
    the code base by setting all startOn time to 2am and endOn at 11 am.
## Environment Variables

To run this project, you will need to set UserName and Password in the connectionString section of configuration in func.settings.json.

to use the RentReady application for diffrent dataverse app you need to update the AppId and Url as well. 

 
`connectionString` : " AuthType=OAuth; Username=usera; Password=password; AppId=304b2042-f0ad-4e35-890d-1d4583af0c06;Url=https://org2dc94d15.crm4.dynamics.com; RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;",
## Run Locally

Clone the project

```bash
  git clone https://github.com/AbelBeneberus/RentReady.git
```

Go to the project directory

open the solution file with visual studio and build and run.



## Deployment

due to the lack of Azure Free account, I wouldn't able to setup a deployment steps.

please refer this link for setting up git integration for deployment.

https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-github-actions?tabs=dotnet
