using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Interfaces
{
    public interface IRatingService
    {
        Status AddRating(NewRatingModel rating, int userId);
        Status EditRating(EditRatingModel model, int userId, bool admin);
        Status DeleteRating(int id, int userId, bool admin);
        List<RatingDTO> GetAllRatings();
        List<RatingDTO> GetRatingsFromRecipe(int id);
        List<RatingDTO> GetRatingsFromUser(int id);
        
    }
}