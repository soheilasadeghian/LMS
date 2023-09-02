# LMS
Website for selling learning packages with Zarinpal payment gateway and a brief management panel

## Overview
The project “LMS” is a responsive website made with Asp.Net MVC Technology using
 C#, SQL, HTML, CSS, JavaScript, Bootstrap, JQuery, Ajax, and EntityFramework.<br/>
_implementation of ZarinPal payment gateway<br/>
_Panel for website (Login System with Session handling)<br/>
_with Database file

## Steps:
1. Restore DB in SQL Server from the DB file(lmspricingDB.sql) in root of repository <br/>
2. Open LMS Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will throw Home page of website<br/>
4. Open hamidiabetSite Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will show Homepage of website (the picture of homepage is end of this readme)<br/>
4. you can go to panel ( add to url /admin) in Login form enter username and password as admin

Admin Url: ./admin<br/>
Admin Username: "username"<br/>
Password : "password"

## About implementation Zarin Pal payment gateway

![alt text](https://github.com/soheilasadeghian/LMS/blob/main/LMSPricing/images/zarrinpall.png)

_Download and install zarinpal package from NuGet.<br/>
_controller for handling this scenario in Homecontroller: [code](https://github.com/soheilasadeghian/LMS/blob/main/LMSPricing/Controllers/HomeController.cs)<br/>
<br/>
_create and send request<br/>
_Catch Authority<br/>
_save factor and Authority in DB<br/>
_redirect user to zarinpal StartPay page with authority

```C#
description = "Buying a bronze package";
mobile = "+989121212121" ;
email = "soheila.sadeghian89@gmail.com";
price = 1000;
string merchant = ConfigurationManager.AppSettings["merchant"];
string Authority = "";

zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new zarinpal.PaymentGatewayImplementationServicePortTypeClient();
int Status = zp.PaymentRequest(merchant, price, description, email, mobile, "http://our_website_name/result", out Authority);

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
```
<br>
_after payment by user, zarinpal send authority and payment status to server(call OrderResult in Homecontroller)<br>
_server send request to zarinpal to get RefID(validate transaction)<br>
_save RefID in DB<br>
<br>

```c#
public ActionResult OrderResult()
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

                return View("OrderResult", model as object);
            }
    }
}
```



LMS Website:<br>
![alt text](https://github.com/soheilasadeghian/LMS/blob/main/LMSPricing/images/lms-screenshot.png)

## Support
For support, [click here](https://github.com/soheilasadeghian).

## Give a star ⭐️ !!!
If you liked the project, please give a star :)

## License
[MIT](https://github.com/soheilasadeghian/LMS/blob/main/LICENSE)


