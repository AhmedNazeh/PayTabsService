using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayTabs.Web.Models
{
    public class UserPaymentInfo
    {
        [Key]
        public long Id { get; set; }
        public string UserId { get; set; }
        public string PaymentReferenceId { get; set; }
        public bool IsPayed { get; set; }
        public DateTime CreationDate { get; set; }
        public string InvoiceId { get; set; }
        public string TransactionId { get; set; }

    }
}