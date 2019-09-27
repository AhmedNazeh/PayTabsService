using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayTabs.Web.Models.Helpers
{
    public class PayTabsVM
    {
        public string merchant_email { get; set; }
        public string secret_key { get; set; }
        public string site_url { get; set; }
        public string return_url { get; set; }
        public string title { get; set; }
        public string cc_first_name { get; set; }
        public string cc_last_name { get; set; }
        public string cc_phone_number { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string products_per_title { get; set; }
        public string unit_price { get; set; }
        public string quantity { get; set; }
        public float other_charges { get; set; }
        public float amount { get; set; }
        public float discount { get; set; }
        public string currency { get; set; }
        public string reference_no { get; set; }
        public string ip_customer { get; set; } // client IP
        public string ip_merchant { get; set; } // server IP
        public string billing_address { get; set; }
        public string state { get; set; }
        [MaxLength(50)]
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string shipping_first_name { get; set; }
        public string shipping_last_name { get; set; }
        public string address_shipping { get; set; }
        public string city_shipping { get; set; }
        public string state_shipping { get; set; }
        public string postal_code_shipping { get; set; }
        public string country_shipping { get; set; }
        public string msg_lang { get; set; }
        public string cms_with_version { get; set; }
        public string payment_type { get; set; }

    }

    public enum Country
    {
        ARE,
        SAU,
        USA
    }
    public enum Currency
    {
        USD ,
        AED,
        SAR
    }

    public enum PhoneCodes
    {
        UAE = 00971,
        USA = 001
    }

    public enum PaymentType
    {
        creditcard,
        sadad,
        mada,
        stcpay,
        knpay,
        omannet,
        amex
    }
}