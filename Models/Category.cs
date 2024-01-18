namespace RecipeManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category()
        {
            Name ??= string.Empty;
        }

        public Category(int id = 0, string? name = null)
        {
            Id = id;
            Name = name ?? string.Empty;
        }
    }
}