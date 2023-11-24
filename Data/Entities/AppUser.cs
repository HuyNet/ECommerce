using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AppUser :IdentityUser<Guid>
    {
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public DateTime BOD { get; set; }

        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }

        public List<Transaction> Transactions { get; set; }

    }
}
