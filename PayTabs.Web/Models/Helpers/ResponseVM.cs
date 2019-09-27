using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayTabs.Web.Models.Helpers
{
    public class ResponseVM
    {
        public string result { get; set; }
        public string response_code { get; set; }
        public string payment_url { get; set; }
        public long p_id { get; set; }
    }
}