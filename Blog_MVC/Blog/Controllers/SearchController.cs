using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using System.Text;

namespace Blog.Controllers
{
    //Логика поиска в бд
    public class SearchController : Controller
    {
        public BlogModels model = new BlogModels();
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// Поиск статей в бд
        /// </summary>
        /// <param name="query"></param>
        /// <param name="searchby"></param>
        /// <returns></returns>
        public ActionResult Search(string query, int searchby)
        {

            IEnumerable<Post> posts =
                from post in model.Posts
                where ((searchby == 1) ? post.Title.Contains(query) : post.Body.Contains(query))
                 orderby post.DateTime descending
                 select post;
            return View(posts);
        }

    }
}
