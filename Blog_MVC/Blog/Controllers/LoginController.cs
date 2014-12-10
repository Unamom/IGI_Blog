using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{   
    //Авторизация
    public class LoginController : Controller
    {
        public BlogModels model = new BlogModels();

        public ActionResult Index()
        {
            return View("Index");
        }
        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult Login(string email, string password)
        {
            IEnumerable<User>User =
                (from user in model.Users
                 where user.Password == password && user.Email == email
                orderby user.Name descending
                select user);

            foreach (User user in User)
            {
                Session["UserID"] = user.ID;
                Session["UserName"] = user.Name+" "+user.Lastname;
                Session["UserGroup"] = user.UserGroup;
            }

            return View("Index");
        }

        /// <summary>
        ///Выход 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }
    }
}
