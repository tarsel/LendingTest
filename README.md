# LendingTest
Lendint Test from Lend Tech GTONE

System Documentation on Testing and accessing
The System has been developed using the following technologies;
IDE: Microsoft Visual Studio 2019
Language: C#
Web service: Rest Json
Framework: .Net Framework 4.7.2

Prerequisites;
- You need to install Microsoft Visual Studio 2019, Run the project on it.

While Using Localhost
http://localhost:58861/Help -> The Web Api Service Document
http://localhost:58861/Help/Api/POST-application-services-ussdservice -> Full Webservice documentation. Its self-documenting.
This is the webservice url. Call this url on Post Man or Advanced Rest Client.
http://localhost:58861/ application/services/ussdservice

While Using Hosted Service (I have it hosted on cloud for ease of testing)
 http://dtone.kenlinks.net/Help -> The Web Api Service Document
 http://dtone.kenlinks.net/Help/Api/POST-application-services-ussdservice -> Full Webservice documentation. Its self-documenting.
This is the webservice url. Call this url on Post Man or Advanced Rest Client.
http://dtone.kenlinks.net/application/services/ussdservice

Description
The system has exposed a USSD service through the Web Service to be run on either Post Man or Advance Rest Client.
Content-Type : application/json

on the body, have the Json object on the body of the Advance Rest Client as below image. / in the following format.
{
  "text": "",
  "phoneNumber": "0724308810",
  "sessionId": "1234",
  "serviceCode": "1234"
}

While Selecting options from the menu, kindly have the values entered through the "text":"" parameter and also use the * separator while inputting various selections. i.e 
{
  "text": "1*2*3000*1",
  "phoneNumber": "0724308810",
  "sessionId": "1234",
  "serviceCode": "1234"
}
