using CI.Business.Models;
using CI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsInventory.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        Data objdata = new Data();
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }  
        }


        public ActionResult AddCar()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            } 
        }

        [HttpPost]
        public ActionResult AddCar(Car car)
        {
            String result = "";
            Car _car = new Car();
            _car.CarId = Guid.NewGuid();
            _car.UserId = new Guid(Session["UserId"].ToString());
            _car.Brand = car.Brand;
            _car.Model = car.Model;
            _car.Year = car.Year;
            _car.Price = car.Price;
            _car.New = car.New;

            objdata.SaveCar(_car);
            return View();
        }


        [HttpPost]
        public ActionResult AddCarpopup(String Brand, String Model, int Year, decimal Price, bool New)
        {
            String result = "";
            Car _car = new Car();
            _car.CarId = Guid.NewGuid();
            _car.UserId = new Guid(Session["UserId"].ToString());
            _car.Brand = Brand;
            _car.Model = Model;
            _car.Year = Year;
            _car.Price = Price;
            _car.New = New;

         result =  objdata.SaveCar(_car);



            if (result == "Success")
            {
                TempData["message"] = "New car has been added successfully!";
                TempData["msgcolor"] = "green";
            }
            else
            {
                TempData["message"] = "Failed! Please try again...";
                TempData["msgcolor"] = "red";
            }

            return RedirectToAction("ViewCar");
        }
        public ActionResult ViewCar()
        {
            if (Session["UserID"] != null)
            {

                var car = objdata.GetCar(new Guid(Session["UserID"].ToString()) , "");
                return View(car);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            } 
        }

        [HttpPost]
        public ActionResult ViewCar(String search)
        {
            if (Session["UserID"] != null)
            {

                var car = objdata.GetCar(new Guid(Session["UserID"].ToString()), search);
                return View(car);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult EditCar(String carid)
        {
            var car = objdata.GetCarById(new Guid(carid));
            return View(car);
        }

        [HttpPost]
        public ActionResult EditCar(Car car)
        {
           string result  = "";
            Car _car = new Car();
            _car.CarId = car.CarId;
            _car.Brand = car.Brand;
            _car.Model = car.Model;
            _car.Year = car.Year;
            _car.Price = car.Price;
            _car.New = car.New;
            result = objdata.EditCar(car);
            if (result == "Success")
            {
                TempData["message"] = "Record has been updated successfully!";
                TempData["msgcolor"] = "green";
            }
            else
            {
                TempData["message"] = "Failed! Please try again.";
                TempData["msgcolor"] = "red";
            }
           
            return RedirectToAction("ViewCar");
        }

    }
}