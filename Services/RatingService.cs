using RecipeManager.Interfaces;
using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Services
{
    public class RatingService(IRatingRepo ratingRepo, IRecipeRepo recipeRepo, IMappingService mappingService, IValidationService validationService) : IRatingService
    {
        private readonly IRatingRepo _ratingRepo = ratingRepo;
        private readonly IRecipeRepo _recipeRepo = recipeRepo;
        private readonly IMappingService _mappingService = mappingService;
        private readonly IValidationService _validationService = validationService;

        public Status AddRating(NewRatingModel rating, int userId)
        {
            if (_validationService.ValidateNewRating(rating) != Status.Ok)
                return Status.InvalidRating;
                
            var recipe = _recipeRepo.GetRecipe(rating.RecipeId);
            if (recipe == null)
                return Status.NotFound;
            
            if (recipe.Owner == userId)
                return Status.Unauthorized;
            
            var recipeRatings = _ratingRepo.GetRatingsFromRecipe(rating.RecipeId);
            if (recipeRatings.FirstOrDefault(rating => rating.User == userId) != null)
                return Status.Unauthorized;
            
            _ratingRepo.InsertRating(_mappingService.NewRatingModelToRatingEntity(rating, userId));

            return Status.Ok;
        }

        public Status EditRating(EditRatingModel model, int userId, bool admin)
        {
            var rating = _ratingRepo.GetRating(model.Id);
            if (rating == null)
                return Status.NotFound;

            if (rating.User != userId && !admin)
                return Status.Unauthorized;
            
            var ratingEntity = _mappingService.EditRatingModelToRatingEntity(model);
            _ratingRepo.EditRating(ratingEntity);

            return Status.Ok;
        }

        public Status DeleteRating(int id, int userId, bool admin)
        {
            var rating = _ratingRepo.GetRating(id);
            if (rating == null)
                return Status.NotFound;
            
            if (rating.User != userId && !admin)
                return Status.Unauthorized;

            _ratingRepo.DeleteRating(id);

            return Status.Ok;
        }

        public List<RatingDTO> GetAllRatings()
        {
            var ratings = _ratingRepo.GetAllRatings();
            return _mappingService.RatingEntityToRatingDTO(ratings);
        }

        public List<RatingDTO> GetRatingsFromRecipe(int id)
        {
            var ratings = _ratingRepo.GetRatingsFromRecipe(id);
            return _mappingService.RatingEntityToRatingDTO(ratings);
        }

        public List<RatingDTO> GetRatingsFromUser(int id)
        {
            var ratings = _ratingRepo.GetRatingsFromUser(id);
            return _mappingService.RatingEntityToRatingDTO(ratings);
        }
    }
}