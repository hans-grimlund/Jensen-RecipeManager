using RecipeManager.Interfaces;
using RecipeManager.Models;
using RecipeManager.Models.DTOs;
using RecipeManager.Models.Entities;

namespace RecipeManager.Services
{
    public class MappingService(IRecipeRepo recipeRepo, IUserRepo userRepo, ICategoryRepo categoryRepo, IRatingRepo ratingRepo) : IMappingService
    {
        private readonly IRecipeRepo _recipeRepo = recipeRepo;
        private readonly IUserRepo _userRepo = userRepo;
        private readonly ICategoryRepo _categoryRepo = categoryRepo;
        private readonly IRatingRepo _ratingRepo = ratingRepo;

        public UserEntity NewUserModelToUserEntity(NewUserModel newUserModel, int userId)
        {
            return new UserEntity()
            {
                Id = userId,
                Username = newUserModel.Username,
                Email = newUserModel.Email,
                Password = newUserModel.Password
            };
        }

        public UserDTO UserEntityToUserDTO(UserEntity userEntity)
        {
            return new UserDTO()
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email
            };
        }

        public List<UserDTO> UserEntityToUserDTO(List<UserEntity> userEntities)
        {
            List<UserDTO> DTOs = [];
            foreach (UserEntity userEntity in userEntities)
            {
                DTOs.Add(UserEntityToUserDTO(userEntity));
            }
            return DTOs;
        }

        public RecipeEntity NewRecipeModelToRecipeEntity(NewRecipeModel newRecipeModel, int userId = 0)
        {
            return new RecipeEntity()
            {
                Title = newRecipeModel.Title,
                Description = newRecipeModel.Description,
                Ingredients = newRecipeModel.Ingredients,
                Category = _categoryRepo.GetCategory(newRecipeModel.Category).Id,
                Owner = userId
            };
        }

        public RecipeDTO RecipeEntityToRecipeDTO(RecipeEntity recipeEntity)
        {
            return new RecipeDTO()
            {
                Id = recipeEntity.Id,
                Title = recipeEntity.Title,
                Description = recipeEntity.Description,
                Ingredients = recipeEntity.Ingredients,
                Category = _categoryRepo.GetCategory(recipeEntity.Category).Name,
                Owner = _userRepo.GetUser(recipeEntity.Owner).Username
            };
        }

        public List<RecipeDTO> RecipeEntityToRecipeDTO(List<RecipeEntity> recipeEntities)
        {
            List<RecipeDTO> DTOs = [];
            foreach (var recipe in recipeEntities)
            {
                DTOs.Add(RecipeEntityToRecipeDTO(recipe));
            }
            return DTOs;
        }

        public RecipeEntity RecipeDTOToRecipeEntity(RecipeDTO recipeDTO)
        {
            return new RecipeEntity()
            {
                Id = recipeDTO.Id,
                Title = recipeDTO.Title,
                Description = recipeDTO.Description,
                Ingredients = recipeDTO.Ingredients,
                Category = _categoryRepo.GetCategory(recipeDTO.Category).Id
            };
        }

        public RatingEntity NewRatingModelToRatingEntity(NewRatingModel newRatingModel, int userId = 0)
        {
            return new RatingEntity()
            {
                Rating = newRatingModel.Rating,
                Recipe = newRatingModel.RecipeId,
                User = userId
            };
        }

        public RatingDTO RatingEntityToRatingDTO(RatingEntity ratingEntity)
        {
            return new RatingDTO()
            {
                Id = ratingEntity.Id,
                Rating = ratingEntity.Rating,
                RecipeTitle = _recipeRepo.GetRecipe(ratingEntity.Recipe).Title,
                Username = _userRepo.GetUser(ratingEntity.User).Username
            };
        }

        public List<RatingDTO> RatingEntityToRatingDTO(List<RatingEntity> ratingEntities)
        {
            List<RatingDTO> DTOs = [];
            foreach (var rating in ratingEntities)
            {
                DTOs.Add(RatingEntityToRatingDTO(rating));
            }
            return DTOs;
        }

        public RatingEntity EditRatingModelToRatingEntity(EditRatingModel editRatingModel)
        {
            return new RatingEntity()
            {
                Id = editRatingModel.Id,
                Rating = editRatingModel.Rating
            };
        }
    }
}