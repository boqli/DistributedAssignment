using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Models
{
    public class User
    {
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
