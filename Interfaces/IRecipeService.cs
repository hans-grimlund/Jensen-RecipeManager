using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Interfaces
{
    public interface IRecipeService
    {
        Status AddRecipe(NewRecipeModel newRecipe, int userId);
        Status EditRecipe(RecipeDTO newRecipe, int userId, bool admin);
        Status DeleteRecipe(int id, int userId, bool admin);
        List<RecipeDTO> GetAllRecipes();
        List<RecipeDTO> FindRecipe(string searchterm);
    }
}