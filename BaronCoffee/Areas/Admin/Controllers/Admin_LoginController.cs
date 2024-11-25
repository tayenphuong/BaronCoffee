//using BaronCoffee.Models;
//using BaronCoffee.Models.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;

//namespace BaronCoffee.Areas.Admin.Controllers
//{
//    public class Admin_LoginController : Controller
//    {
//        private MyStoreEntities db = new MyStoreEntities();
//        // GET: Admin/Admin_Login
//        public ActionResult Login()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Login(LoginVM model, bool rememberMe)
//        {
//            if (ModelState.IsValid)
//            {
//                var validUser = db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password && u.UserRole == "A");
//                if (validUser != null)
//                {
//                    Session["Username"] = validUser.Username;
//                    Session["Role"] = validUser.UserRole;
//                    FormsAuthentication.SetAuthCookie(validUser.Username, rememberMe);
//                    return RedirectToAction("Index", "Admin_Home");

//                }
//                else
//                {
//                    ModelState.AddModelError("", "Tên dăng nhập hoặc mật khẩu không đúng");
//                }
//            }
//            return View(model);
//        }
//        public ActionResult Logout()
//        {
//            Session.Clear();
//            return RedirectToAction("Index", "Admin_Home");
//        }
//    }
//}