using RecipeManager.Interfaces;
using RecipeManager.Models;

namespace RecipeManager.Services
{
    public class CategoryService(ICategoryRepo categoryRepo) : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo = categoryRepo;
        
        public Status AddCategory(string name)
        {
            _categoryRepo.AddCategory(new Category(name: name));
            return Status.Ok;
        }

        public Status EditCategory(Category newCategory)
        {
            if (_categoryRepo.GetCategory(newCategory.Id) == null)
                return Status.NotFound;

            _categoryRepo.EditCategory(newCategory);
            return Status.Ok;
        }

        public Status DeleteCategory(int id)
        {
            if (_categoryRepo.GetCategory(id) == null)
                return Status.NotFound;

            _categoryRepo.DeleteCategory(id);
            return Status.Ok;
        }

        public Category GetCategory(int id)
        {
            var category = _categoryRepo.GetCategory(id);
            if (category == null)
                return new();

            return category;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepo.GetAllCategories();
        }
    }
}