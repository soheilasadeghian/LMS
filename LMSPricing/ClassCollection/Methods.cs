using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LMSPricing.ClassCollection
{
    public class Methods
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string convertPersianNumberToEnglishNumber(string str)
        {
            char[] temp = str.ToCharArray();
            for (int i = 0; i < temp.Length; i++)
            {
                switch (Convert.ToInt32(temp[i]).ToString())
                {
                    case "1632":
                        temp[i] = '0';
                        break;
                    case "1633":
                        temp[i] = '1';
                        break;
                    case "1634":
                        temp[i] = '2';
                        break;
                    case "1635":
                        temp[i] = '3';
                        break;
                    case "1636":
                        temp[i] = '4';
                        break;
                    case "1637":
                        temp[i] = '5';
                        break;
                    case "1638":
                        temp[i] = '6';
                        break;
                    case "1639":
                        temp[i] = '7';
                        break;
                    case "1640":
                        temp[i] = '8';
                        break;
                    case "1641":
                        temp[i] = '9';
                        break;

                    case "1776":
                        temp[i] = '0';
                        break;
                    case "1777":
                        temp[i] = '1';
                        break;
                    case "1778":
                        temp[i] = '2';
                        break;
                    case "1779":
                        temp[i] = '3';
                        break;
                    case "1780":
                        temp[i] = '4';
                        break;
                    case "1781":
                        temp[i] = '5';
                        break;
                    case "1782":
                        temp[i] = '6';
                        break;
                    case "1783":
                        temp[i] = '7';
                        break;
                    case "1784":
                        temp[i] = '8';
                        break;
                    case "1785":
                        temp[i] = '9';
                        break;
                    default:

                        break;

                }
            }

            return new string(temp);


            // return value.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");
        }
        public static string toFriendlyDate(DateTime date)
        {
            var subdate = DateTime.Now - date;
            string result = "";
            if (subdate.TotalMinutes < 60)
            {
                result = Persia.Calendar.ConvertToPersian(date).ToRelativeDateString("M");
            }
            else if (subdate.TotalHours < 24)
            {
                result = Persia.Calendar.ConvertToPersian(date).ToRelativeDateString("H");
            }
            else if (subdate.Days < 7)
            {
                //result = Persia.Calendar.ConvertToPersian(date).ToRelativeDateString("TY") + " - " + Persia.Calendar.ConvertToPersian(date).ToString("H");
                result = Persia.Calendar.ConvertToPersian(date).ToRelativeDateString("TY");
            }
            else
            {
                //result = Persia.Calendar.ConvertToPersian(date).ToString("m") + " - " + Persia.Calendar.ConvertToPersian(date).ToString("H");
                result = Persia.Calendar.ConvertToPersian(date).ToString("m");
            }

            return result;
        }
        public static string toFriendlyDateF(DateTime date)
        {
            var subdate = date - DateTime.Now;
            string result = "";
            if (subdate.TotalMinutes < 60)
            {
                result = ((int)subdate.TotalMinutes) + " دقیقه دیگر";
            }
            else if (subdate.TotalHours < 24)
            {
                result = ((int)subdate.TotalHours) + " ساعت دیگر";
            }
            else
            {
                result = ((int)subdate.Days) + " روز دیگر";
            }


            return result;
        }

        public static string getfilePath()
        {
            return ConfigurationManager.AppSettings["filePath"];
        }
        public static string getfileURL()
        {
            return ConfigurationManager.AppSettings["fileURL"];
        }
        public static bool checkUserKey(string value)
        {
            return ConfigurationManager.AppSettings["UserKey"] == value;
        }
        public static string saveBase64FileToServer(string data, string path)
        {
            string fullName = null;

            try
            {
                Byte[] bytes = Convert.FromBase64String(data);
                File.WriteAllBytes(path, bytes);
                fullName = path;
            }
            catch (Exception rrr)
            {
                fullName = "errr " + rrr.Message;
            }

            return fullName;
        }
        public static string md5(string sPassword)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
       
    }
}