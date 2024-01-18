using RecipeManager.Models;

namespace RecipeManager.Interfaces
{
    public interface IUserRepo
    {
        int AddUser(UserEntity user);
        void ChangeEmail(int userId, string newEmail);
        void ChangePassword(int userId, string newPassword);
        void DeleteUser(int id);
        UserEntity GetUser(int id);
        UserEntity GetUser(string username);
        UserEntity GetUserFromEmail(string email);
        List<UserEntity> GetAllUsers();
    }
}