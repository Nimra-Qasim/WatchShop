using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watch_Shop.Models;
using System.Data.Entity;
using System.Data;

namespace Watch_Shop.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository i;
        // GET: /Account/
        My_DBEntities5 ed = new My_DBEntities5();
        public AccountController(IAccountRepository iA)
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
                return View("../Admin/Error");
            }
          
        }

        public ActionResult Login()
        {
            
            return View();

        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUpUser(Account a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    for (int j = 0; j < Request.Files.Count; j++)
                    {
                        HttpPostedFileBase file = Request.Files[j];
                        file.SaveAs(Server.MapPath(@"~/Files/" + file.FileName));
                        a.Image = @"/Files/" + file.FileName;
                    }

                    //ed.Accounts.Add(a);
                    //ed.SaveChanges();

                    i.signUp(a);
                    ViewBag.msg = "You are successfully SIGNED UP !";


                }
                catch (Exception e)
                {
                }
                return View("Login");
            }
            // return View("SignUp");
            else
            {
                ViewBag.msg = "Soory! Try Again";
                return View("../Home/Index");
            }
           
        }
        [HttpPost]
        public ActionResult LoginUser()
        {
               string AName = "Admin";
                string APass = "Admin";
                string username = Request["Username"];
                string password = Request["Password"];
                string image = Request["Image"];
               Account temp = ed.Accounts.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));

                if (username.Equals(AName) && password.Equals(APass))
                {
                    Session["Aname"] = AName;

                    return View("../Admin/Index");
                }
                else if (temp == null)
                {
                    ViewBag.abc = "Wrong Username/Password Try again!!";
                    return View("Login");

                }
                else
                {
                    List<Catagory> w = ed.Catagories.ToList();

                    Session["name"] = temp.Username;
                    Session["firstName"] = temp.FirstName;
                    Session["lastName"] = temp.LastName;
                    Session["password"] = temp.Password;
                    Session["gender"] = temp.Gender;
                    Session["email"] = temp.Email;
                    Session["image"] = temp.Image;
                    Session["id"] = temp.Id;
                    return View("../Home/newIndex", w);
                }
           
      
        }

        public ActionResult Logout()
        {
            Session["name"] = null;
            Session["Aname"] = null;
            return View("../Home/Index" , ed.Catagories.ToList());
        }

        public ActionResult Profile()
        {
            int Id = (int)Session["Id"];
            Account a = new Account();
            a = ed.Accounts.First(x => x.Id == Id);
            
            return View("Profile",a);
        }
       
        public ActionResult Edit(int id)
        {
            Account a = ed.Accounts.First(x => x.Id == id);
           
            return View("Edit",a);
        }
       [HttpPost]
        public ActionResult EditProfile(Account a)
        {
            try
            {
                for (int j = 0; j < Request.Files.Count; j++)
                {
                    HttpPostedFileBase file = Request.Files[j];
                    file.SaveAs(Server.MapPath(@"~/Files/" + file.FileName));
                    a.Image = @"/Files/" + file.FileName;
                }


                ed.Entry(a).State = System.Data.EntityState.Modified;
                ed.SaveChanges();
               
            }
            catch(Exception e)
            { }
            return View("Profile", a);
        }

       public JsonResult CheckUserName(string Username)
       {

           Account ac  = ed.Accounts.FirstOrDefault(x => x.Username.Equals(Username));
           if (ac == null)
           {

               return this.Json(true, JsonRequestBehavior.AllowGet);
           }
           else
           {

               return this.Json(false, JsonRequestBehavior.AllowGet);
           }
       }
        
        public ActionResult AddToCart(int id)
       {
           Watch w = ed.Watches.First(X => X.Id == id);

           return View(w);
       }


        [HttpPost]
        public ActionResult Cart(Watch w)
        {
            string quantity = Request["Quantity"];

            Order o = new Order();
            o.Model = w.Model;
            o.Brand = w.Catagory;
            o.Quantity = int.Parse(quantity);

            try
            {
                ed.Orders.Add(o);
                ed.SaveChanges();
            }
            catch(Exception e)
            { }
            return View("../Home/Index" , ed.Catagories.ToList());
        }
    }
    }
