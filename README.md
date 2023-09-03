# LMS 🔥
Website for selling learning packages with Zarinpal payment gateway and a brief management panel

:star: Star me on GitHub — it helps!


[![Maintenance](https://img.shields.io/badge/maintained-yes-green.svg)](https://github.com/SoheilaSadeghian/SoheilaSadeghian.github.io)
[![Ask Me Anything !](https://img.shields.io/badge/ask%20me-linkedin-1abc9c.svg)](https://www.linkedin.com/in/SoheilaSadeghian/)
[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](https://github.com/soheilasadeghian/LMS/blob/main/LICENSE)
[![Ask Me Anything !](https://img.shields.io/badge/production%20year-2017-1abc9c.svg)]()



## Overview
The project “LMS” is a responsive website made with Asp.Net MVC Technology using
 C#, SQL, HTML, CSS, JavaScript, Bootstrap, JQuery, Ajax, and EntityFramework.<br/>

## Features 📋
⚡️ implementation of ZarinPal payment gateway\
⚡️ Panel for website (Login System with Session handling)\
⚡️ with Database file

## Installation Steps 📦 
1. Restore DB in SQL Server from the DB file `lmspricingDB.sql` in root of repository <br/>
2. Open LMS Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will throw Home page of website<br/>
4. Open hamidiabetSite Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will show Homepage of website (the picture of homepage is end of this readme)<br/>
4. you can go to panel ( add to url `/admin`) in Login form enter username and password as admin

## Admin User
✔️ Admin Url: ./admin\
✔️ Admin Username: "username"\
✔️ Password : "password"\


## Tools Used 🛠️
*  Visual studio app,Sql server app
*  Asp.Net MVC, C#, SQL, HTML, CSS, JavaScript, Bootstrap, JQuery, Ajax, and EntityFramework

## Contributing implementation Zarin Pal payment gateway 💡

![alt text](https://github.com/soheilasadeghian/LMS/blob/main/LMSPricing/images/zarrinpall.png)

✔️ Download and install zarinpal package from NuGet.
✔️ controller for handling this scenario in Homecontroller: [code](https://github.com/soheilasadeghian/LMS/blob/main/LMSPricing/Controllers/HomeController.cs)

<br/>

- **create and send request with**
- **Catch Authority**
- **save factor and Authority in DB**
- **redirect user to zarinpal StartPay page with authority**

✔️ a unique identification number attached to a business that tells the payment processing systems involved in a transaction where to send which funds\

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

- **after payment by user, zarinpal send authority and payment status to server(call OrderResult in Homecontroller)**
- **server send request to zarinpal to get RefID(validate transaction)**
- **save RefID in DB**


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


