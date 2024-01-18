using Microsoft.AspNetCore.Identity.Data;
using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Interfaces
{
    public interface IUserService
    {
        Status AddUser(NewUserModel newUser);
        Status ChangeEmail(string email, int userId);
        Status ChangePassword(ChangePasswordModel model, int userId);
        List<UserDTO> GetAllUsers();
        void DeleteUser(int userId);
        string Login(LoginRequest request);
    }
}