using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Interfaces
{
    public interface IValidationService
    {
        Status ValidateNewUser(NewUserModel newUser);
        Status ValidateEmail(string email);
        Status ValidatePassword(string password);
        Status ValidateUsername(string username);
        Status ValidateNewRecipe(NewRecipeModel newRecipe);
        Status ValidateNewRecipe(RecipeDTO newRecipe);
        Status ValidateNewRating(NewRatingModel newRating);
        Status ValidateNewRating(EditRatingModel newRating);
    }
}