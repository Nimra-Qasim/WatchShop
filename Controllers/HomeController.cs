using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Watch_Shop.Models;

namespace Watch_Shop.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        My_DBEntities5 ed = new My_DBEntities5();
        
        public ActionResult newIndex()
        {
            return View(ed.Catagories.ToList());
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult singlePage(string brand)
        {
            List<Watch> list = ed.Watches.Where(x => x.Catagory.Contains(brand)).Select(y => y).ToList();
            return View(list);
        }

        //public ActionResult singlePage(string catagory)
        //{

        //    return View();
        //}
        public ActionResult Search(String Catagory)
        {
            List<Watch> list = ed.Watches.Where(x => x.Catagory.Contains(Catagory) ||  x.Model.Contains(Catagory)).Select(y => y).ToList();
            return View(list);

        }

        public ActionResult Buy(int id)
        {
            Watch w = ed.Watches.First(it => it.Id == id);
                return View(w);
            
           
        }
        
        public ActionResult Messages(Message m)
        {

            try
            {
                ed.Messages.Add(m);
                ed.SaveChanges();
            }
            catch (Exception e) { }
            return View("Index", ed.Catagories.ToList());
        }
    }
}
