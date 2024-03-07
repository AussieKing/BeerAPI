//? Adding Dependency Injection to the Project
//? Step 1 - Create a new file called IBeerDescriptionService.cs in the Services folder
// Here we define the IBeerDescriptionService interface, which contains a single method called GetDescription. 
// This method takes a BeerAPI object as an argument and returns a string.

namespace BeerAPI.Services
{
    public interface IBeerDescriptionService
    {
        string GetDescription(Beer beer);
    }
}