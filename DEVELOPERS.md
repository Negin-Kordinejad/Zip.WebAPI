# User API Dev Guide
We design a microservice to provide user management API

Requirements:
 Expose APis for Crud operations for users.
 Expose Apis for account For credit users.
 Users just can have account When they have credit.
 Every user can have more than one acount.
 Credit:***** 
 
## Building

Database :  Sql server relational database

	ORM  :  Entity Framework code first
		    Key Constraint for field ids 
		    Unique index on field Email.
		
	   
Repositories : Linq.

Services: Define business logic and validations.
		  UserSercie
		  AccountService.
		  ValidationService

DTO     : Data Transfer Objects: Need to define to talk with APIs
          We need a mapper tool to map with domain models.

Api :    WebApI Asp.Net 
          A global middleware for error handling
		  Swagger for testing and documentation.
		  A generic response and error model.
		  Define routes and urls.
		 
  
     
## Testing

Functional Test : Add a functional test for every service.
                  Services under test should not Mock. 
                  Just mock calling services and data.

## Deploying

## Additional Information

