# Case Study Requirements 
Preventing Cross-Site Request Forgery (CSRF) Attack in ASP.NET Core

Scenario: We have a web application called "SecureApp" that allows authenticated users to perform various actions, such as updating their profile, changing their password, and submitting forms with sensitive data. We need to ensure that the application is protected against CSRF attacks to maintain its security and integrity.

Requirements:
1.	Implement Anti-CSRF Tokens: Ensure that each critical form submission requires an Anti-CSRF token to prevent CSRF attacks.
2.	Validate Anti-CSRF Tokens: Validate the Anti-CSRF token on the server-side before processing any sensitive form submissions.
	
# Introduction 
 We have developed two MVC applications "UnsecureApp" without the CSRF tokens and "SecureApp" with CSRF token.Both applications are haveing CRUD functionalities for logged in user details.

 We can test both the applications using AttackerApp/Postman to inject the url for user operations. "UnsecureApp" will allow the requests and update the data. But  CSRF token in "SecureApp" will prevent any requests coming outside of the application.

CSRF Token Introduction:

Anti-CSRF tokens are related pairs of tokens given to users to validate their requests and prevent issue requests from attackers via the victim.
Anti-forgery tokens prevents anyone from submitting requests to your site while postback the data that are generated by a malicious script not generated by the actual user. For this purpose, the input element with hidden value field and name attribute is created.

 We can test both the applications using postman to inject the url for user operations. "UnsecureApp" will aloow the requests and update the data. But  CSRF token in "SecureApp" will prevent any requests coming outside of the application.

# Project Architecture Overview
 UI Layer :
 .Net6 MVC application with Razor pages
 Business Layer :
  MVC controllers 
 Data Layer :
  .Net6 RestAPI with EntityFrameworkcore
  



