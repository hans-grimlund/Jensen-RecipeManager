namespace RecipeManager.Models
{
    public class NewRecipeModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}