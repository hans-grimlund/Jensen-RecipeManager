namespace RecipeManager.Models
{
    public class NewRatingModel(int rating = 0, int recipeId = 0)
    {
        public int Rating { get; set; } = rating;
        public int RecipeId { get; set; } = recipeId;
    }
}