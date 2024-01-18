using RecipeManager.Models;

namespace RecipeManager.Interfaces
{
    public interface ICategoryService
    {
        Status AddCategory(string name);
        Status EditCategory(Category newCategory);
        Status DeleteCategory(int id);
        Category GetCategory(int id);
        List<Category> GetAllCategories();
    }
}