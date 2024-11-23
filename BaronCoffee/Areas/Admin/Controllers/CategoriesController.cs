using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaronCoffee.Models;

namespace BaronCoffee.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Admin/Categories
        public ActionResult Index()
        {



            return View(db.Categories.ToList());
        }

        // GET: Admin/Categories/Details/5

        // hàm Details dùng để lấy chi tiết 1 bản ghi có CategoriesID = id
        public ActionResult Details(int? id)
        {
            if (id == null) // trường hợp không tìm thấy bản ghi
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        // có 2 hàm Create
        // GET: Admin/Categories/Create
        // hàm create đầu tiên : get dữ liệu
        // chạy form Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        //POST : dữ liệu nhập vào từ form lên DB
        // hàm create thứ hai: đem dl vào DB
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken] // loại trừ việc lưu bản ghi giống nhau 
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            // Kiểm tra nếu danh mục đã tồn tại 
            bool isCategoryExists = db.Categories.Any(c => c.CategoryName == category.CategoryName);

            if (isCategoryExists)
            {
                // Thông báo lỗi nếu danh mục đã tồn tại
                ModelState.AddModelError("CategoryName", "Danh mục này đã tồn tại.");
                return View(category);
            }


            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        // giống hàm Details // lấy dl của một id nào đó
        public ActionResult Edit(int? id)
        {
            Details(id);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Category category = db.Categories.Find(id);
            //if (category == null)
            //{
            //    return HttpNotFound();
            //}

            return View(category);
        }

        // POST: Admin/Categories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            bool isCategoryExists = db.Categories.Any(c => c.CategoryName == category.CategoryName);

            if (isCategoryExists)
            {
                // Thông báo lỗi nếu danh mục đã tồn tại
                ModelState.AddModelError("CategoryName", "Danh mục này đã tồn tại.");
                return View(category);
            }

            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
