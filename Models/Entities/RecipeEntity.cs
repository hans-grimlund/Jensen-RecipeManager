namespace RecipeManager.Models.Entities
{
    public class RecipeEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public int Category { get; set; }
        public int Owner { get; set; }
    }
}