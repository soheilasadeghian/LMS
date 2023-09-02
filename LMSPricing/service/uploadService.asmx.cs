using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace LMSPricing.service
{
    /// <summary>
    /// این سرویس جهت آپلود فایل میباشد
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class uploadService : System.Web.Services.WebService
    {
        /// <summary>
        /// این متد وظیفه ارسال توکن جهت شروع یک آپلود فایل را بر عهده دارد
        /// </summary>
        /// <param name="key">کلمه عبور برنامه ای که درحال استفاده از متد جاری می باشد</param>
        /// <param name="packetCount">تعداد بسته هایی که قرار است در آپلود مورد نظر ارسال گردد</param>
        /// <param name="fileFormat">نوع فایلی که درحال آپلود آن هستیم</param>
        /// <returns>
        /// خروجی ساختار جی سون از کلاس زیر را دارد.
        /// <para><see cref="ClassCollection.Model.UploadRequestResult"/></para>
        /// <remarks>
        /// <para>Result.Code : -1 , Result.Message : <see cref="ClassCollection.Messages.OPERATION_NO_ACCESS"/>, Result.Token : <c>null</c></para>
        /// <para>Result.Code : 0 , Result.Message : <see cref="ClassCollection.Messages.UPLOAD_TOKEN_SENT"/> , Result.Token : String </para>
        /// <para>Result.Code : 1 , Result.Message : <see cref="ClassCollection.Messages.INPUT_ERROR_PACKET_COUNT_INCORRECT"/> , Result.Token : <c>null</c></para>
        /// <para>Result.Code : 2 , Result.Message : <see cref="ClassCollection.Messages.INPUT_ERROR_FILE_FORMAT_INCORRECT"/> , Result.Token : <c>null</c></para>
        /// </remarks>
        /// </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void uploadRequest(string key, int packetCount, string fileFormat)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ClassCollection.Model.UploadRequestResult Result = new ClassCollection.Model.UploadRequestResult();
            Result.Result = new ClassCollection.Model.Result();

            fileFormat = fileFormat.Trim();

            var userkey = ConfigurationManager.AppSettings["UserKey"];
            if (key != userkey)
            {
                Result.Result.code = -1;
                Result.Result.message = ClassCollection.Message.OPERATION_NO_ACCESS;
                Result.Token = null;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }

            if (packetCount <= 0)
            {
                Result.Result.code = 1;
                Result.Result.message = ClassCollection.Message.INPUT_ERROR_PACKET_COUNT_INCORRECT;
                Result.Token = null;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }

            if (fileFormat.Length == 0)
            {
                Result.Result.code = 2;
                Result.Result.message = ClassCollection.Message.INPUT_ERROR_FILE_FORMAT_INCORRECT;
                Result.Token = null;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }

            var db = new DataAccessDataContext();
            var dt = new DateTime();
            dt = DateTime.Now;

            var upload = new tokenUploadServiceTbl();
            upload.format = fileFormat;
            upload.packetCount = packetCount;
            upload.token = "";
            upload.upDate = dt;
            upload.receivedPacket = 0;

            db.tokenUploadServiceTbls.InsertOnSubmit(upload);
            db.SubmitChanges();

            var token = ClassCollection.Methods.md5(upload.ID.ToString());

            upload.token = token;
            db.SubmitChanges();

            Result.Token = upload.token;
            Result.Result.code = 0;
            Result.Result.message = ClassCollection.Message.UPLOAD_TOKEN_SENT;

            Context.Response.Write(js.Serialize(Result).ToLower());
            return;
        }

        /// <summary>
        /// این متد وظیفه ساخت فایل مورد نظر با استفاده از بسته های آپلود شده فایل را بر عهده دارد
        /// </summary>
        /// <param name="key">کلمه عبور برنامه ای که درحال استفاده از متد جاری می باشد</param>
        /// <param name="token">توکن فایل مورد درخواست</param>
        /// <returns>
        /// خروجی ساختار جی سون از کلاس زیر را دارد.
        /// <para><see cref="ClassCollection.Model.UploadFileResult"/></para>
        /// <remarks>
        /// <para>Result.Code : -1 , Result.Message : <see cref="ClassCollection.Messages.OPERATION_NO_ACCESS"/>, Result.image : <c>null</c></para>
        /// <para>Result.Code : 0 , Result.Message : <see cref="ClassCollection.Messages.UPLOAD_FILE_SUCCESS"/> , Result.image : <c>String </c></para>
        /// <para>Result.Code : 1 , Result.Message : <see cref="ClassCollection.Messages.TOKEN_ERROR_NOT_EXIST"/> , Result.image : <c>null</c></para>
        /// <para>Result.Code : 2 , Result.Message : <see cref="ClassCollection.Messages.UPLOAD_FILE_FAILED"/> , Result.image : <c>null</c></para>
        /// <para>Result.Code : 3 , Result.Message : <see cref="ClassCollection.Messages.UPLOAD_FILE_ERROR_NOT_FINISHED"/> , Result.image : <c>null</c></para>
        /// </remarks>
        /// </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UploadFile(string key, string token)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ClassCollection.Model.UploadFileResult Result = new ClassCollection.Model.UploadFileResult();
            Result.Result = new ClassCollection.Model.Result();
            //File.WriteAllText(Server.MapPath( ClassCollection.Methods.getfilePath() + "log.txt"), "hoooy");
            var userkey = ConfigurationManager.AppSettings["UserKey"];
            if (key != userkey)
            {
                Result.Result.code = -1;
                Result.Result.message = ClassCollection.Message.OPERATION_NO_ACCESS;
                Result.image = null;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }
            var db = new DataAccessDataContext();
            if (db.tokenUploadServiceTbls.Count(c => c.token == token) == 0)
            {
                Result.Result.code = 1;
                Result.Result.message = ClassCollection.Message.TOKEN_ERROR_NOT_EXIST;
                Result.image = null;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }
            var tokenRec = db.tokenUploadServiceTbls.Single(c => c.token == token);
         
            if (tokenRec.receivedPacket == tokenRec.packetCount)
            {
                string base64 = "";
                var t = db.packetUploadServiceTbls.Where(c => c.token == tokenRec.token).OrderBy(c => c.ID);
                foreach (var item in t)
                {
                    base64 += item.data;
                }
                var path = ClassCollection.Methods.getfilePath();
                var filename = "lms" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + "." + tokenRec.format;

                var file = ClassCollection.Methods.saveBase64FileToServer(base64, Server.MapPath(path + filename));
                if (file.Contains("errr")==false)
                {
                    Result.Result.code = 0;
                    Result.Result.message = ClassCollection.Message.UPLOAD_FILE_SUCCESS;
                    Result.image = filename ;

                    db.packetUploadServiceTbls.DeleteAllOnSubmit(db.packetUploadServiceTbls.Where(c => c.token == tokenRec.token));
                    db.tokenUploadServiceTbls.DeleteOnSubmit(tokenRec);
                    db.SubmitChanges();

                    Context.Response.Write(js.Serialize(Result).ToLower());
                    return;
                }
                else
                {
                    Result.Result.code = 2;
                    Result.Result.message = file;// ClassCollection.Message.UPLOAD_FILE_FAILED;
                    Result.image = null;

                    Context.Response.Write(js.Serialize(Result).ToLower());
                    return;
                }
            }
            else
            {
                Result.Result.code = 3;
                Result.Result.message = ClassCollection.Message.UPLOAD_FILE_ERROR_NOT_FINISHED;
                Result.image = null;

                Context.Response.Write(js.Serialize(Result).ToLower());
            }


        }

        /// <summary>
        /// این متد وظیفه آپلود یکی از بسته های  فایل مورد نظر را بر عهده دارد
        /// </summary>
        /// <param name="key">کلمه عبور برنامه ای که درحال استفاده از متد جاری می باشد</param>
        /// <param name="token">توکن فایل مورد درخواست</param>
        /// <param name="data">محتوای بسته ارسالی
        /// <remarks>
        /// محتوای این پارامتر بصورت کد بیس 64 میباشد.
        /// </remarks>
        /// </param>
        /// <returns>
        /// خروجی ساختار جی سون از کلاس زیر را دارد.
        /// <para><see cref="ClassCollection.Model.Result"/></para>
        /// <remarks>
        /// <para>Result.Code : -1 , Result.Message : <see cref="ClassCollection.Messages.OPERATION_NO_ACCESS"/></para>
        /// <para>Result.Code : 0 , Result.Message : <see cref="ClassCollection.Messages.UPLOAD_PACKET_SUCCESS"/> </para>
        /// <para>Result.Code : 1 , Result.Message : <see cref="ClassCollection.Messages.TOKEN_ERROR_NOT_EXIST"/> </para>
        /// <para>Result.Code : 2 , Result.Message : <see cref="ClassCollection.Messages.UPLOAD_PACKET_ERROR_OVERFLOW"/></para>
        /// </remarks>
        /// </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void uploadPacket(string key, string token, string data)
        {

            JavaScriptSerializer js = new JavaScriptSerializer();
            ClassCollection.Model.Result Result = new ClassCollection.Model.Result();

            var userkey = ConfigurationManager.AppSettings["UserKey"];
            if (key != userkey)
            {
                Result.code = -1;
                Result.message = ClassCollection.Message.OPERATION_NO_ACCESS;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }
            var db = new DataAccessDataContext();

            if (db.tokenUploadServiceTbls.Count(c => c.token == token) == 0)
            {
                Result.code = 1;
                Result.message = ClassCollection.Message.TOKEN_ERROR_NOT_EXIST;
  

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }
            var tokenRec = db.tokenUploadServiceTbls.Single(c => c.token == token);

            //دریافت بسته وارد شده
            if (tokenRec.receivedPacket < tokenRec.packetCount)
            {
                //data = HttpUtility.UrlDecode(data);

                var pac = new packetUploadServiceTbl();
                pac.data = data;
                pac.token = tokenRec.token;

                db.packetUploadServiceTbls.InsertOnSubmit(pac);
                tokenRec.receivedPacket = tokenRec.receivedPacket + 1;
                db.SubmitChanges();

                Result.code = 0;
                Result.message = ClassCollection.Message.UPLOAD_PACKET_SUCCESS;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;

            }
            //فایل مورد نظر قبلا به پایان رسیده است
            else
            {
                Result.code = 2;
                Result.message = ClassCollection.Message.UPLOAD_PACKET_ERROR_OVERFLOW;

                Context.Response.Write(js.Serialize(Result).ToLower());
                return;
            }

        }

    }
}
