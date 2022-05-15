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
        public FireStoreDataAccess(string project) //google project id
        {
              db = FirestoreDb.Create(project);
        }

        public async void AddUser(User user)
        {
            DocumentReference docRef = db.Collection("users").Document(user.Email);
            await docRef.SetAsync(user);
        }

        public void DeleteUser(string email)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
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

       

    }
}
