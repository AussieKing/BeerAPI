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

## Usage

This Application is not designed to work with a front-end. It is designed to be tested using Insomnia or Postman.

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

## Progress

### DependencyInjection - implementation
Next, we can try to expand the DI use. 
For example, if we'll start to build a database, we could start a logic where the reposisotry can be injected into services/controllers (like I did with IBeerDescriptionService)
DI can also be used more in order for better logging (ILogger);
Also, if we want to test services that usually would rely on API keys, we could inject the `IOptions<T>` (with "T" being the configuration class to swap for the testing).

Structure:
- Data: we could create a `Repositories` folder, and in it a `IBeerRepository.cs` (the interface) and `BeerRepository.cs`. The interface could define the data for `Beer`, and the `BeerRepository.cs` would simply implememt it.
- Logging: read up on `ILogger<T>`
- Config: we could define a new Service Options class (`BeerServiceOptions.cs`), to represent the configuration settings that I want to inject. This would go in `Configurations` folder I assume? We can also modify the Contrctor

### Calling the Controller and Testing
Gurdip mentioned "calling the controller".
Ask her if she meant how the controllers are called during testing? Or how they are used/designed to handle request in the App?
Could be some of these:
- Controller Unit Testing : 
	Basically creating a unit test to the controller, to mock the dependency. We would simulate calling the controller, to test the responses.
- Integration Testing :
	Reading up, I foumd we cuold use `WebApplicationFactory` or `TestServer ` to simulate the app's env. This way we could send HTTP requests (like a client would do), calling the controllers to verify the app behaviour.
- Endpoint Routing :
	Checking that the config of our controllers is configured ok for routing (`[Route]`, `[HttpGet]`, etc).

Ask her if this what she meant, as the above would improve maintainability, and testing.

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
	c. Then the trolley count shows 1 [x] --> `PrintTrolleyState` does this
3. When Customer removes item from the trolley, the count of items is decremented [x] --> `RemoveItem` does this
	a. Given customer has 1 item in the trolley
	b. When they remove that item from the trolley
	c. Then the trolley count shows [x] --> `PrintTrolleyState` does this

### NOTES
In regards to "Then the trolley count shows" requirement, this is currently setup to print in the console. Is this sufficient?
If I need to, say, test in Insomnia via API requests, I would need to change the return to a JSON object.																					
Then, integrate the Endpoints in the Controllers (`BeersController` , or maybe create a `TrolleyController`?? Probably best actually, to keep separation of concerns).
After that, I'd need to register `TrolleyService` in the `Program.cs` DI container, so that it can be injected into the Controllers.											
Finally, in the controller's constructor (the new `TrolleyController`), I can inject the `ITrolleyService` and use it to call the methods to add/remove items from the trolley.

- Step 1: Create a Controller for Trolley operations
- Step 2: Register the TrolleyService in the DI container (Program.cs), so that it can be injected into the Controllers
	`builder.Services.AddScoped<ITrolleyService, TrolleyService>();`
- Step 3: Test Endpoints with Insomnia

Gurdip mentioned I shouldn't use the IBeeRepository in the TrolleyService, as it's not needed. 
I could create a new Service that both Controllers can depend on, so that they can both be separate and not depending on each other.


### COMMENTS 
We write test cases against Services, as this is where all the logic is. Only dealing with the interface allows for more flexibility.
Every unit testing consists of Setup, Act, Verify (3 steps),
Use test explores in VS.

#### TASK:
Write function in a service for the Story 1 to be satisfied, using the Beer list from my database.

#### Pseudocoding:
- Initialize an empty list of `TrolleyItems` (class TrolleyService)
- Create a function to <find> the TrolleyItem in the list via the beerId -> if found, increment its quantity -> else, create a new TrolleyItem with the beerId and qty (=1) -> add the TrolleyItem in the list.
- Create another function to <remove> the TrolleyItem in the list via the beerId -> if found, decrement its quantity -> else (if qty is 0), remove the TrolleyItem from the list.
- Create a function to add up the qty of all TrolleyItems in the list -> return a total count

I'll basically have a `Trolley` that can hold multiple `TrolleyItems`, which will have quantities and different beers.
The logic will be handled by `TrolleyService`, to add/remove beers, and display the item count.
I'll use Dependency Injection in services (`TrolleyService` and `IBeerRepository`), which will be registered in the DI container (for ease of use / injections).
I won't need a model for beers as I already have one, but I will need a new model for the `TrolleyItem` and `Trolley`
I'll then need to create a new Service to manage the trolley logic above.
Then, I'll have to register the Trolley Service in Program.cs.
By doing this, I will have the ability to test the logic in the Services, whilst decoupling it from the Controllers (which, instead, will just do the routing and the requests handling)

#### Progress:
- Trolley and TrolleyItem models provide the necessary structure for representing a shopping trolley and its items, each with a reference to a Beer and a quantity. 
- ITrolleyService interface defines the contract for the trolley service, specifying methods for adding and removing items, getting the item count, and retrieving the trolley itself. any class implementing this interface, like the TrolleyService, will provide these functionalities.
- TrolleyService has the logic itself:
	- AddItem: adds a new TrolleyItem if it's not already in the trolley or increments the quantity of an existing item. 
	- RemoveItem: decrements the quantity of an item or removes it from the trolley if its quantity reaches 0.
	- GetItemCount: gives the total count of all items in the trolley by summing up their quantities.
	- GetTrolley: returns the current state of the trolley.
- PrintTrolleyState prints the status of the trolley.

#### TODO:
- Invoke the service methods within a designated testing area (to be creaed, like Gurdip showed me today with Setup, Act, Verify), to test TrolleyService;
- 