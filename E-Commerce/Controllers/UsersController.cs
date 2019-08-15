using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;

namespace E_Commerce.Controllers
{
    public class UserController : Controller
    {
        private E_CommerceContext db = new E_CommerceContext();

        // GET: User

            public ActionResult WelcomeHome()
             {
            return View();
                
             }

        public ActionResult UserIndex()
        {
            if (Session["email"] == null)
                return RedirectToAction("login");

            string email = Session["email"].ToString();
            User u = db.Users.SingleOrDefault(user => user.Email == email);
            return View(u);
        }
        public ActionResult Login()
        {
            if (Session["Email"] != null)
            {

                return RedirectToAction("Shop", "products");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            User u = db.Users.SingleOrDefault(user => user.Email == Email && user.Password == Password);
            if (u == null)
            {
                return View();
            }
            Session["Email"] = Email;
            Session["ID"] = u.ID;
            Session["Name"] = u.Name;
            if (Email == "admin@admin.com")
                return RedirectToAction("AdminIndex", "Products");
            else
                return RedirectToAction("Shop", "Products");
        }
        public ActionResult LogOut()
        {
            Session.Clear();


            return RedirectToAction("logoutDelete", "products");
        }
        public ActionResult Home()
        {

            return View();
        }
        //GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Register()
        {
            if (Session["Email"] != null)
            {
                return RedirectToAction("Shop", "Products");
            }
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,Name,Email,Password,CPassword")] User users)
        {
            if (ModelState.IsValid)
            {
               
                var u = db.Users.SingleOrDefault(user => user.Email == users.Email);

                if (u==null)
                {

                    db.Users.Add(users);
                    db.SaveChanges();
                    Session["Email"] = users.Email;
                    return RedirectToAction("Shop", "Products");
                }
                else
                {
                    return View(users);
                }
            }

            return View(users);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Password,CPassword")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }
            return View(user);
        }

        // GET: User/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        // POST: User/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("UserIndex");
        //}

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
