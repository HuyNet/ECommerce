using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Transaction
    {
        public int TransactionId { set; get; }
        public DateTime TransactionDate { set; get; }
        public int ExternalTransactionId { set; get; }
        public decimal Amount { set; get; }
        public decimal Fee { set; get; }
        public string Result { set; get; }
        public string Message { set; get; }
        public TransactionStatus Status { set; get; }
        public String Provider { set; get; }

        public Guid UserId { set; get; }
        public AppUser AppUser { set; get; }

    }
}
