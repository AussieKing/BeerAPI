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

## Overview

This API application was designed to return info on Beer objects in a database. It was created using C# and .NET Core. The application uses a Model-View-Controller (MVC) structure. The application can be tested using Postman or Insomnia.mmm

Recently the appliacation had FluentValidation added to it to validate the incoming data. This was done to ensure that the data being sent to the database is valid and to prevent any errors.
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
Also, if we want to test services that usually would rely on API keys, we could inject the `IOptions<T>` (with "T" being the configuratiion class to swap for the testing).

### Calling the Controller
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