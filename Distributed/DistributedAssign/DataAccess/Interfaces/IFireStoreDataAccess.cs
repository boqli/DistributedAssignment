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
        Task<Fund> createFund(string bankCode, string BankName, string Country, string CountryCode, int openingBal, string payee);
        Task<List<Fund>> getAllFunds(string email);
        Task<List<Fund>> getSpecificFund(string email, int bankAccNo);
        void deactivate(string email, int bankAccNo);
        Task<int> getAllFunds();

        //TRANSFER - in controller
        void Deposit(string email, int bankAccNo, double money);
        Task transferToOwnAccount(string email, int fundAccSender, int fundAccReciever, double money);
        Task transferToOtherAccount(string email, int fundAccSender, int fundAccReciever, double money);

        //AUDIT
        Task<Audits> createAudit(string emailSender, string emailReciever, int fundAccSender, int fundAccReciever, int amount);



    }
}
