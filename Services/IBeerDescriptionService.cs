// Adding Dependency Injection to the Project
namespace BeerAPI.Services
{
    public interface IBeerDescriptionService
    {
        string GetDescription(Models.Beer beer); 
    }
}