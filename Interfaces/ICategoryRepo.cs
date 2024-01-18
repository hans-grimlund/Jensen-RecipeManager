using RecipeManager.Models;

namespace RecipeManager.Interfaces
{
    public interface ICategoryRepo
    {
        void AddCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(int id);
        Category GetCategory(int id);
        Category GetCategory(string name);
        List<Category> GetAllCategories();
    }
}