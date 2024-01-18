using RecipeManager.Models.Entities;

namespace RecipeManager.Interfaces
{
    public interface IRecipeRepo
    {
        void AddRecipe(RecipeEntity recipe);
        void EditRecipe(RecipeEntity recipe);
        void DeleteRecipe(int id);
        RecipeEntity GetRecipe(int id);
        List<RecipeEntity> GetAllRecipes();
        List<RecipeEntity> GetRecipesFromUser(int id);
        List<RecipeEntity> FindRecipe(string searchterm);
    }
}