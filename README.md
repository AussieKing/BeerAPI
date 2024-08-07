# BeerAPI

## Description

[![MIT License](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

An API implementation of CRUD operations using C# in a SQL Server environment.

## Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Usage](#usage)
- [Installation](#installation)
- [License](#license)
- [User Story](#user-story)
- [Progress](#progress)
- [Screenshots](#screenshots)
- [Issues](#issues)
- [Author](#author)
  
## Overview

This API application was designed to return info on Beer objects in a database. It was created using C# and .NET Core. The application uses a Model-View-Controller (MVC) structure. The application can be tested using Postman or Insomnia.mmm

Recently the application had FluentValidation added to it to validate the incoming data. This was done to ensure that the data being sent to the database is valid and to prevent any errors.
Also, Dependency Injection was added, by registering the `BeerValidator` so that it is automatically used by ASP.NET Core to validate the `Beer` object when it is passed to the `CreateBeer` method.

## Technologies Used

  - C#
  - .NET Core
  - ASP.NET Core
  - SQL Server 
  - Visual Studio Code
  - Git
  - Insomnia 
  - Swagger
  - OpenWeather 

## Usage

This Application is not designed to work with a front-end at this stage. The backend is designed to be tested using SQL Server, and Swagger or Insomnia.

![Insomnia Screenshot](<Screenshot 2024-02-15 at 10.39.05 am.png>)

The API routes are as below.
Please make sure to add to your usual server url.

```
http://localhost:5244/
```

### Routes

#### Beers 
```
GET ../api/beers/{id} - returns the beer with the specified {id}
POST ../api/beers - creates a beer by providing `name`, `price`, and `promoPrice` (optional)
PATCH ../api/api/beers/{id}/promoprice - updates a beer promo price by specifying their id and passing a `newPromoPrice`
PUT ../api/api/beers/{id} - updates a beer details by passing `id`, `name`, `price`, and `promoPrice` (optional)
DELETE ..api/beers/{id} - deletes a beer by specifying their `id`
```
#### Trolley 
```
GET ../api/trolley - returns the trolley items
POST ../api/trolley{beerId} - adds a beer item with the given {id} to the trolley list
DELETE ../api/trolley{beerId} - removes the beer with the given {id} from the trolley list
```
#### Recommend Beer
```
GET ../BeerRecommendation/recommend - returns a beer based on fetched weather data
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


## PROGRESS
- New functionality of Recommending Beers based on weather data (pulled from OpenWeather API) is now working properly: 
This was achieved by creating a new controller `BeerRecommendationController ` that handles the HTTP requests for recommendations. 
This controller fetched the temperature data (`main.temp`) from the OpenWeather API, deserializes the JSON response into a WeatherResponse object, and uses
`RecommendBeerBasedOnWeather` method to make the recommendation.
The new `WeatherService` handles the business logic for making the HTTP request, and retrieve the weather data.
Lastly, the new `WeatherResponse` model defines the structure of the API response (`Main` and `Temp` store the temperature data).
`Program.cs` was modfied to include configuration and Dependency Injection for `WeatherService`.


## SCREENSHOTS
### GET
  	GET Trolley (empty) in Swagger
<img width="733" alt="trolley" src="https://github.com/AussieKing/BeerAPI/assets/126050763/c77d68c6-44fe-4ef6-9a19-13a390d1b2c8">

  	GET Trolley (empty) in DB Browser
<img width="534" alt="DbBr-GET" src="https://github.com/AussieKing/BeerAPI/assets/126050763/512d97fc-08a0-4300-a850-f3e0ee04a180">

### POST
	POST Item to Trolley in Swagger
<img width="817" alt="POST to trolley" src="https://github.com/AussieKing/BeerAPI/assets/126050763/4d7cd06d-9820-4868-b17b-bc1e145dd7ca">

	POST Item to Trolley in DB Browswer
<img width="490" alt="add beer to trolley" src="https://github.com/AussieKing/BeerAPI/assets/126050763/1cf5e17d-4693-487c-889d-4e71902d8569">

	GET Trolley in DB  Browser and Swagger with new Items
<img width="517" alt="get new trolley" src="https://github.com/AussieKing/BeerAPI/assets/126050763/8b64f91e-2bfe-4ee4-b87a-384b6f0c2f5c">
<img width="745" alt="get swagger" src="https://github.com/AussieKing/BeerAPI/assets/126050763/6b809de5-191d-4709-b180-8b462c1f0a3c">

### DELETE
	DELETE Item from Trolley in Swagger
<img width="770" alt="swagger delete" src="https://github.com/AussieKing/BeerAPI/assets/126050763/6ec90987-e23d-48c9-a610-7eef129d6a0e">

	DELETE Item from Trolley in DB Browser
<img width="505" alt="delete DBB" src="https://github.com/AussieKing/BeerAPI/assets/126050763/47e80064-1de0-4300-a841-c31c0ac507bf">

### CRUD operations for Beer Items in DB Browser
	CREATE New Beer Iteam in DB Browser
<img width="526" alt="So's sour" src="https://github.com/AussieKing/BeerAPI/assets/126050763/fe044161-fa23-4e57-be8d-1cdb1c305571">
<img width="146" alt="So's sour-select" src="https://github.com/AussieKing/BeerAPI/assets/126050763/3c45f6c2-ecc8-49b8-b657-07f9486c8954">

	UPDATE Beer Item in DB Browser
<img width="462" alt="so's sour-update" src="https://github.com/AussieKing/BeerAPI/assets/126050763/4a884161-a98f-42ba-b299-f770f1c68939">
<img width="229" alt="so's updated" src="https://github.com/AussieKing/BeerAPI/assets/126050763/6ee1929e-49a9-4e66-89a8-3cd248cd4df0">

	DELETE a Beer Item in DB Browser
<img width="294" alt="so's deleted" src="https://github.com/AussieKing/BeerAPI/assets/126050763/f9f2a725-0f96-4ee5-87b3-bebbd0055955">

### RECOMMEND A BEER
	RECOMMENDS a Beer Item based on weather data fetched from OpenWeather API.
<img width="662" alt="beer-recommend" src="https://github.com/AussieKing/BeerAPI/assets/126050763/82385c4b-0b3e-4294-b330-3d41d57c4787">

	

## ISSUES


### PSEUDO CODING

#### TODO:


### STEPS


## Author
This App was created for training purposes only.



