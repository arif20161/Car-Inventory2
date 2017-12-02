using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CI.Business.Models;

namespace CI.Data
{
    public class Data
    {
        public string successResult = "Success";
        public string failedResult = "Failed";
        CarsInventoryContext objContext = new CarsInventoryContext();
        public string SaveUser(User user)
        {
            using (var dbcxtransaction = objContext.Database.BeginTransaction())
            {
                string em = "";
                try {
                    em = objContext.Users.FirstOrDefault(x => x.Email == user.Email).Email;
                }
                catch (Exception e)
                {
                    var lineNumber = new System.Diagnostics.StackTrace(e, true).GetFrame(0).GetFileLineNumber();
                    LogFile("SaveUser(User user):" + lineNumber + " " + e.Message + " " + e.InnerException);
                }
                try
                {
                    if (em == "")
                    {
                        objContext.Users.Add(user);
                        objContext.SaveChanges();

                        dbcxtransaction.Commit();
                        return successResult;
                    }
                    else {
                        return failedResult;
                    }
                }
                catch (Exception e)
                {
                    dbcxtransaction.Rollback();
                    var lineNumber = new System.Diagnostics.StackTrace(e, true).GetFrame(0).GetFileLineNumber();
                    LogFile("SaveUser(User user):" + lineNumber + " " + e.Message + " " + e.InnerException);
                    return failedResult;
                }
            }
        }

        public User GetUser(String email, String password)
        {
            User _user = new User();
            try{
             _user = objContext.Users.FirstOrDefault(x => x.IsDeleted == false && x.Email == email && x.Password == password);
            }
              catch (Exception e)
                {
                    var lineNumber = new System.Diagnostics.StackTrace(e, true).GetFrame(0).GetFileLineNumber();
                    LogFile("GetUser(String email, String password):" + lineNumber + " " + e.Message + " " + e.InnerException);
                }
            return _user;
        }

        public string SaveCar(Car car)
        {
            using (var dbcxtransaction = objContext.Database.BeginTransaction())
            {

                try
                {
                    objContext.Cars.Add(car);
                    objContext.SaveChanges();
                    dbcxtransaction.Commit();
                    return successResult;
                }
                catch (Exception e)
                {
                    dbcxtransaction.Rollback();
                    return failedResult;
                    var lineNumber = new System.Diagnostics.StackTrace(e, true).GetFrame(0).GetFileLineNumber();
                    LogFile("SaveCar(Car car):" + lineNumber + " " + e.Message + " " + e.InnerException);
                }
            }
        }

        public string EditCar(Car car)
        {
            using (var dbcxtransaction = objContext.Database.BeginTransaction())
            {
                Car _car = new Car();
                try
                {
                     _car = objContext.Cars.FirstOrDefault(x => x.CarId == car.CarId);
                    if (_car != null)
                    {
                        _car.Brand = car.Brand;
                        _car.Model = car.Model;
                        _car.Year = car.Year;
                        _car.Price = car.Price;
                        _car.New = car.New;
                    }
                   
                    objContext.SaveChanges();
                    dbcxtransaction.Commit();
                    return successResult;
                }
                catch (Exception e)
                {
                    dbcxtransaction.Rollback();
                    return failedResult;
                    var lineNumber = new System.Diagnostics.StackTrace(e, true).GetFrame(0).GetFileLineNumber();
                    LogFile("EditCar(Car car):" + lineNumber + " " + e.Message + " " + e.InnerException);
                }
            }
        }
        public List<Car> GetCar(Guid userid, String search)
        {
            List<Car> car = new List<Car>();
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    car = objContext.Cars.Where(x => x.UserId == userid).ToList();
                }
                else{
                    car = objContext.Cars.Where(x => x.UserId == userid && (x.Brand == search || x.Model == search)).ToList();
                }
               

            }
            catch (Exception e)
            {
                var lineNumber = new System.Diagnostics.StackTrace(e, true).GetFrame(0).GetFileLineNumber();
                LogFile("GetCar(Guid userid, String search): " + lineNumber + " " + e.Message + " " + e.InnerException);
            }
            return car;
        }


        public Car GetCarById(Guid carid)
        {
            Car car = new Car();
            try
            {
                if (carid != null)
                {
                    car = objContext.Cars.FirstOrDefault(x => x.CarId == carid);
                }
             
            }
            catch (Exception e)
            {
                var lineNumber = new System.Diagnostics.StackTrace(e, true).GetFrame(0).GetFileLineNumber();
                LogFile("GetCarById(Guid carid):" + lineNumber + " " + e.Message + " " + e.InnerException);
            }
            return car;
        }
        public void LogFile(string message)
        {
            try
            {
                var filename = System.Configuration.ConfigurationSettings.AppSettings["Logfile"].ToString();
                var sw = new System.IO.StreamWriter(filename, true);
                sw.WriteLine(DateTime.Now.ToString() + " " + message);
                sw.Close();
            }
            catch (Exception e)
            { }
        }

    }
}