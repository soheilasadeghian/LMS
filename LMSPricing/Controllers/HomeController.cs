using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LMSPricing.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order(int? packetID)
        {
            var model = new Models.RegisterOrderViewModel();
            model.email.value = "";
            model.explain.value = "";
            model.fullName.value = "";
            model.hostList.value = 0;
            model.mobile.value = "";
            model.packetList.value = 0;
            if (packetID.HasValue == true && packetID.Value >= 0 && packetID.Value <= 4)
            {
                model.packetList.value = packetID.Value;
            }
            return View(model as object);
        }
        public ActionResult Agent()
        {
            var model = new Models.AgentViewModel();
            model.email.value = "";
            model.explain.value = "";
            model.fullName.value = "";
            model.company.value = "";
            model.mobile.value = "";
            model.address.value = "";

            return View(model as object);
        }
        [HttpPost]
        public ActionResult Agent(string company, string address, string email, string fullName, string explain, string mobile)
        {
            var model = new Models.AgentViewModel();

            if (!ModelState.IsValid)
            {
                return Redirect("/");
            }

            bool hasError = false;

            #region validation

            if (fullName == "")
            {
                model.fullName.message = "نام و نام‌خانوادگی وارد شده صحیح نمی‌باشد";
                hasError = true;
            }
            else if (mobile == "" || IsValidMobile(mobile) == false)
            {
                model.mobile.message = "موبایل وارد شده صیحی نمی‌باشد";
                hasError = true;
            }

            else if (email == "" || IsValidEmail(email) == false)
            {
                model.email.message = "ایمیل وارد شده صحیح نمی‌باشد";
                hasError = true;
            }

            else if (address == "")
            {
                model.address.message = "لطفا آدرس خود را وارد نمایید";
                hasError = true;
            }


            model.company.value = company;
            model.address.value = address;
            model.email.value = email;
            model.fullName.value = fullName;
            model.mobile.value = mobile;
            model.explain.value = explain;
            #endregion

            if (!hasError)
            {
                var db = new DataAccessDataContext();
                var dt = new DateTime();
                dt = DateTime.Now;
                var ag = new agentTbl();

                ag.address = address;
                ag.company = company;
                ag.description = explain;
                ag.email = email;
                ag.fullName = fullName;
                ag.mobile = mobile;
                ag.regDate = dt;

                db.agentTbls.InsertOnSubmit(ag);
                db.SubmitChanges();

                var Error = new Models.OrderResultViewModel();
                Error.Result = new Models.BaseObject<string>();
                Error.Result.isError = false;
                Error.Result.message = "درخواست شما با موفقیت ارسال شد";
                Error.Result.value = (ag.ID + 1920) + "";

                sendSms("989128481421", "درخواست نمایندگی دورآموز : " + ag.fullName + "\n" + "کد رهگیری : " + Error.Result.value);
                sendSms("989128481421", "درخواست نمایندگی دورآموز : " + ag.fullName + "\n" + "کد رهگیری : " + Error.Result.value);

                return View("AgentRequestResult", Error as object);

            }
            return View(model as object);
        }

        [HttpPost]
        public ActionResult Order(string packetList, string hostList, string email, string fullName, string explain, string mobile)
        {
            var model = new Models.RegisterOrderViewModel();

            if (!ModelState.IsValid)
            {
                return Redirect("/");
            }

            bool hasError = false;

            #region validation


            if (packetList == "0")
            {
                model.packetList.message = "یکی از بسته های موجود را انتخاب نمایید";
                hasError = true;
            }
            else if (hostList == "0")
            {
                model.hostList.message = "یکی از گزینه های فضای میزبانی را انتخاب نمایید";
                hasError = true;
            }
            else if (fullName == "")
            {
                model.fullName.message = "نام و نام‌خانوادگی وارد شده صحیح نمی‌باشد";
                hasError = true;
            }
            else if (mobile == "" || IsValidMobile(mobile) == false)
            {
                model.mobile.message = "موبایل وارد شده صیحی نمی‌باشد";
                hasError = true;
            }
            else if (email == "" || IsValidEmail(email) == false)
            {
                model.email.message = "ایمیل وارد شده صحیح نمی‌باشد";
                hasError = true;
            }


            model.packetList.value = int.Parse(packetList);
            model.hostList.value = int.Parse(hostList);
            model.email.value = email;
            model.fullName.value = fullName;
            model.mobile.value = mobile;
            model.explain.value = explain;
            #endregion

            string description = "";
            string hostDomain = "";

            if (hasError == false)
            {
                int price = 0;
                if (model.packetList.value == 1)
                {
                    price = 5000000;
                    description = "خرید بسته ی برلیان دورآموز";
                }
                else if (model.packetList.value == 2)
                {
                    price = 3000000;
                    description = "خرید بسته ی طلایی دورآموز";
                }
                else if (model.packetList.value == 3)
                {
                    price = 2000000;
                    description = "خرید بسته ی نقره ای دورآموز";
                }
                else if (model.packetList.value == 4)
                {
                    price = 1000000;
                    description = "خرید بسته ی برنز دورآموز";
                }

                if (model.hostList.value == 1)
                {
                    hostDomain = "فضای میزبانی و دامنه دارم";
                }
                else if (model.hostList.value == 2)
                {
                    hostDomain = "فضای میزبانی ندارم، دامنه دارم";
                }
                else if (model.hostList.value == 3)
                {
                    hostDomain = "فضای میزبانی دارم، دامنه ندارم";
                }
                else if (model.hostList.value == 4)
                {
                    hostDomain = "فضای میزبانی ندارم، دامنه ندارم";
                }

                  //price = 1000;
                string merchant = ConfigurationManager.AppSettings["merchant"];
                string Authority = "";

                zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new zarinpal.PaymentGatewayImplementationServicePortTypeClient();
                int Status = zp.PaymentRequest(merchant, price, description, email, mobile, "http://lms.tana.ir/result", out Authority);

                if (Status == 100)
                {

                    var db = new DataAccessDataContext();
                    var dt = new DateTime();
                    dt = DateTime.Now;

                    var factor = new factorTbl();

                    factor.Authority = Authority;
                    factor.description = explain;
                    factor.email = email;
                    factor.fullName = fullName;
                    factor.hostDomain = hostDomain;
                    factor.mobile = mobile;
                    factor.packetTitle = description;
                    factor.regDate = dt;
                    factor.price = price;
                    

                    db.factorTbls.InsertOnSubmit(factor);
                    db.SubmitChanges();

                    Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
                }
                else
                {
                    var Error = new Models.OrderResultViewModel();
                    Error.Result = new Models.BaseObject<string>();
                    Error.Result.isError = true;
                    Error.Result.message = "خطا در اتصال به درگاه پرداخت";

                    return View("OrderResult", Error as object);
                }

            }

            return View(model as object);
        }

        public ActionResult OrderResult()
        {
            var model = new Models.OrderResultViewModel();
            model.Result = new Models.BaseObject<string>();

            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {

                    long RefID;

                    System.Net.ServicePointManager.Expect100Continue = false;
                    zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new zarinpal.PaymentGatewayImplementationServicePortTypeClient();
                    string merchant = ConfigurationManager.AppSettings["merchant"];

                    var Authority = Request.QueryString["Authority"].ToString();

                    var db = new DataAccessDataContext();
                    var fac = db.factorTbls.Single(c => c.Authority == Authority);

                    int Status = zp.PaymentVerification(merchant, Authority, fac.price, out RefID);

                    if (Status == 100)
                    {
                        model.Result.isError = false;
                        model.Result.message = "عملیات پرداخت با موفقیت انجام شد";
                        model.Result.value = RefID + "";

                        fac.RefID = RefID + "";
                        db.SubmitChanges();

                        sendSms("989128481421", "خرید دورآموز به مبلغ : " + fac.price + "تومان " + "\n" + "کد رهگیری : " + RefID);
                        sendSms("989128481421", "خرید دورآموز به مبلغ : " + fac.price + "تومان " + "\n" + "کد رهگیری : " + RefID);

                        return View("OrderResult", model as object);

                    }
                    else
                    {
                        var Error = new Models.OrderResultViewModel();
                        Error.Result = new Models.BaseObject<string>();
                        Error.Result.isError = true;
                        Error.Result.message = "خطا در عملیات پرداخت";

                        return View("OrderResult", Error as object);
                    }

                }
                else
                {
                    var Error = new Models.OrderResultViewModel();
                    Error.Result = new Models.BaseObject<string>();
                    Error.Result.isError = true;
                    Error.Result.message = "خطا در اتصال به درگاه پرداخت";

                    return View("OrderResult", Error as object);
                }
            }
            else
            {
                var Error = new Models.OrderResultViewModel();
                Error.Result = new Models.BaseObject<string>();
                Error.Result.isError = true;
                Error.Result.message = "خطا در اتصال به درگاه پرداخت";

                return View("OrderResult", Error as object);
            }

        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        bool IsValidMobile(string mobile)
        {
            try
            {
                if (mobile.Length == 11 && mobile.StartsWith("09") && !mobile.Any(c => c != '1' && c != '2' && c != '3' && c != '4' && c != '5' && c != '6' && c != '7' && c != '8' && c != '9' && c != '0'))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public static void sendSms(string to, string text)
        {
            new Thread(() =>
            {
                try
                {
                    var durl = "http://172.16.32.29:9501/api?action=sendmessage&username=cwm&password=Abc123??&recipient=" + to + "&messagetype=SMS:TEXT&messagedata=" + text;
                    System.Net.WebRequest req = System.Net.WebRequest.Create(durl);

                    System.Net.WebResponse resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    var res = sr.ReadToEnd().Trim();

                }
                catch
                {

                }
            }).Start();
        }
    }
}