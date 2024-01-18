using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using RecipeManager.Interfaces;
using RecipeManager.Models;

namespace RecipeManager.Repository
{
    public class UserRepo : IUserRepo
    {   
        public int AddUser(UserEntity user)
        {
            using SqlConnection conn = new(ConnectionString.str);
            
            DynamicParameters parameters = new();
            parameters.Add("@Username", user.Username);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);

            return conn.QueryFirst<UserEntity>("InsertUser", parameters, commandType: CommandType.StoredProcedure).Id;
        }

        public void ChangeEmail(int userId, string newEmail)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", userId);
            parameters.Add("@Email", newEmail);

            conn.Execute("ChangeEmail", parameters, commandType: CommandType.StoredProcedure);
        }

        public void ChangePassword(int userId, string newPassword)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", userId);
            parameters.Add("@Password", newPassword);

            conn.Execute("ChangePassword", parameters, commandType: CommandType.StoredProcedure);
        }

        public void EditUser(UserEntity user)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", user.Id);
            parameters.Add("@Username", user.Username);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);

            conn.Execute("EditUser", parameters, commandType: CommandType.StoredProcedure);
        }

        public void DeleteUser(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            conn.Execute("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
        }

        public UserEntity GetUser(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            return conn.QueryFirstOrDefault<UserEntity>("SelectUser", parameters, commandType: CommandType.StoredProcedure)!;
        }

        public UserEntity GetUser(string username)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Username", username);

            return conn.QueryFirstOrDefault<UserEntity>("SelectUserFromUsername", parameters, commandType: CommandType.StoredProcedure)!;
        }
        
        public UserEntity GetUserFromEmail(string email)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Email", email);

            return conn.QueryFirstOrDefault<UserEntity>("SelectUserFromEmail", parameters, commandType: CommandType.StoredProcedure)!;
        }

        public List<UserEntity> GetAllUsers()
        {
            using SqlConnection conn = new(ConnectionString.str);

            return conn.Query<UserEntity>("SelectAllUsers", commandType: CommandType.StoredProcedure).ToList();
        }
    }
}