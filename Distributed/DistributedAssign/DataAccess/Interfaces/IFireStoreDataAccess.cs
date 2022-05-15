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

        //FUNDS
        Task<List<Fund>> createFund();
        Task<List<Fund>> getAllFunds(string email);
        Task<List<Fund>> getSpecificFund(string email, int bankAccNo);
        void deactivate(string email, int bankAccNo);
        void Deposit(string email, int bankAccNo, double money);

        //TRANSFER - in controller

        //AUDIT



    }
}
