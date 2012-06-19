************************************************
*INTRODUCTION:
************************************************

"Book!t" is a project for Thomson Reuters (China) Innovation Competition. 
It is a website that employs the latest techs to help everyone search and book conference rooms, 
with innovative features like location-aware path finding etc.


************************************************
*HOW TO BUILD:
************************************************

0. Install ASP.NET MVC4: http://www.asp.net/mvc/mvc4
1. Register Outlook Redemption: regsvr32 Lib\Redemption.dll
2. Restore DB: restore DB\BookitDB_v1.1.bak to database "BookitDb".
3. Build the whole solution in Visual Studio.
4. Install service: 
	a. copy all built service assembly files to a dedicated folder;
	b. register the service using installutil
	c. IMPORTANT: change "Log On" information of the service 
		      to an account that has proper access right to Exchange server.

5. Install Web UI: publish the website and configure it in IIS.

************************************************
*DISCLAIMER:
************************************************

This project leverages following 3rd party libraries to build the UI and query Outlook:

0. Telerik ASP.NET MVC control (DateTimePicker) - trial version
1. Outlook Redemption - developer version

Since this is an internal open source project I don't think it would violate copyright 
but if it does, please let me know.



