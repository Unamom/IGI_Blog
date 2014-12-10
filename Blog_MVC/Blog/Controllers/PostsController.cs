using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using System.Text;

namespace Blog.Controllers
{
    //Статьи
    public class PostsController : Controller
    {
        public BlogModels model = new BlogModels();
        private const int PostsPerPage = 4;
        
        /// <summary>
        /// Вывод статей
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            int pageNumber = id ?? 0;
            IEnumerable<Post> posts =
                (from post in model.Posts
                 where post.DateTime < DateTime.Now
                 orderby post.DateTime descending
                 select post).Skip(pageNumber * PostsPerPage).Take(PostsPerPage + 1);
            ViewBag.IsPreviousLinkVisible = pageNumber > 0;
            ViewBag.IsNextLinkVisible = posts.Count() > PostsPerPage;
            ViewBag.PageNumber = pageNumber;
           // ViewBag.IsAdmin = IsAdmin;

            //Session["UserID"] = null;
            //Session["UserName"] = null;
            //Session["UserGroup"] = null;

            //автор
            IEnumerable<User> Author =
                from author in model.Users
                orderby author.ID
                select author;
            ViewBag.Author = Author;


            //ссылки на категории
            string cats = string.Empty;
            IEnumerable<Category> categories =
                from category in model.Categories
                orderby category.ID
                select category;

            foreach (Category category in categories)
            {
                cats += "<li><a href=\"Posts/Categories/"+category.ID+"\">" + category.Name + "</a></li>";
            }

            ViewBag.Categories = cats;

            ViewBag.CategoriesClean = categories;

           return View(posts.Take(PostsPerPage));
        }

        /// <summary>
        /// Вся статья
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Post post = GetPost(id);
            //ViewBag.IsAdmin = IsAdmin;

            IEnumerable<Category> categories =
                from category in model.Categories
                orderby category.ID
                select category;

            //автор
            IEnumerable<User> Author =
                from author in model.Users
                orderby author.ID
                select author;
            ViewBag.Author = Author;

            ViewBag.CategoriesClean = categories;

            return View(post);
        }

        /// <summary>
        /// Оставление комента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Comment(int id, string body)
        {
            Post post = GetPost(id);
            Comment comment = new Comment();
            comment.Post = post;
            comment.DateTime = DateTime.Now;
            comment.Body = body;
            comment.AuthorID = (int)Session["UserID"];
            model.AddToComments(comment);
            model.SaveChanges();
            return RedirectToAction("Details", new { id = id });
        }

        /// <summary>
        /// Отоброжение статей по тегу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Tags(string id)
        {
            Tag tag = GetTag(id);
           // ViewBag.IsAdmin = IsAdmin;

            IEnumerable<Category> categories =
                from category in model.Categories
                orderby category.ID
                select category;

            //автор
            IEnumerable<User> Author =
                from author in model.Users
                orderby author.ID
                select author;
            ViewBag.Author = Author;

            ViewBag.CategoriesClean = categories;


            return View("Index", tag.Posts);
        }

        /// <summary>
        /// Отображение статей в категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Categories(int? id)
        {
            Category cat = GetCategory(id);
            IEnumerable<Post> posts =
            from post in model.Posts
             where post.CategoryID == cat.ID
             orderby post.DateTime descending
             select post;

            IEnumerable<Category> categories =
            from category in model.Categories
            orderby category.ID
            select category;

            //автор
            IEnumerable<User> Author =
                from author in model.Users
                orderby author.ID
                select author;
            ViewBag.Author = Author;

            ViewBag.CategoriesClean = categories;

            return View(posts);
        }

        /// <summary>
        /// Обновление статьи в базе данных
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="category"></param>
        /// <param name="body"></param>
        /// <param name="shortBody"></param>
        /// <param name="dateTime"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Update(int? id, string title, int category, string body, string shortBody, DateTime dateTime, string tags)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }

            Post post = GetPost(id);
            post.Title = title;
            post.Body = body;
            post.DateTime = dateTime;
            post.ShortBody = shortBody;
            post.CategoryID = category;

            post.AuthorID = (int)Session["UserID"];

            post.Tags.Clear();

            tags = tags ?? string.Empty;
            string[] tagNames = tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string tagName in tagNames)
            {
                post.Tags.Add(GetTag(tagName));
            }

            if (!id.HasValue)
            {
                model.AddToPosts(post);
            }
            model.SaveChanges();

            return RedirectToAction("Details", new { id = post.ID });
        }

        /// <summary>
        /// Удаление статьи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
                Post post = GetPost(id);
                model.DeleteObject(post);
                model.SaveChanges();
 
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Удаление уоментов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteComment(int id)
        {
                Comment comment = model.Comments.Where(x => x.ID == id).First();
                model.DeleteObject(comment);
                model.SaveChanges();
                return RedirectToAction("/Details/" + comment.PostID);
        }

        /// <summary>
        /// Редактирование постов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            Post post = GetPost(id);
            StringBuilder tagList = new StringBuilder();

            foreach (Tag tag in post.Tags)
            {
                tagList.AppendFormat("{0}", tag.Name);
            }

            IEnumerable<Category> categories =
                from category in model.Categories
                orderby category.ID
                select category;

            ViewBag.Categories = categories;
            ViewBag.Tags = tagList.ToString();
            return View(post);
        }

        /// <summary>
        /// Получение тега
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private Tag GetTag(string tagName)
        {
            return model.Tags.Where(x => x.Name == tagName).FirstOrDefault() ?? new Tag() { Name = tagName };
        }

        /// <summary>
        /// Получение тега
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Post GetPost(int? id)
        {
            return id.HasValue ? model.Posts.Where(x => x.ID == id).First() : new Post() { ID = -1 };
        }


        /// <summary>
        /// Получение категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Category GetCategory(int? id)
        {
            return id.HasValue ? model.Categories.Where(x => x.ID == id).First() : new Category() { ID = -1 };
        }

        /*public bool IsAdmin { get { return Session["IsAdmin"] != null && (bool)Session["IsAdmin"]; } }*/

        //public bool IsAdmin { get { return true; } }
    }
}
