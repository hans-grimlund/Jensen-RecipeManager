using RecipeManager.Interfaces;
using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Services
{
    public class RecipeService(IValidationService validationService, IMappingService mappingService, IUserRepo userRepo, IRecipeRepo recipeRepo) : IRecipeService
    {
        private readonly IValidationService _validationService = validationService;
        private readonly IMappingService _mappingService = mappingService;
        private readonly IUserRepo _userRepo = userRepo;
        private readonly IRecipeRepo _recipeRepo = recipeRepo;

        public Status AddRecipe(NewRecipeModel newRecipe, int userId)
        {
            var status = _validationService.ValidateNewRecipe(newRecipe);
            if (status == Status.Ok)
            {
                var recipeEntity = _mappingService.NewRecipeModelToRecipeEntity(newRecipe, userId);
                _recipeRepo.AddRecipe(recipeEntity);
                return Status.Ok;
            }
            return status;
        }

        public Status EditRecipe(RecipeDTO newRecipe, int userId, bool admin)
        {
            var oldRecipe = _recipeRepo.GetRecipe(newRecipe.Id);
            if (oldRecipe == null)
                return Status.NotFound;

            if (!admin && oldRecipe.Owner != userId)
                return Status.Unauthorized;

            var status = _validationService.ValidateNewRecipe(newRecipe);
            if (status != Status.Ok)
                return status;

            var recipeEntity = _mappingService.RecipeDTOToRecipeEntity(newRecipe);
            _recipeRepo.EditRecipe(recipeEntity);
            return Status.Ok;
        }

        public Status DeleteRecipe(int id, int userId, bool admin)
        {
            var recipe = _recipeRepo.GetRecipe(id);
            if (recipe == null)
                return Status.NotFound;
            
            if (!admin && recipe.Owner != userId)
                return Status.Unauthorized;
            
            _recipeRepo.DeleteRecipe(id);
            return Status.Ok;
        }

        public List<RecipeDTO> FindRecipe(string searchterm)
        {
            var recipes = _recipeRepo.FindRecipe(searchterm);
            return _mappingService.RecipeEntityToRecipeDTO(recipes);
        }

        public List<RecipeDTO> GetAllRecipes()
        {
            var recipes = _recipeRepo.GetAllRecipes();
            return _mappingService.RecipeEntityToRecipeDTO(recipes);
        }
    }
}