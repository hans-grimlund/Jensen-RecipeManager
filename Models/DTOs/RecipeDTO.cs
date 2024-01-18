namespace RecipeManager.Models.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}