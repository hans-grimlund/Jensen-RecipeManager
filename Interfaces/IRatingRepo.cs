using RecipeManager.Models.Entities;

namespace RecipeManager.Interfaces
{
    public interface IRatingRepo
    {
        void InsertRating(RatingEntity rating);
        void EditRating(RatingEntity rating);
        void DeleteRating(int id);
        RatingEntity GetRating(int id);
        List<RatingEntity> GetAllRatings();
        List<RatingEntity> GetRatingsFromRecipe(int id);
        List<RatingEntity> GetRatingsFromUser(int id);
    }
}