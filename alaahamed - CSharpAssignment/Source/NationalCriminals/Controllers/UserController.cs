using NationalCriminals.Models;
using NationalCriminals.NationalCriminalsWcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NationalCriminals.Controllers
{
    public class UserController : Controller
    {


        #region Login Actions
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user, string returnUrl)
        {

            var nationalCriminalsWcf = new NationalCriminalSvcClient();

            var userProxy = Mapper.MapUserModelToUserProxy(user);

            //Retreive user from database
            var userInstance = nationalCriminalsWcf.GetUserInstanceFromDb(userProxy);

            //If exist
            if (userInstance != null)
            {

                //Set Auth cookie
                var userAuthCookie = userInstance.UserName + ":" + userInstance.Email;


                //We can define custom Iprinicipal class to store email
                FormsAuthentication.SetAuthCookie(userAuthCookie, false);

                if (ModelState.IsValid && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Search", "Criminals");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            return View(user);
        }

        #endregion

        #region Logoff Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "User");
        }

        #endregion


        #region Register actions
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                //Create new instance of the service 
                var nationalCriminalsWcf = new NationalCriminalSvcClient();

                //create new user proxy instance
                var userProxy = Mapper.MapUserModelToUserProxy(user);

                //Add user to the database 
                nationalCriminalsWcf.AddUser(userProxy);


                //Redirect to login action
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "There was an error in register try again later");

            return View(user);
        }


        #endregion








    }
}
