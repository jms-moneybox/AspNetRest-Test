# Task : Create a simple REST API using ASP.NET

## Summary 

The solution has been built using WebApi to provide a RESTful API and SQL Lite & Dapper to provide a persitent data access layer. Sql lite has been configured to create a db in c:\Temp\ which may require permissions. No other configuration should be required.

I decided not to use sql server because of the possible requirements to get this running, however I had not used SQL Lite before, so it ended up consuming more time than expected. I chose dapper with dapper extensions over entity framework because of the potential simplicity. Unfortunately dapper and dapper extensions did not play well with SQL Lite so a simple task ended up taking a bit longer (but it was good to learn). On reflection, I could easily have built an in memory repository, but felt this would not satisfy requirement 3, while using SQL server EF may have compromised requirement 1.

I have also split the solution out into seperate projects to display how the code should be isolated. While this is not strictly the simplest solution, I felt it was worthwhile to show best practise.
 
### Debugging

To Debug the solution, simply run the web.api project either from visual studio or iis.

### REST Api Endpoints
A Postman script has been included in the root of this repository to facilitate executing the requests detailed below.

#### Create Transaction
> POST /api/transactions
 
####  Update Transaction.
> PUT /api/transactions/{transactionId}

#### Delete Transaction
> DELETE /api/transactions/{transactionId}

#### Get Transaction
> GET /api/transactions/{transactionId}

#### Get All Transactions
> GET /api/transactions

### Tests
Some unit tests have been written against the API controllers using MsTest and MOQ. The repository layer is mocked out to avoid writing to the db when testing.
I have used MSTest over Nunit to ensure that they can be run on any machine.

Further improvements to the tests would include a full integration test suite that called the hosted api.

### Time & Improvements
I spent around 2 hours on this, though setting up SQL Lite was a time drain and using an in memory repository or EF would have reduced the effort. 

I didnt get around to setting up a dependency injection framework.

Things that could be added given the time include : 
- DI Framework
- Logging
- Validation
- Error Handling
- Message Envelopes
- Async actions


