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
        /*
        public async void minusCredit(string email)
        {
            var myUser = await GetUser(email);
            var final = myUser.creditCount -1;
            DocumentReference docRef = db.Collection("users").Document(email);
            Dictionary<string, object> u = new Dictionary<string, object>
            {
                { "creditCount", final }
            };
            await docRef.UpdateAsync(u);
        }
        */
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
        public Task<List<Fund>> createFund()
        {
            throw new NotImplementedException();
        }

        public Task<List<Fund>> getAllFunds(string email)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fund>> getSpecificFund(string email, int bankAccNo)
        {
            throw new NotImplementedException();
        }

        public async void deactivate(string email, int bankAccNo)
        {
            DocumentReference doc = db.Collection("fund").Document(bankAccNo.ToString());
            Dictionary<string, object> u = new Dictionary<string, object>
            {
                {"isActive", false }
            };
            await doc.UpdateAsync(u);
        }

        public async void Deposit(string email, int bankAccNo, double money)
        {
            DocumentReference doc = db.Collection("fund").Document(bankAccNo.ToString());
            Dictionary<string, object> u = new Dictionary<string, object>
            {
                {"OpeningBalance", money }
            };
            await doc.UpdateAsync(u);
        }



    }
}
