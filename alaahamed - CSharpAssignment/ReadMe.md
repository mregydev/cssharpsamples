Steps to create and initialize the database
=============================================

* Run the attached database script file which can be found inside Source folder on your sql server 
* In the NationalCriminalsDAL project which connects to the database uses windows authentication mode and you can change it from the app.config file 
found in the project through changing added connectionstring 


Steps to prepare the source code to build properly
=====================================================
* Deploy NationalCriminalsWcf on your IIS local or express server 
* Modify the new wcf endpoint  path in your Mvc project (NationalCriminals) in web.config -- found in the client xml tag--

Any assumptions made and missing requirements that are not covered in the requirements
=======================================================================================
* I added a new scenario to the application which is giving the ability to the logged in user to add new criminals


