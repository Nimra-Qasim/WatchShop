using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Watch_Shop.Models;

namespace Watch_Shop.Controllers
{
    public class AdminController : Controller
    {
         IAdminRepository i;
        // GET: /Account/
        My_DBEntities5 ed = new My_DBEntities5();
        public AdminController (IAdminRepository iA)
        {
            i = iA;
        }

        public ActionResult Index()
        {
            if (Session["Aname"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult ViewWatches()
        {
            return View(ed.Watches.ToList());
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddWatch(Watch w)
        {
            for (int j = 0; j < Request.Files.Count; j++)
            {
                HttpPostedFileBase file = Request.Files[j];
                file.SaveAs(Server.MapPath(@"~/images/" +file.FileName));
                w.Image = @"/images/" + file.FileName;
            }

            i.addWatch(w);
            //ed.Watches.Add(w);
            //ed.SaveChanges();
            return View("Index");
            
        }

      
        public ActionResult Delete()
        {
           
            return View(ed.Watches.ToList());
         
        }
        public ActionResult DeleteWatch(int id)
        {
            //try
            //{
            //    Watch w = ed.Watches.First(it => it.Id == id);
            //    ed.Watches.Remove(w);
            //    ed.SaveChanges();
            //}
            //catch(Exception e){}
            i.deleteWatch(id);
            return RedirectToAction("Delete");
            
        }
         public ActionResult ViewUser()
        {
            return View(ed.Accounts.ToList());
        }

         public ActionResult DeleteUser(int id)
         {
             //try
             //{
             //    Account a = ed.Accounts.First(it => it.Id == id);
             //    ed.Accounts.Remove(a);
             //    ed.SaveChanges();
             //}
             //catch (Exception e) { }
             i.deleteUser(id);
             return RedirectToAction("ViewUser");

         }

       

        public ActionResult AddBrand()
         {
            return View();
         }

        public ActionResult AddBrands(Catagory c)
        {
            for (int j = 0; j < Request.Files.Count; j++)
            {
                HttpPostedFileBase file = Request.Files[j];
                file.SaveAs(Server.MapPath(@"~/images/" +file.FileName));
                c.Image = @"/images/" + file.FileName;
            }

            i.addBrand(c);
            //ed.Watches.Add(w);
            //ed.SaveChanges();
            return View("Index");
            
        
        }

        public ActionResult ViewMessage()
        {
            return View(ed.Messages.ToList());
        }

        public ActionResult ViewOrder()
        {
            return View(ed.Orders.ToList());
        }

        public ActionResult DeleteMessage(int id)
        {
            i.deleteMsg(id);
            return RedirectToAction("ViewMessage");

        }
          public ActionResult DeleteOrder(int id)
        {
            i.deleteOrder(id);
            return RedirectToAction("ViewOrder");

        }

        
    }
}
