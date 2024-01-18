namespace RecipeManager.Models.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string RecipeTitle { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}