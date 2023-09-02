using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.ClassCollection
{
    public class Message
    {
        public static String OPERATION_NO_ACCESS = "شما دسترسی لازم به این عملیات را ندارید";
        public static String OPERATION_SUCCESS = "عملیات با موفقیت انجام شد";
        public static String USER_EXIST = "کاربر مورد نظر وجود دارد";
        public static String USER_NOT_EXIST = "کاربر درخواستی وجود ندارد";

        public static String INPUT_ERROR_PACKET_COUNT_INCORRECT = "تعداد بسته های ارسالی جهت آپلود  صحیح نیست";
        public static String INPUT_ERROR_FILE_FORMAT_INCORRECT = "نوع فایل مورد نظر جهت آپلود فایل صحیح نیست";
        public static String UPLOAD_TOKEN_SENT = "توکن با موفقیت ارسال شد";
        public static String UPLOAD_FILE_SUCCESS = "فایل مورد نظر با موفقیت آپلود شد";
        public static String UPLOAD_FILE_FAILED = "فایل مورد نظر بارگذاری نشد";

        public static String UPLOAD_PACKET_SUCCESS = "بسته مورد نظر از فایل با موفقیت آپلود شد";
        public static String UPLOAD_PACKET_ERROR_OVERFLOW = "آپلود فایل مورد نظر قبلا به پایان رسیده است";
        public static String UPLOAD_FILE_ERROR_NOT_FINISHED = "آپلود فایل مورد نظر هنوز به پایان نرسیده است";
        public static String TOKEN_ERROR_NOT_EXIST = "توکن مورد نظر وجود ندارد";
    }
}