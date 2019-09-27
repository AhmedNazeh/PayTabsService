using Newtonsoft.Json;
using PayTabs.Web.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayTabs.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
      
        // test action 
        public async Task<ActionResult> SendPaymentAsync()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.paytabs.com/");
               
                PayTabsVM payTabs = new PayTabsVM()
                {
                    merchant_email = "ahmed.nazeh20@gmail.com",
                    secret_key = "fsjMvMj0g2KUYtgoSqaNiQ4Z1UHa3Yeiry5WkoXBqIwpcMO4cZSvPXuQSb1VageCOjz8HwK8egyr2mwfloKame1eq29Biod7HNgp",
                    address_shipping = "Flat 3021 Manama Bahrain",
                    ip_merchant = ServerClient.GetServerIPAddress(),
                    ip_customer = ServerClient.GetClientIP(),
                    cc_first_name = "ahmed",
                    cc_last_name = "nazeh",
                    title = "test Payment",
                    amount = 10,
                    currency = "SAR",
                    unit_price = "10",
                    discount = 0,
                    other_charges = 0,
                    products_per_title = "firstItem ",                   
                    quantity = "1",
                    email ="test@test.com",
                    billing_address ="test",
                    city_shipping ="test",
                    country_shipping = "SAU",
                    country = "SAU",
                    city = "SA",
                    state ="test",
                    state_shipping = "test",
                    shipping_first_name = "Ahmed",
                    shipping_last_name = "Ahmed",
                    
                    postal_code = "12345",
                    postal_code_shipping = "12345",
                    phone_number = "101308568",
                    cc_phone_number = "USA",
                    cms_with_version ="test",
                    reference_no = "0012",
                    return_url = "https://localhost:44355/Home/About",
                    site_url = Request.Url.GetLeftPart(UriPartial.Authority),
                    payment_type = "creditcard",
                    
                    
                    msg_lang ="Arabic",
                    
                    

                };
                var keyValueContent = payTabs.ToKeyValue();
                var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
                var urlEncodedString = await formUrlEncodedContent.ReadAsStringAsync();

                var result = await client.PostAsync("apiv2/create_pay_page", formUrlEncodedContent);
                string resultContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseVM>(resultContent);
                if(response.response_code == "4012")
                {
                    return Redirect(response.payment_url);
                }
                else
                {
                    return View();
                }

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}