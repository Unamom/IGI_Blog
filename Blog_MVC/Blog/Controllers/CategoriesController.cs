using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using System.Text;

namespace Blog.Controllers
{
    //Все о категориях
    public class CategoriesController : Controller
    {
        public BlogModels model = new BlogModels();

        public ActionResult Index(int? id)
        {
            IEnumerable<Category> categories =
                from category in model.Categories
                orderby category.ID
                select category;
            ViewBag.Categories = categories;
            return View(categories);
        }

        /// <summary>
        /// Обновление категорий
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Update(int? id, string name)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            Category category = GetCategory(id);
            category.Name = name;

            if (!id.HasValue)
            {
                model.AddToCategories(category);
            }
            model.SaveChanges();

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Удаление категорий
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            Category category = GetCategory(id);
            model.DeleteObject(category);
            model.SaveChanges();

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Редактирование категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null || (int)Session["UserGroup"] != 1)
            {
                return RedirectToAction("Index");
            }
            Category category = GetCategory(id);
            return View(category);
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

    }















}
