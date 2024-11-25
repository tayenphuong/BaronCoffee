﻿using BaronCoffee.Models.ViewModel;
using BaronCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Data.Entity;

namespace BaronCoffee.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var existUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "C"
                };
                db.Users.Add(user);
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerEmail = model.CustomerEmail,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                Session["Username"] = user.Username;
                Session["Role"] = user.UserRole;
                RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                var validUser = db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password && u.UserRole == "C");
                if (validUser != null)
                {
                    Session["Username"] = validUser.Username;
                    Session["Role"] = validUser.UserRole;
                    FormsAuthentication.SetAuthCookie(validUser.Username, rememberMe);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Tên dăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult ProfileInfo()
        {
            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterVM()
            {
                Username = user.Username,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                CustomerEmail = customer.CustomerEmail,
                CustomerAddress = customer.CustomerAddress,
            };
            ViewBag.Message = TempData["Message"];
            return View(model);
        }
        public ActionResult UpdateProfile()
        {
            var username = User.Identity.Name;
            var customer = db.Customers.SingleOrDefault(c => c.Username == username);

            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new Customer
            {
                Username = customer.Username,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                CustomerEmail = customer.CustomerEmail,
                CustomerAddress = customer.CustomerAddress
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(Customer model)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.SingleOrDefault(c => c.Username == model.Username);

                if (customer == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                customer.CustomerName = model.CustomerName;
                customer.CustomerPhone = model.CustomerPhone;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CustomerAddress = model.CustomerAddress;

                db.SaveChanges();

                TempData["Message"] = "Cập nhật thông tin thành công!";
                return RedirectToAction("ProfileInfo");
            }
            return View(model);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (user.Password != model.CurrentPassword)
                {
                    ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
                }

                user.Password = model.NewPassword;
                db.SaveChanges();

                ViewBag.Message = "Đổi mật khẩu thành công!";
            }
            return View(model);
        }
    }
}