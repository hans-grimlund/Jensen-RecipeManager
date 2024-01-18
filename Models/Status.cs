namespace RecipeManager.Models
{
    public enum Status
    {
        None,
        NotFound,
        InvalidEmail,
        InvalidPassword,
        IncorrectPassword,
        InvalidUsername,
        InvalidTitle,
        InvalidDescription,
        InvalidIngredients,
        InvalidCategory,
        InvalidRating,
        Unauthorized,
        Ok,
        Error,
    }
}