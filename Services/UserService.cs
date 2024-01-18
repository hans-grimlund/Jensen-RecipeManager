using Microsoft.AspNetCore.Identity.Data;
using RecipeManager.Interfaces;
using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Services
{
    public class UserService(IValidationService validationService, IMappingService mappingService, IUserRepo userRepo) : IUserService
    {
        private readonly IValidationService _validationService = validationService;
        private readonly IMappingService _mappingService = mappingService;
        private readonly IUserRepo _userRepo = userRepo;

        public Status AddUser(NewUserModel newUser)
        {
            Status status = _validationService.ValidateNewUser(newUser);
            if (status == Status.Ok)
            {
                newUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newUser.Password, 13);
                _ = _userRepo.AddUser(_mappingService.NewUserModelToUserEntity(newUser));
            }

            return status;
        }

        public Status ChangeEmail(string email, int userId)
        {
            var status = _validationService.ValidateEmail(email);
            if (status != Status.Ok)
                return status;
            
            if (_userRepo.GetUser(userId) == null)
                return Status.NotFound;

            _userRepo.ChangeEmail(userId, email);
            return Status.Ok;
        }

        public Status ChangePassword(ChangePasswordModel model, int userId)
        {
            var user = _userRepo.GetUser(userId);
            if (!BCrypt.Net.BCrypt.EnhancedVerify(model.OldPassword, user.Password))
                return Status.IncorrectPassword;

            var status = _validationService.ValidatePassword(model.NewPassword);
            if (status != Status.Ok)
                return status;

            _userRepo.ChangePassword(userId, BCrypt.Net.BCrypt.EnhancedHashPassword(model.NewPassword));
            return Status.Ok;
        }

        public void DeleteUser(int userId)
        {
            _userRepo.DeleteUser(userId);
        }

        public List<UserDTO> GetAllUsers()
        {
            var userEntities = _userRepo.GetAllUsers();
            return _mappingService.UserEntityToUserDTO(userEntities);
        }

        public string Login(LoginRequest request)
        {
            var user = _userRepo.GetUserFromEmail(request.Email);
            if (user == null)
                return "not found";
            
            if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password))
                return "incorrect password";

            return TokenGenerator.GenerateJWT(user);
            
        }
    }
}