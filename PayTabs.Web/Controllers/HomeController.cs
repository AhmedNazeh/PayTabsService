using Newtonsoft.Json;
using PayTabs.Web.Models;
using PayTabs.Web.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace PayTabs.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
      
        // test action 
        public async Task<ActionResult> SendPaymentAsync()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var userId = User.Identity.GetUserId();

            var getOlderRef = db.UserPaymentInfo.FirstOrDefault(x => x.UserId == userId && !x.IsPayed ); // فحص هل يوجد رقم مسبق للدفع

            if(getOlderRef != null)
            {
                TempData["msg"] = " ";

                return View("ErrorResponse",new ResponseVM
                {
                    result = "عفوا لديك عملية مسبقه لم تقم بدفعها",
                    p_id = getOlderRef.PaymentReferenceId ,
                    response_code = getOlderRef.Id.ToString()
                });
            }

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
                    return_url = "https://localhost:44355/Home/CheckPaymentAsync",
                    site_url = Request.Url.GetLeftPart(UriPartial.Authority),
                   
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

                        db.UserPaymentInfo.Add(new UserPaymentInfo()
                        {
                            PaymentReferenceId = response.p_id,
                            UserId = userId,
                            CreationDate = DateTime.Now,
                            IsPayed = false
                            
                        });
                    
                    
                    db.SaveChanges();

                    return Redirect(response.payment_url);
                }
                else
                {
                    return View("ErrorResponse", new ResponseVM
                    {
                        result = response.result,
                       
                    }); // الرسالة الظاهره فى حالة وجود خطأ

                  
                }

            }
           
        }




        public async Task<ActionResult> CheckPaymentAsync()
        {
            var userId = User.Identity.GetUserId();
            var OlderRef = db.UserPaymentInfo.FirstOrDefault(x => x.UserId == userId && !x.IsPayed);
            if (OlderRef != null)
            {


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.paytabs.com/");

                    VerifyPaymentVM checkPayment = new VerifyPaymentVM()
                    {
                        merchant_email = "ahmed.nazeh20@gmail.com",
                        secret_key = "fsjMvMj0g2KUYtgoSqaNiQ4Z1UHa3Yeiry5WkoXBqIwpcMO4cZSvPXuQSb1VageCOjz8HwK8egyr2mwfloKame1eq29Biod7HNgp",
                        payment_reference = OlderRef.PaymentReferenceId
                    };
                    var keyValueContent = checkPayment.ToKeyValue();
                    var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
                    var urlEncodedString = await formUrlEncodedContent.ReadAsStringAsync();

                    var result = await client.PostAsync("apiv2/verify_payment", formUrlEncodedContent);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<ResponseCheckPaymentVM>(resultContent);
                    
                    if(response.response_code == "100")
                    {
                        OlderRef.InvoiceId = response.pt_invoice_id;
                        OlderRef.TransactionId = response.transaction_id;
                        OlderRef.IsPayed = true;
                        db.Entry(OlderRef).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return View("Thank", OlderRef);
                    }
                }
            }
            return View("ErrorResponse", new ResponseVM
            {
                result = "لا يوجد بيانات مسبقه لعمليات الدفع لهذا المستخدم",
             
            });
           
        }

        public ActionResult CancelPayment(long PayId)
        {
            var OlderRef = db.UserPaymentInfo.FirstOrDefault(x =>  x.Id == PayId);
            db.UserPaymentInfo.Remove(OlderRef);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Thank(ResponseCheckPaymentVM paymentVM)
        {
            if(paymentVM == null)
            {
                return RedirectToAction("Index");
            }
            return View(paymentVM);
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