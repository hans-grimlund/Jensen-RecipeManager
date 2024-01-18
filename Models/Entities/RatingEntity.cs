namespace RecipeManager.Models.Entities
{
    public class RatingEntity
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int Recipe { get; set; }
        public int User { get; set; }
    }
}