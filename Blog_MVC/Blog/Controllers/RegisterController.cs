using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    //Регистрация
    public class RegisterController : Controller
    {
        public BlogModels model = new BlogModels();

        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// Юзера в бд
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastname"></param>
        /// <param name="sex"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Register(string name, string lastname, string sex, string email, string password)
        {
            User user = new User();
            user.Name = name;
            user.Lastname = lastname;
            user.Age = ""; //потом убрать
            user.Sex = sex;
            user.Email = email;
            user.Password = password;

            model.AddToUsers(user);

            model.SaveChanges();

            Session["UserID"] = user.ID;
            Session["UserName"] = user.Name + " " + user.Lastname;
            Session["UserGroup"] = user.UserGroup;

            return RedirectToAction("Index");
        }

    }
}
