using RecipeManager.Interfaces;
using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Services
{
    public class ValidationService(ICategoryRepo categoryRepo) : IValidationService
    {
        private readonly ICategoryRepo _categoryRepo = categoryRepo;

        public Status ValidateNewUser(NewUserModel newUser)
        {
            var emailStatus = ValidateEmail(newUser.Email);
            if (emailStatus != Status.Ok)
                return emailStatus;

            var usernameStatus = ValidateUsername(newUser.Username);
            if (usernameStatus != Status.Ok)
                return usernameStatus;
            
            var passwordStatus = ValidatePassword(newUser.Password);
            if (passwordStatus != Status.Ok)
                return passwordStatus;
            
            return Status.Ok;
        }

        public Status ValidateEmail(string email)
        {
            if (email.Contains('@') && email.Length > 4 && email.Length < 100)
                return Status.Ok;
            return Status.InvalidEmail;
        }

        public Status ValidatePassword(string password)
        {
            if (password.Length > 7 && password.Length < 50)
                return Status.Ok;
            return Status.InvalidPassword;
        }

        public Status ValidateUsername(string username)
        {
            if (username.Length > 7 && username.Length < 50)
                return Status.Ok;
            return Status.InvalidUsername;
        }

        public Status ValidateNewRecipe(NewRecipeModel recipe)
        {
            if (recipe.Title == null || recipe.Title == string.Empty || recipe.Title.Length < 3)
                return Status.InvalidTitle;
            
            if (recipe.Description == null || recipe.Description == string.Empty || recipe.Description.Length < 8)
                return Status.InvalidDescription;

            if (recipe.Ingredients == null || recipe.Ingredients == string.Empty || recipe.Ingredients.Length < 4)
                return Status.InvalidIngredients;
            
            if (_categoryRepo.GetCategory(recipe.Category) == null)
                return Status.InvalidCategory;
            
            return Status.Ok;
        }

        public Status ValidateNewRecipe(RecipeDTO newRecipe)
        {
            return Status.Ok;
        }

        public Status ValidateNewRating(NewRatingModel newRating)
        {
            if (newRating.Rating > 0 && newRating.Rating < 6)
                return Status.Ok;
            return Status.InvalidRating;
        }

        public Status ValidateNewRating(EditRatingModel newRating)
        {
            if (newRating.Rating >= 1 && newRating.Rating <= 5)
                return Status.Ok;
            return Status.InvalidRating;
        }
    }
}