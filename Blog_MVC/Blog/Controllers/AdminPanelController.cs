using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using System.Text;

namespace Blog.Controllers
{
    //Админ
    public class AdminPanelController : Controller
    {
        public BlogModels model = new BlogModels();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Редактирование статей
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ManagePosts(int? id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            IEnumerable<Post> posts =
                from post in model.Posts
                orderby post.ID
                select post;
            ViewBag.Posts = posts;
            return View(posts);
        }
        /// <summary>
        /// Управление пользователями
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ManageUsers(int? id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            IEnumerable<User> users =
                from user in model.Users
                orderby user.ID
                select user;
            ViewBag.Users = users;
            return View(users);
        }
        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditUser(int? id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            User user = GetUser(id);
            return View(user);
        }
        /// <summary>
        /// Удаление статей
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeletePost(int id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            Post post = GetPost(id);
            model.DeleteObject(post);
            model.SaveChanges();
            return RedirectToAction("ManagePosts");
        }
        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="lastname"></param>
        /// <param name="age"></param>
        /// <param name="sex"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="usergroup"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UpdateUser(int? id, string name, string lastname, string age, string sex, string email, string password, int usergroup)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            User user = GetUser(id);
            user.Name = name;
            user.Lastname = lastname;
            user.Age = age;
            user.Sex = sex;
            user.Email = email;
            user.Password = password;
            user.UserGroup = usergroup;

            if (!id.HasValue)
            {
                model.AddToUsers(user);
            }
            model.SaveChanges();

            return RedirectToAction("ManageUsers");
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(int id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            User user = GetUser(id);
            model.DeleteObject(user);
            model.SaveChanges();

            return RedirectToAction("ManageUsers");
        }
        /// <summary>
        /// Получаем статью
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Post GetPost(int? id)
        {
            return id.HasValue ? model.Posts.Where(x => x.ID == id).First() : new Post() { ID = -1 };
        }
        /// <summary>
        /// Получаем пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private User GetUser(int? id)
        {
            return id.HasValue ? model.Users.Where(x => x.ID == id).First() : new User() { ID = -1 };
        }

    }
}
