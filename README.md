

Project Description
-------------------

This is a Claim Management web service to expose primary CRUD operation to Get, Create and Delete Claim. It is a REST based service. This webservice developed using Microsoft DotNet technology **ASP.Net MVC5-WebAPI2** with .Net Framework version 4.5. It also has an  emebeded test client to test webservice.

Usage
--------------------

- After downloading this project , please open it in either Visual Studio 2012 or  2013 or 2015.
- Once you are able to build this solution then Start running by selecting one Browser.
- Webpage will load with welcome brief description about the service and a link to show documention.
- Click on Show Documentation link to see all exposed methods in this API.
- You will redirect to another detailed page, if click on each operations.
- You should able to find a **Test API** button at bottom right corner of the page.
- A popup box will appear to test API method upon clicking above mentioned button.

Technologies
--------------------

- ASP.Net WebAPI2.
- C# Language.
- Sql Server Local dababase file(.mdf) 
- Entity Framework.
- Ninject.
- Microsoft UnitTesting Framework.

WebService REST Endpoints
--------------------
API      | Description
-------- | ---
POST Claims/Claim | Creates the claim.
GET Claims/Id/{claimId}    | Gets the claim by identifier.
GET Claims/ClaimNo/{claimNo}     | Gets the claim by claim no.
GET Claims/LossDate/{minDate}/{maxDate}  | Gets the claim by loss date.
GET Claims/Claim/{claimNo}/Vehicle/{vin} | Gets the vehicle of claim.
DELETE Claims/Claim?claimNo={claimNo}  | Deletes the claim.

Other Details
--------------------

- Update is not implemented because of gap in the requirements. Requirements says to update only fields witch are passed in the XML.VIN is not mandatory in the Vehicle Schema, and i am not sure wheather user will pass anyother primary key to update claim and vehicle details.
- Assuming that user will pass date in the format of YYYY-MM-DD or MM-DD-YYYY while calling Claims/LossDate.
- I have incorporated Validations that are mentioned in the XSD. However I can extend it more if required to implement any other validations.
- I have used Sql serve local MDF file to store claim data. I have placed this file in ProcessClaimService/App_Data.


Data Model
--------------------
This is the Databse table structure to store claim information.

![alt tag](https://github.com/)

