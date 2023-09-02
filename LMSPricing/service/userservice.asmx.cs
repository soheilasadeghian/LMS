using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace LMSPricing.service
{
    /// <summary>
    /// این سرویس جهت استفاده اپراتورهای سامانه مورد استفاده قرار میگیرد
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class userservice : System.Web.Services.WebService
    {
        #region user

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void registerUser(string key, string fullName, string tell, string image)
        {
            Context.Response.ContentEncoding = Encoding.UTF8;
            JavaScriptSerializer js = new JavaScriptSerializer();
            ClassCollection.Model.LongResult Result = new ClassCollection.Model.LongResult();
            Result.result = new ClassCollection.Model.Result();

            if (!ClassCollection.Methods.checkUserKey(key))
            {
                Result.result.code = -1;
                Result.result.message = ClassCollection.Message.OPERATION_NO_ACCESS;

                Context.Response.Write(js.Serialize(Result));
                return;
            }


            tell = tell.TrimEnd().TrimStart();
            fullName = fullName.TrimEnd().TrimStart();

            if (tell == "")
            {
                Result.result.code = 1;
                Result.result.message = "شماره تماس وارد شده صحیح نمی باشد";
                Context.Response.Write(js.Serialize(Result));
                return;
            }
            if (fullName == "")
            {
                Result.result.code = 2;
                Result.result.message = "نام وارد شده صحیح نمی باشد";
                Context.Response.Write(js.Serialize(Result));
                return;
            }

            DateTime dt = new DateTime();
            dt = DateTime.Now;
            var db = new DataAccessDataContext();

            if (db.userTbls.Any(c => c.mobile == tell))
            {
                Result.result.code = 3;
                Result.result.message = ClassCollection.Message.USER_EXIST;
                Context.Response.Write(js.Serialize(Result));
                return;
            }

            if (File.Exists(Server.MapPath( ClassCollection.Methods.getfilePath() + image)) == false)
            {
                Result.result.code = 4;
                Result.result.message = "عکس ارسال شده صحیح نمی باشد";
                Context.Response.Write(js.Serialize(Result));
                return;

            }

            var user = new userTbl();
            user.fullName = fullName;
            user.image = image;
            user.mobile = tell;
            user.regDate = dt;
            db.userTbls.InsertOnSubmit(user);
            db.SubmitChanges();

            Result.value = user.ID;
            Result.result.code = 0;
            Result.result.message = ClassCollection.Message.OPERATION_SUCCESS;
            Context.Response.Write(js.Serialize(Result));
            return;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Reserve(string key, long userID, int courseID)
        {
            Context.Response.ContentEncoding = Encoding.UTF8;
            JavaScriptSerializer js = new JavaScriptSerializer();
            ClassCollection.Model.Result Result = new ClassCollection.Model.Result();

            if (!ClassCollection.Methods.checkUserKey(key))
            {
                Result.code = -1;
                Result.message = ClassCollection.Message.OPERATION_NO_ACCESS;

                Context.Response.Write(js.Serialize(Result));
                return;
            }


            DateTime dt = new DateTime();
            dt = DateTime.Now;
            var db = new DataAccessDataContext();

            if (db.userTbls.Any(c => c.ID == userID) == false)
            {
                Result.code = 3;
                Result.message = ClassCollection.Message.USER_NOT_EXIST;
                Context.Response.Write(js.Serialize(Result));
                return;
            }

            if (db.userReserveTbls.Any(c => c.userID == userID && c.courseID == courseID))
            {
                Result.code = 4;
                Result.message = "این دوره قبلا رزرو شده است";
                Context.Response.Write(js.Serialize(Result));
                return;
            }


            var userReserve = new userReserveTbl();
            userReserve.courseID = courseID;
            userReserve.regDate = dt;
            userReserve.userID = userID;
            db.userReserveTbls.InsertOnSubmit(userReserve);

            db.SubmitChanges();

            Result.code = 0;
            Result.message = ClassCollection.Message.OPERATION_SUCCESS;
            Context.Response.Write(js.Serialize(Result));
            return;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CancelReserve(string key, long userID, int courseID)
        {
            Context.Response.ContentEncoding = Encoding.UTF8;
            JavaScriptSerializer js = new JavaScriptSerializer();
            ClassCollection.Model.Result Result = new ClassCollection.Model.Result();

            if (!ClassCollection.Methods.checkUserKey(key))
            {
                Result.code = -1;
                Result.message = ClassCollection.Message.OPERATION_NO_ACCESS;

                Context.Response.Write(js.Serialize(Result));
                return;
            }


            DateTime dt = new DateTime();
            dt = DateTime.Now;
            var db = new DataAccessDataContext();

            if (db.userTbls.Any(c => c.ID == userID) == false)
            {
                Result.code = 3;
                Result.message = ClassCollection.Message.USER_NOT_EXIST;
                Context.Response.Write(js.Serialize(Result));
                return;
            }

            if (db.userReserveTbls.Any(c => c.userID == userID && c.courseID == courseID)==false)
            {
                Result.code = 4;
                Result.message = "این دوره قبلا رزرو نشده است";
                Context.Response.Write(js.Serialize(Result));
                return;
            }


            var userReserve = db.userReserveTbls.Single(c => c.userID == userID && c.courseID == courseID);
         
            db.userReserveTbls.DeleteOnSubmit(userReserve);

            db.SubmitChanges();

            Result.code = 0;
            Result.message = "این دوره با موفقیت لغو رزرو شد";
            Context.Response.Write(js.Serialize(Result));
            return;

        }
        #endregion

    }
}
