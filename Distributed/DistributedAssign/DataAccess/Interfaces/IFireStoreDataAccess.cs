using Common;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IFireStoreDataAccess
    {
        Task<User> GetUser(string email);
        Task<List<User>> GetAllUsers();
        Task<User> GetUsers();
        void AddUser(User user);
        void UpdateUser(User user);


        void DeleteUser(string email);



    }
}
