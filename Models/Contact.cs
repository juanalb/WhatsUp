using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsUp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MobileNumber { get; set; }

        public int OwnerAccountId { get; set; }
        public Account OwnerAccount { get; set; }

        public int? ContactAccountId { get; set; }
        public Account ContactAccount { get; set; }

    }
}