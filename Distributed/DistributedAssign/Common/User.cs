using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }
    }


    [FirestoreData]
    public class Fund
    {
        [FirestoreProperty]
        public int BankAccountNo { get; set; }

        [FirestoreProperty]
        public string BankCode { get; set; }

        [FirestoreProperty]
        public string BankName { get; set; }

        [FirestoreProperty]
        public string Country { get; set; }

        [FirestoreProperty]
        public string CountryCode { get; set; }

        [FirestoreProperty]
        public double OpeningBalance { get; set; }
        [FirestoreProperty]
        public string currencyCode { get; set; }

        [FirestoreProperty]
        public string Payee { get; set; }

        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public bool isActive { get; set; }
    }

    [FirestoreData]
    public class Audits
    {
        [FirestoreProperty]
        public string DateOfLog { get; set; }

        [FirestoreProperty]
        public string Payee { get; set; }

        [FirestoreProperty]
        public string Reciever { get; set; }

        [FirestoreProperty]
        public string id { get; set; }

        [FirestoreProperty]
        public double ReceivedAmount { get; set; }

        [FirestoreProperty]
        public int recieverAccountNo { get; set; }

        [FirestoreProperty]
        public int senderAccountNo { get; set; }

    }

    public class Symbols{

        public string symbol { get; set; }
    }


}
