﻿using Lab24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab24.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            NorthwindEntities database = new NorthwindEntities();
            List<Customer> customerList = database.Customers.ToList();
            return View(customerList);
        }

        // GET: Customers/List/5
        public ActionResult List(string id)
        {
            NorthwindEntities database = new NorthwindEntities();
            Customer singleCustomer = database.Customers.Find(id);
            return View(singleCustomer);
        }

        // GET: Customers/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Customers/Add
        [HttpPost]
        public ActionResult Add(FormCollection collection)
        {
            try
            {
                NorthwindEntities databaseTwo = new NorthwindEntities();
                Customer newCustomer = new Customer();

                newCustomer.CustomerID = collection["CustomerID"];
                newCustomer.CompanyName = collection["CompanyName"];
                newCustomer.ContactName = collection["ContactName"];
                newCustomer.ContactTitle = collection["ContactTitle"];
                newCustomer.Address = collection["Address"];
                newCustomer.City = collection["City"];
                newCustomer.Region = collection["Region"];
                newCustomer.PostalCode = collection["PostalCode"];
                newCustomer.Country = collection["Country"];
                newCustomer.Phone = collection["Phone"];
                newCustomer.Fax = collection["Fax"];
                databaseTwo.Customers.Add(newCustomer);
                databaseTwo.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException;
                return View();
            }
        }

        // GET: Customers/Modify/5
        public ActionResult Modify(string id)
        {

            NorthwindEntities db = new NorthwindEntities();
            return View(db.Customers.Find(id));
         
        }

        // POST: Customers/Modify/5
        [HttpPost]
        public ActionResult Modify(string id, FormCollection collection)
        {
            try
            {
                NorthwindEntities db = new NorthwindEntities();
                var foundCustomer = db.Customers.Find(id);

                foundCustomer.CustomerID = collection["CustomerID"];
                foundCustomer.CompanyName = collection["CompanyName"];
                foundCustomer.ContactName = collection["ContactName"];
                foundCustomer.ContactTitle = collection["ContactTitle"];
                foundCustomer.Address = collection["Address"];
                foundCustomer.City = collection["City"];
                foundCustomer.Region = collection["Region"];
                foundCustomer.PostalCode = collection["PostalCode"];
                foundCustomer.Country = collection["Country"];
                foundCustomer.Phone = collection["Phone"];
                foundCustomer.Fax = collection["Fax"];
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            NorthwindEntities db = new NorthwindEntities();
            return View(db.Customers.Find(id));
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                NorthwindEntities db = new NorthwindEntities();              
                var foundCustomer = db.Customers.Find(id);
                
                
                var matchingOrders = db.Orders.Where(x => x.CustomerID == foundCustomer.CustomerID);
                if (matchingOrders.ToList().Count > 0)
                { TempData["Error"] = "Invalid request. This customer still has orders that need to be processed."; return View(); }
                else
                {
                    db.Customers.Remove(foundCustomer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.InnerException;
                return View();
            }
        }
    }
}
