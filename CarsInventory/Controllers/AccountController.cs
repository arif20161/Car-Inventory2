using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CI.Data;
using CI.Business.Models;

namespace CarsInventory.Controllers
{
    public class AccountController : Controller
    {

        Data objData = new Data();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateAccount()
            {
                return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(User user)
        {
           String result = "";
            User _user = new User();
            _user.UserId = Guid.NewGuid();
            _user.Email = user.Email;
            _user.Password = user.Password;
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.IsDeleted = false;
            _user.DateCreated = DateTime.Now;

         result = objData.SaveUser(_user);
         if (result == "Success")
         {
             TempData["message"] = "Congratulations! your account has been created successfully."; ;

            return  RedirectToAction("Login");
         }
         else
         {
             ViewData["message"] = "Invalid values! Please try again.";
             return View();
         }
            
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var _user = objData.GetUser(user.Email, user.Password);
            try {

                if (_user != null)
                {
                    if (!string.IsNullOrEmpty(_user.UserId.ToString()))
                    {
                        Session["UserID"] = _user.UserId.ToString();
                        Session["UserName"] = _user.FirstName.ToString();
                        ViewData["message"] = "Login Success.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["message"] = "Invalid Login or password.";
                        return View();
                    }
                }
                else {
                    ViewData["message"] = "Invalid Login or password.";
                    return View();
                }
           
            }
            catch(Exception ex)
            {
                ViewData["message"] = "Invalid Login or password. ";
                return View();
                var lineNumber = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                objData.LogFile("Login(User user):" + lineNumber + " " + ex.Message + " " + ex.InnerException);

            }
          
            
        }



        public ActionResult Logout()
        {

            Session["UserID"] = null;

            return RedirectToAction("Login", "Account");
        }
    }
}