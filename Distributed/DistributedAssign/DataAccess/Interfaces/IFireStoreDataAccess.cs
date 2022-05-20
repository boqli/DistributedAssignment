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
        Task<Fund> createFund(string bankCode, string BankName, string Country, string CountryCode, string currencyCode,int openingBal, string payee);
        Task<List<Fund>> getAllFunds(string email);
        Task<List<Fund>> getSpecificFund( int bankAccNo);
        Task<List<Fund>> getSpecificFundWithId(string email, int bankAccNo);
        void deactivate(string email, int bankAccNo);
        Task<int> getAllFunds();
        Task<List<Fund>> Search(string fundNo);

        //TRANSFER - in controller
        void Deposit(string email, int bankAccNo, double money);
        Task transferToOwnAccount(string email, int fundAccSender, int fundAccReciever, double money, double newMoney);
        Task transferToOtherAccount(string email, int fundAccSender, int fundAccReciever, double money, double newMoney);

        //AUDIT
        Task<Audits> createAudit(string emailSender, string emailReciever, int fundAccSender, int fundAccReciever, double amount);
        Task<List<Audits>> getAudits(string email);
        Task<List<Audits>> getByDateAudits(string email, string dateFrom, string dateTo);


    }
}
