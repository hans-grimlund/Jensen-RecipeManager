using RecipeManager.Models;
using RecipeManager.Models.DTOs;
using RecipeManager.Models.Entities;

namespace RecipeManager.Interfaces
{
    public interface IMappingService
    {
        UserEntity NewUserModelToUserEntity(NewUserModel newUserModel, int userId = 0);
        UserDTO UserEntityToUserDTO(UserEntity userEntity);
        List<UserDTO> UserEntityToUserDTO(List<UserEntity> userEntities);
        RecipeEntity NewRecipeModelToRecipeEntity(NewRecipeModel newRecipeModel, int userId = 0);
        RecipeDTO RecipeEntityToRecipeDTO(RecipeEntity recipeEntity);
        List<RecipeDTO> RecipeEntityToRecipeDTO(List<RecipeEntity> recipeEntities);
        RecipeEntity RecipeDTOToRecipeEntity(RecipeDTO recipeDTO);
        RatingEntity NewRatingModelToRatingEntity(NewRatingModel newRatingModel, int userId = 0);
        RatingDTO RatingEntityToRatingDTO(RatingEntity ratingEntity);
        List<RatingDTO> RatingEntityToRatingDTO(List<RatingEntity> ratingEntities);
        RatingEntity EditRatingModelToRatingEntity(EditRatingModel editRatingModel);
    }
}