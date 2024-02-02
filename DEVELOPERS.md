# User API Dev Guide
We design a microservice to provide user management API

Requirements:
 Expose APis for Crud operations for users.
 Expose Apis for account For credit users.
 Users just can have account When they have credit.
 Every user can have more than one acount.
 Credit:***** 
 
## Building

Database :  SqlLite InMemory Relational Database

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

Api :    webApI Asp.Net 
          A Global middleware for error handling
		  Swagger for testing and documentation.
		  A generic response and error model.
		  Define routes and urls.
		 
Issue :
       Email uniqueness is checking in the database layer and user service unit tests for failing so    we need to discuss if we can add this to the validation service . 
	     
     
## Testing

Functional Test : Add a functional test for every service.
                  Services under test should not Mock. 
                  Just mock calling services and data.

## Deploying

## Additional Information

