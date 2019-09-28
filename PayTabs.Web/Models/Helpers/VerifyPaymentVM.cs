using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayTabs.Web.Models.Helpers
{
    public class VerifyPaymentVM
    {
        public string merchant_email { get; set; }
        public string secret_key { get; set; }
        public string payment_reference { get; set; }
    }

    public class ResponseCheckPaymentVM
    {
        public string result { get; set; }
        public string response_code { get; set; }
        public string pt_invoice_id { get; set; }
        public float amount { get; set; }
        public string currency { get; set; }
        public string reference_no { get; set; }
        public string transaction_id { get; set; }
    }
}