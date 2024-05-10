# BeerAPI

## Description

[![MIT License](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

An API implementation of CRUD operations using C#.

## Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Usage](#usage)
- [Installation](#installation)
- [License](#license)
- [Author](#author)
- [Progress](#progress)
- [User Story](#user-story)
- [Screenshots](#screenshots)

## Overview

This API application was designed to return info on Beer objects in a database. It was created using C# and .NET Core. The application uses a Model-View-Controller (MVC) structure. The application can be tested using Postman or Insomnia.mmm

Recently the application had FluentValidation added to it to validate the incoming data. This was done to ensure that the data being sent to the database is valid and to prevent any errors.
Also, Dependency Injection was added, by registering the `BeerValidator` so that it is automatically used by ASP.NET Core to validate the `Beer` object when it is passed to the `CreateBeer` method.

## Technologies Used

  - C#
  - .NET Core
  - ASP.NET Core
  - Visual Studio Code
  - Git
  - Insomnia 

## Usage

This Application is not designed to work with a front-end. The backend is designed to be tested using Insomnia or Postman.

![Insomnia Screenshot](<Screenshot 2024-02-15 at 10.39.05 am.png>)

The API routes are as below.
Please make sure to add to your usual server url.

```
http://localhost:5244/
```

### Routes

```
GET ..api/beers/{id} - returns the beer with the specified {id}
POST ../api/beers - creates a user by providing Name, Price, and PromoPrice (optional)
PUT ../api/api/beers/{id}/promo-price - updates a beer promo price by specifying their id
DELETE ..api/beers/{id} - deletes a beer by specifying their id
```


## Installation

1. Clone the Repository from [GitHub](https://github.com/AussieKing/BeerAPI.git) by using the Git Clone command.
2. Open the cloned repository in your code editor of choice (I used VS Code).
3. Open the integrated terminal and run the following command to install the dependencies:

``` bash
dotnet build
dotnet watch run
```

4. If you're on Mac, also run the following in the terminal (from the root directory)

```bash
dotnet dev-certs https --trust 
```

5. Use Insomnia to test the api routes.


## License

This application is covered under the MIT license. Please refer to the document titled [LICENSE](LICENSE).

## Author

This application was written and developed by Freddy Dordoni.

## USER STORY
You are a developer in our e-commerce team. We are tasked to build the Shopping Trolley
feature. The requirements given as a user stories as below.
Your task is to design and develop this feature. 
You have the freedom to do it anyway you want and demonstrate, show us your strengths. 
Few suggestions are:

a. Build one or more libraries or services
b. Build one or more APIs / services
c. Choose an appropriate hosting environment - this can be just running in your favorite IDE, containers in your local machines or hosted in your favourite cloud environment
d. Optionally build a single page app or a web front end
e. Optionally deploy your application with build pipelines
f. Optionally build dashboards to monitor your services and application
	
Like true agile teams, we prioritise and build our features in small iterations. 
The user stories build up to provide richer experience with each adding some enhancements.

We care a lot about clean design, clean code, good tests and continuous delivery practices.
Demonstrate your strengths in these aspects.

#### Trolley Use Cases:
User Story 1:
As a customer I want to add / remove items to the trolley so that I can purchase the drinks
want.

#### Acceptance Criteria:
1	. Customers can add the same item more than once. For MVP, there is no upper limit on the number of items or quantity they add [x]

2	. When customer adds an item to the trolley, they can see the count of items incremented. [x] --> `AddItem` does this
	a. Given customer has no item in the trolley 
	b. When they add the first item
	c. Then the trolley count shows 1 [x] --> `AddItemToTrolley` in `TrolleyController` does this
 
3	. When Customer removes item from the trolley, the count of items is decremented [x] --> `RemoveItem` does this
	a. Given customer has 1 item in the trolley
	b. When they remove that item from the trolley
	c. Then the trolley count shows [x] --> `PrintTrolleyState` does this

## SCREENSHOTS
- ADD Beer item to Trolley :
<img width="627" alt="ADD beer" src="https://github.com/AussieKing/BeerAPI/assets/126050763/f5895d2d-a2bb-4ef2-b382-102d9ed65477">

- GET Trolley status:
<img width="669" alt="GET Trolley-1" src="https://github.com/AussieKing/BeerAPI/assets/126050763/e59d7989-8af8-4e96-ba50-25d70933019c">
  
- DELETE Beer item from Trolley:
<img width="675" alt="REMOVE Beer-1" src="https://github.com/AussieKing/BeerAPI/assets/126050763/46e99c0d-88b1-4061-a4fe-511aa378e9e5">

- DELETE Last of Beer items from Trolley
<img width="663" alt="REMOVE Beer-2" src="https://github.com/AussieKing/BeerAPI/assets/126050763/752b4054-1edd-4a7c-870c-9fa5be67093d">

- GET Trolley status again (Trolley now Empty):
<img width="660" alt="GET Trolley-2" src="https://github.com/AussieKing/BeerAPI/assets/126050763/543cf6b1-e346-46d4-a4e0-aef836042c4b">

### DEV COMMENTS 
We write test cases against Services, as this is where all the logic is. Only dealing with the interface allows for more flexibility.
Every unit testing consists of Setup, Act, Verify (3 steps),
Use test explores in VS.

#### TODO:
- Practice using LocalDb (ideally SQL Server, but I don't have access yet), which is a file based compact version of SQL Server
- 
- 

### PSEUDO CODING

I need to refactor my trolley service so that it takes a trolley repository, an the treolley repo does CRUD operations on the list in memory. The repo should be Singleton.

I could star by defining an interface for the the trolley repository that includes the CRUD ops that are specific to the trolley data.

Then, I need to implement the Trolley repository, so that it can handle the in-memory data storage (or, ask So if we should look at implementing a database-based solution right away, as if I use the `Dictionary` in the Trolley repository to store the data, the data will be lost once the app restarts)

Use SQL Lite.

### STEPS
1. Installed NuGet package SQLite: `dotnet add package Microsoft.Data.Sqlite
dotnet add package Dapper`
2. Connection string in appsettings.json (database in output directory)
3. Create + Initalize db : create new folder (Data) with a dedicated Class (`DatabaseSetup.cs`) to initialise the db. Then go through usual steps (check for db file existance, create tables, etc)
4. Create Repository classes (to bridge the app logic and the db access), use Repo interface for specific db operations. Then implement the interface in a class with Dapper (to execute SQL on the SqLite db), and finally place this in the Repositories folder (inisde the Data directory)
5. Integrate the db operations in the app: change the service classes to use the db instead of the in app memory (inject Repository into the Service)
6. Register Database as Singleton, 


### PROGRESS
- CRUD operations now work successfully: I can now create, update, read and delete new Beer items, as well as TrolleyItems.

- Database can be queried properly: I can now perform CRUD operations using a database directly, rather than in-memory data like previously.

