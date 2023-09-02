using System.Web.Mvc;

namespace LMSPricing.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admindashboard",
                "admin/dashboard/{id}",
                new { action = "Index", controller = "panel", AreaName = "admin", id = UrlParameter.Optional }
            );

            #region user

            context.MapRoute(
                          "userList",
                          "admin/user/list/{title}",
                          new { action = "userList", controller = "panel", AreaName = "admin", title = UrlParameter.Optional }
                        );

            #endregion

            #region Login

            context.MapRoute(
               "adminLogin",
               "admin/login/{title}",
               new { action = "Login", controller = "panel", AreaName = "admin", title = UrlParameter.Optional }
             );

            context.MapRoute(
               "adminLogout",
               "admin/Logout/{title}",
               new { action = "Logout", controller = "panel", AreaName = "admin", title = UrlParameter.Optional }
             );

            #endregion

            #region course

            context.MapRoute(
                          "CourseUser",
                          "admin/course/list/{title}",
                          new { action = "courselist", controller = "panel", AreaName = "admin", title = UrlParameter.Optional }
                        );

            #endregion

            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new {controller="Panel" ,action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}