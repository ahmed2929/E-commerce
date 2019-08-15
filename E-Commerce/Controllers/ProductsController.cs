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
    public class ProductsController : Controller
    {
        static List<tempProduct> products = new List<tempProduct>();



        private E_CommerceContext db = new E_CommerceContext();

        // GET: Products
        public ActionResult clearTempList()
        {
            if(Session["email"] == null)
            {
                return RedirectToAction("WelcomeHome", "user");

            }

            products.Clear();
            return RedirectToAction("Login", "User");
        }

        public ActionResult Index()
        {
            if (Session["email"] == null)
            {

            }

            return View(db.Products.ToList());
        }


        public ActionResult logoutDelete()
        {
            products.Clear();
            return RedirectToAction("Login", "user");

           
        }



        public ActionResult Shop()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("WelcomeHome", "user");

            }

            return View(db.Products.ToList());
        }
      
         public ActionResult CartSet(int id,string name,decimal price,int quantaty,string description)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("WelcomeHome", "user");

            }

            tempProduct  p = new tempProduct() { ID = id, name = name,price=price,quantaty=quantaty };
            if (p.Pcounter != p.quantaty)
            {
                p.Pcounter += 1;
            }
           

            tempProduct pFound = products.SingleOrDefault(pp => pp.ID == id);
            if (pFound == null)
            {
                p.totalPrice = price;
                products.Add(p);
            }
            else
            {
                return RedirectToAction($"addAnotherToCart/{id}");
            }
            return RedirectToAction("cart");
          
        }



        public ActionResult DeleteFromCart(int id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("WelcomeHome", "user");

            }

            products.RemoveAll(s => s.ID == id);
            return RedirectToAction("cart");
        }
        public ActionResult addAnotherToCart(int id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("WelcomeHome", "user");


            }

            tempProduct p = products.SingleOrDefault(pp => pp.ID == id);
            if (p.Pcounter != p.quantaty)
            {
                p.totalPrice = ++p.Pcounter * p.price;
            }
            return RedirectToAction("cart");



        }
        public ActionResult AdminIndex()
        {
            if (Session["email"].ToString() != "admin@admin.com")
            {
                return RedirectToAction("WelcomeHome", "user");

            }
            //if(!="hamoo@yahoo.com")
            //{
            //    return RedirectToAction("UserIndex","Product");
            //}
            return View(db.Products.ToList());
        }

        public ActionResult ConfirmPay(int id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("WelcomeHome", "user");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id); //DB product

            tempProduct p = products.SingleOrDefault(pp => pp.ID == id); // temp DB


            if (product == null)
            {
                return HttpNotFound();
            }

            product.Quantity = product.Quantity - p.Pcounter;

            db.SaveChanges();

            tempProduct PDelete = products.SingleOrDefault(pp => pp.ID == id);
            products.Remove(PDelete);
            

            return RedirectToAction("shop");

        }

        public ActionResult Cart()
        {
            if (Session["email"] == null|| Session["email"].ToString() == "admin@admin.com")
            {
                return RedirectToAction("WelcomeHome", "user");


            }

            return View(products);
           
        }






        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["email"].ToString() != "admin@admin.com")
            {
                 return RedirectToAction("shop", "products");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["email"].ToString() != "admin@admin.com")
            {
                return RedirectToAction("shop", "products");


            }

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Quantity,Price,ImgURL,description")] Product product)
        {
            if (Session["email"].ToString() != "admin@admin.com")
            {
                return RedirectToAction("shop", "products");

            }

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["email"].ToString() != "admin@admin.com")
            {
                return RedirectToAction("shop", "products");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Quantity,Price,ImgURL,description")] Product product)
        {
            if (Session["email"].ToString() != "admin@admin.com")
            {
                return RedirectToAction("shop", "products");

            }

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["email"].ToString() != "admin@admin.com")
            {
                return RedirectToAction("shop", "products");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["email"].ToString() != "admin@admin.com")
            {
                return RedirectToAction("shop", "products");

            }

            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
