using CaptchaMvc.HtmlHelpers;
using LMSPricing.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Saham.ClassCollection;

namespace LMSPricing.Areas.admin.Controllers
{
    public class PanelController : Controller
    {
        // GET: admin/Panel
        public ActionResult Index()
        {
            if (!ClassCollection.Method.isAdminLogin(HttpContext))
            {
                return Redirect("/admin/login/ورود");
            }
            return View();
        }

        #region login

        public ActionResult Login()
        {
            if (ClassCollection.Method.isAdminLogin(HttpContext))
            {
                return Redirect("/admin/dashboard/داشبورد");
            }
            ViewBag.message = "";
            return View();
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();

            return Redirect("/admin/login/ورود");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.message = "دسترسی غیر مجاز!!!";
                return View();
            }

            if (!this.IsCaptchaValid("کد امنیتی صحیح نمی باشد!!!"))
            {
                ViewBag.message = "کد امنیتی صحیح نمی باشد!!!";
                return View();
            }
            if (ClassCollection.Method.isAdminLogin(HttpContext))
            {
                return Redirect("/admin/dashboard/داشبورد");
            }

            var db = new DataAccessDataContext();
            var dt = new DateTime();
            dt = DateTime.Now;

            if (ConfigurationManager.AppSettings["adminUserName"] != username || ConfigurationManager.AppSettings["adminPassword"] != password)
            {
                ViewBag.message = "نام کاربری یا کلمه عبور صحیح نمی‌باشد";
                return View();

            }
            var admin = new User();
            admin.username = username;
            admin.ID = 1;
            admin.name = "مدیریت";
            admin.family = "دورآموز";

            Session.Add("admin9652", admin);
            Session.Timeout = 20;
            return Redirect("/admin/dashboard/داشبورد");
        }

        #endregion

        #region users

        /// <summary>
        /// لیست کاربر هارا نمایش میدهد
        /// </summary>
        /// <returns></returns>
        public ActionResult userList()
        {
            if (!ClassCollection.Method.isAdminLogin(HttpContext))
            {
                return Redirect("/admin/login/ورود");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult userlist(int pageIndex, int pageCount)
        {
            var result = new Models.UserListResult();
            result.result = new Models.Result();

            if (!ClassCollection.Method.isAdminLogin(HttpContext))
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }

            if (!ModelState.IsValid)
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }

            int skipCount = (pageIndex - 1) * pageCount;
            var db = new DataAccessDataContext();
            

            var users = db.userTbls;
            
            var co = users.Count();
            var count = (co % pageCount == 0) ? (int)(co / pageCount) : ((int)(co / pageCount) + 1);
            var acList = users.Skip(skipCount).Take(pageCount).ToList();

            result.user = new List<Models.User>();
            result.count = count;

            foreach (var item in acList)
            {
                var t = new Models.User();
                t.ID = item.ID;
                t.fullname = item.fullName;
                t.mobile = item.mobile;
                t.image = LMSPricing.ClassCollection.Methods.getfileURL() + item.image;
                t.regrDate = Persia.Calendar.ConvertToPersian(item.regDate).ToString("d");
                result.user.Add(t);
            }
            
            result.result.code = 0;
            result.result.message = "عملیات با موفقیت انجام شد";

            return Json(result);
        }
        
        /// <summary>
        /// ارسال کاربر
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult getUser(long userID)
        {
            var result = new Models.UserResult();
            result.result = new Models.Result();
            result.user = new Models.User();
            if (!ClassCollection.Method.isAdminLogin(HttpContext))
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }
            if (!ModelState.IsValid)
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }

            var db = new DataAccessDataContext();
            if (!db.userTbls.Any(c => c.ID == userID))
            {
                result.result.code = 1;
                result.result.message = "کاربر وجود ندارد";
                return Json(result);
            }

            var u = db.userTbls.Single(c => c.ID == userID);

            result.user.fullname = u.fullName;
            result.user.regrDate = Persia.Calendar.ConvertToPersian(u.regDate).ToString("d");
            result.user.ID = u.ID;
            result.user.image = LMSPricing.ClassCollection.Methods.getfileURL() + u.image;
            result.user.mobile = u.mobile;
            result.result.code = 0;
            result.result.message = "عملیات با موفقیت انجام شد";

            return Json(result);


        }


        #endregion

        #region Cources

        /// <summary>
        /// لیست دوره هارا نمایش میدهد
        /// </summary>
        /// <returns></returns>
        public ActionResult courseList()
        {
            if (!ClassCollection.Method.isAdminLogin(HttpContext))
            {
                return Redirect("/admin/login/ورود");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult courselist(string filter, string course, string orderby, string ordertype, int pageIndex, int pageCount)
        {
            course = LMSPricing.ClassCollection.Methods.convertPersianNumberToEnglishNumber(course);
            orderby = LMSPricing.ClassCollection.Methods.convertPersianNumberToEnglishNumber(orderby);
            ordertype = LMSPricing.ClassCollection.Methods.convertPersianNumberToEnglishNumber(ordertype);

            var result = new Models.CourseUserListResult();
            result.result = new Models.Result();

            if (!ClassCollection.Method.isAdminLogin(HttpContext))
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }

            if (!ModelState.IsValid)
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }

            var db = new DataAccessDataContext();
            var courseusers = db.userReserveTbls.Where(c => c.regDate != null );

            #region search
            
            switch (course)
            {
                case "0":
                    break;
                case "1":
                    courseusers = courseusers.Where(c => c.courseID == 1);
                    break;
                case "2":
                    courseusers = courseusers.Where(c => c.courseID == 2);
                    break;
                case "3":
                    courseusers = courseusers.Where(c => c.courseID == 3);
                    break;
                case "4":
                    courseusers = courseusers.Where(c => c.courseID == 4);
                    break;
                default:
                    break;
            }

            if (filter != "")
            {
                var search = new Search(filter);
                var ors = search.getOR;

                var predicate = PredicateBuilder.False<userReserveTbl>();
                foreach (string keyword in ors)
                {
                    string temp = keyword;
                    predicate = predicate.Or(p => p.userTbl.fullName.Contains(keyword)
                    );
                }

                courseusers = courseusers.Where(predicate);
            }

            if (ordertype == "0")
            {
                switch (orderby)
                {
                    case "5":
                        courseusers = courseusers.OrderBy(c => c.regDate);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (orderby)
                {
                    case "5":
                        courseusers = courseusers.OrderByDescending(c => c.regDate);
                        break;

                    default:
                        break;
                }
            }

            courseusers = courseusers.OrderBy(c => c.isRead == true);
            #endregion


            int skipCount = (pageIndex - 1) * pageCount;

            var co = courseusers.Count();
            var count = (co % pageCount == 0) ? (int)(co / pageCount) : ((int)(co / pageCount) + 1);
            var acList = courseusers.Skip(skipCount).Take(pageCount).ToList();

            result.CourseUser = new List<Models.CourseUser>();
            result.count = count;

            foreach (var item in acList)
            {
                var t = new Models.CourseUser();

                var courseID = item.courseID;
                switch (courseID)
                {
                    case 1:
                        t.coursename = "آشپزی";
                        break;
                    case 2:
                        t.coursename = "شیرینی پزی";
                        break;
                    case 3:
                        t.coursename = "پته دوزی";
                        break;
                    case 4:
                        t.coursename = "جاوا";
                        break;
                    default:
                        t.coursename = "دوره بدون نام";
                        break;
                }

                t.ID = item.ID;
                t.courseID = item.courseID;
                t.userID = item.userID;
                t.fullname = item.userTbl.fullName;
                t.isread = item.isRead;
                t.regDate = Persia.Calendar.ConvertToPersian(item.regDate).ToString("d");
                result.CourseUser.Add(t);
            }

            result.result.code = 0;
            result.result.message = "عملیات با موفقیت انجام شد";

            return Json(result);
        }


        /// <summary>
        /// ارسال کاربر
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult getCourse(long courseID)
        {
            var result = new Models.CourseUserResult();
            result.result = new Models.Result();
            result.CourseUser = new Models.CourseUser();
            if (!ClassCollection.Method.isAdminLogin(HttpContext))
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }
            if (!ModelState.IsValid)
            {
                result.result.code = -1;
                result.result.message = "دسترسی شما غیرمجاز است";
                return Json(result);
            }

            var db = new DataAccessDataContext();
            if (!db.userReserveTbls.Any(c => c.ID == courseID))
            {
                result.result.code = 1;
                result.result.message = "دوره رزرو شده وجود ندارد";
                return Json(result);
            }

            var u = db.userReserveTbls.Single(c => c.ID == courseID);

            switch (u.courseID)
            {
                case 1:
                    result.CourseUser.coursename = "آشپزی";
                    break;
                case 2:
                    result.CourseUser.coursename = "شیرینی پزی";
                    break;
                case 3:
                    result.CourseUser.coursename = "پته دوزی";
                    break;
                case 4:
                    result.CourseUser.coursename = "جاوا";
                    break;
                default:
                    break;
            }

            result.CourseUser.fullname = u.userTbl.fullName;
            result.CourseUser.regDate = Persia.Calendar.ConvertToPersian(u.regDate).ToString("d");
            result.CourseUser.mobile = u.userTbl.mobile;
            result.CourseUser.ID = u.ID;
            result.CourseUser.isread = u.isRead;
            result.CourseUser.userimage = LMSPricing.ClassCollection.Methods.getfileURL() + u.userTbl.image;
            result.result.code = 0;
            result.result.message = "عملیات با موفقیت انجام شد";

            u.isRead = true;
            db.SubmitChanges();


            return Json(result);


        }
        #endregion
    }
}