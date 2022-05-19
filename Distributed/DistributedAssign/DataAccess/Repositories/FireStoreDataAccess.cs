using Common;
using DataAccess.Interfaces;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;

namespace DataAccess.Repositories
{
    public class FireStoreDataAccess : IFireStoreDataAccess
    {
        private FirestoreDb db { get; set; }
        public FireStoreDataAccess(string project)
        {
              db = FirestoreDb.Create(project);
        }
        // ----------------- USER BULLSHIT ------------------------
        public async void AddUser(User user)
        {
            DocumentReference docRef = db.Collection("users").Document(user.Email);
            await docRef.SetAsync(user);
        }

        public async Task<User> GetUser(string email)
        {
            DocumentReference docRef = db.Collection("users").Document(email);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                //Console.WriteLine("Document data for {0} document:", snapshot.Id);
                //Dictionary<string, object> city = snapshot.ToDictionary();
                //foreach (KeyValuePair<string, object> pair in city)
                //{
                //    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                //}
                User myUser = snapshot.ConvertTo<User>();
                return myUser;
            }
            else
            {
                return null; 
            }
        }
        public async Task<User> GetUsers()
        {
            DocumentReference docRef = db.Collection("users").Document();
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                User myUser = snapshot.ConvertTo<User>();
                return myUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            Query messageQuery = db.Collection("users");
            QuerySnapshot messageQuerySnapshot = await messageQuery.GetSnapshotAsync();
            List<User> messages = new List<User>();
            foreach (DocumentSnapshot documentSnapshot in messageQuerySnapshot.Documents)
            {
                messages.Add(documentSnapshot.ConvertTo<User>());
            }
            return messages;
        }

        // ----------------- FUNDS ------------------------
        public async Task<Fund> createFund(string bankCode, string BankName, string Country, string CountryCode, int openingBal, string payee)
        {
            Task<int> count = getAllFunds();
            int num = count.Result + 1;
            
            DocumentReference docRef = db.Collection("fund").Document(num.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return null;
            }
            else
            {
                Fund fund = new Fund();
                fund.BankAccountNo = num;
                fund.BankCode = bankCode;
                fund.BankName = BankName;
                fund.Country = Country;
                fund.CountryCode = CountryCode;
                fund.OpeningBalance = openingBal;
                fund.Payee = payee;
                fund.id = Guid.NewGuid().ToString();
                fund.isActive = true;

                await docRef.SetAsync(fund);
                return fund;
            }
        }

        public async Task<int> getAllFunds()
        {
            Query messageQuery = db.Collection("fund");
            QuerySnapshot messageQuerySnapshot = await messageQuery.GetSnapshotAsync();
            List<Fund> messages = new List<Fund>();
            foreach (DocumentSnapshot documentSnapshot in messageQuerySnapshot.Documents)
            {
                messages.Add(documentSnapshot.ConvertTo<Fund>());
            }
            return messages.Count;
        }

        public async Task<List<Fund>> getAllFunds(string email)
        {
            Query messageQuery = db.Collection("fund").WhereEqualTo("Payee",email);
            QuerySnapshot messageQuerySnapshot = await messageQuery.GetSnapshotAsync();
            List<Fund> messages = new List<Fund>();
            foreach (DocumentSnapshot documentSnapshot in messageQuerySnapshot.Documents)
            {
                messages.Add(documentSnapshot.ConvertTo<Fund>());
            }
            return messages;
        }

        public async Task<List<Fund>> getSpecificFund(string email, int bankAccNo)
        {
            Query messageQuery = db.Collection("fund").WhereEqualTo("Payee", email).WhereEqualTo("BankAccountNo",bankAccNo);
            QuerySnapshot messageQuerySnapshot = await messageQuery.GetSnapshotAsync();
            List<Fund> messages = new List<Fund>();
            foreach (DocumentSnapshot documentSnapshot in messageQuerySnapshot.Documents)
            {
                messages.Add(documentSnapshot.ConvertTo<Fund>());
            }
            return messages;
        }

        public async Task<List<Fund>> getSpecificFundWithId(string email, int bankAccNo)
        {
            Query messageQuery = db.Collection("fund").WhereEqualTo("BankAccountNo", bankAccNo);
            QuerySnapshot messageQuerySnapshot = await messageQuery.GetSnapshotAsync();
            List<Fund> messages = new List<Fund>();
            foreach (DocumentSnapshot documentSnapshot in messageQuerySnapshot.Documents)
            {
                messages.Add(documentSnapshot.ConvertTo<Fund>());
            }
            return messages;
        }
        public async void deactivate(string email, int bankAccNo)
        {
            List<Fund> fund = await getSpecificFund(email, bankAccNo);
            DocumentReference doc = db.Collection("fund").Document(bankAccNo.ToString());
            if (fund[0].isActive == true)
            {
                Dictionary<string, object> u = new Dictionary<string, object>
                {
                    {"isActive", false }
                };
                await doc.UpdateAsync(u);
            }
            else
            {
                Dictionary<string, object> u = new Dictionary<string, object>
                {
                    {"isActive", true }
                };
                await doc.UpdateAsync(u);
            }
            
        }

        //-------------------------TRANSACTIONS---------------------------
        public async void Deposit(string email, int bankAccNo, double money)
        {
            List<Fund> fund = await getSpecificFund(email, bankAccNo);
            if (fund.Count == 1)
            {
                DocumentReference doc = db.Collection("fund").Document(bankAccNo.ToString());
                Dictionary<string, object> u = new Dictionary<string, object>
                {
                    {"OpeningBalance", fund[0].OpeningBalance+ money }
                };
                await doc.UpdateAsync(u);
                Task<Audits> aud = createAudit(fund[0].Payee, "Self", bankAccNo, bankAccNo, money);
            }
        }

        public async Task transferToOwnAccount(string email, int fundAccSender, int fundAccReciever ,double money)
        {
            List<Fund> fundSender = await getSpecificFund(email, fundAccSender);
            List<Fund> fundReciever = await getSpecificFund(email, fundAccReciever);
            //check currency 
            if(fundSender[0].Payee == fundReciever[0].Payee)
            {
                if(fundSender[0].OpeningBalance > money)
                {
                    DocumentReference doc = db.Collection("fund").Document(fundAccReciever.ToString());
                    Dictionary<string, object> u = new Dictionary<string, object>
                    {
                        {"OpeningBalance", fundReciever[0].OpeningBalance+ money }
                    };
                    DocumentReference doc2 = db.Collection("fund").Document(fundAccSender.ToString());
                    Dictionary<string, object> u2 = new Dictionary<string, object>
                    {
                        {"OpeningBalance", fundSender[0].OpeningBalance- money }
                    };
                    var a = await doc.UpdateAsync(u);
                    var b = await doc2.UpdateAsync(u2);
                    Task<Audits> aud = createAudit(fundSender[0].Payee, "Self", fundAccSender, fundAccReciever, money);
                } 
            }
        }

        public async Task transferToOtherAccount(string email, int fundAccSender, int fundAccReciever, double money)
        {
            List<Fund> fundSender = await getSpecificFund(email, fundAccSender);
            List<Fund> fundReciever = await getSpecificFundWithId(email, fundAccReciever);

            if (fundSender[0].Payee != fundReciever[0].Payee)
            {
                if (fundSender[0].OpeningBalance > money)
                {
                    DocumentReference doc = db.Collection("fund").Document(fundAccReciever.ToString());
                    Dictionary<string, object> u = new Dictionary<string, object>
                    {
                        {"OpeningBalance", fundReciever[0].OpeningBalance+ money }
                    };
                    DocumentReference doc2 = db.Collection("fund").Document(fundAccSender.ToString());
                    Dictionary<string, object> u2 = new Dictionary<string, object>
                    {
                        {"OpeningBalance", fundSender[0].OpeningBalance- money }
                    };
                    var a = await doc.UpdateAsync(u);
                    var b = await doc2.UpdateAsync(u2);

                    Task<Audits> aud = createAudit(fundSender[0].Payee, fundReciever[0].Payee, fundAccSender, fundAccReciever, money);
                }
            }
        }

        //-------------------------AUDITS/LOGS

        public async Task<Audits> createAudit(string emailSender, string emailReciever, int fundAccSender, int fundAccReciever, double amount)
        {
            string id = Guid.NewGuid().ToString();
            DocumentReference docRef = db.Collection("audits").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return null;
            }
            else
            {
                Audits audit = new Audits();
                var dateTimeNow = DateTime.Now; 
                var dateOnlyString = dateTimeNow.ToShortDateString();
                audit.DateOfLog = dateOnlyString;
                audit.Payee = emailSender;
                audit.id = id;
                audit.ReceivedAmount = amount;
                audit.recieverAccountNo = fundAccReciever;
                audit.senderAccountNo = fundAccSender;
                audit.Reciever = emailReciever;
                await docRef.SetAsync(audit);
                return audit;
            }
        }

        public async Task<List<Audits>> getAudits(string email)
        {
            Query auditQuery1 = db.Collection("audits").WhereEqualTo("Payee", email);
            QuerySnapshot snapshot1 = await auditQuery1.GetSnapshotAsync();
            List<Audits> auditsList1 = new List<Audits>();

            Query auditQuery2 = db.Collection("audits").WhereEqualTo("Reciever", email);
            QuerySnapshot snapshot2 = await auditQuery2.GetSnapshotAsync();
            //List<Audits> auditsList2 = new List<Audits>();

            foreach (DocumentSnapshot documentSnapshot in snapshot1.Documents)
            {
                auditsList1.Add(documentSnapshot.ConvertTo<Audits>());
            }
            foreach (DocumentSnapshot documentSnapshot in snapshot2.Documents)
            {
                auditsList1.Add(documentSnapshot.ConvertTo<Audits>());
            }

            return auditsList1;
        }

        public async Task<List<Audits>> getByDateAudits(string email, string dateFrom, string dateTo)
        {
            CollectionReference logRef = db.Collection("audits"); //.WhereEqualTo("Payee", email);
            Query auditQuery1 = logRef.WhereGreaterThanOrEqualTo("DateOfLog", dateFrom).WhereLessThanOrEqualTo("DateOfLog",dateTo);
            QuerySnapshot snapshot1 = await auditQuery1.GetSnapshotAsync();
            List<Audits> auditsList1 = new List<Audits>();
            List<Audits> auditsList2 = new List<Audits>();

            foreach (DocumentSnapshot documentSnapshot in snapshot1.Documents)
            {
                auditsList1.Add(documentSnapshot.ConvertTo<Audits>());
            }

            for(int i=0; i<auditsList1.Count; i++)
            {
                if(email == auditsList1[i].Payee || email == auditsList1[i].Reciever)
                {
                    auditsList2.Add(auditsList1[i]);
                }
            }

            return auditsList2;
        }


    }
}
