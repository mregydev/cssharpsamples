using NationalCriminals.Models;
using NationalCriminals.NationalCriminalsWcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationalCriminals.Controllers
{
    [Authorize]
    public class CriminalsController : Controller
    {
        #region Criminals Actions

        #region Searhcing Actions


        public ActionResult Search()
        {
            //Default search parameters
            var defaultCriminalModel = new CriminalsSearchModel { IsMale = true, Name = "test" };

            return View(defaultCriminalModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(CriminalsSearchModel searchModel)
        {

            #region Validations
            if (searchModel.MaxAge < searchModel.MinAge)
            {
                ModelState.AddModelError("", "Max Age should be bigger than min age");
                return View(searchModel);
            }
            else if (searchModel.MaxWeight < searchModel.MinWeight)
            {
                ModelState.AddModelError("", "Mac weight should be bigger than min weight ");
                return View(searchModel);
            }

            else if (searchModel.MaxHeight < searchModel.MinHeight)
            {
                ModelState.AddModelError("", "Max height should be bigger than min height");
                return View(searchModel);
            }

            else if (!searchModel.IsMale && !searchModel.IsFemale)
            {
                ModelState.AddModelError("", "You should choose either male or female or both of them");
                return View(searchModel);
            }

            #endregion
            else
            {
                searchModel.MaxResultCount = 20;

                //get logged in user email 
                //User.Identity.Name="name:email"
                var email = User.Identity.Name.Split(':')[1];

                //build search filter criteria
                var criminalSearchCriteria = Mapper.MapCriminalSearchModelToCriminalSearchCriteria(searchModel);

                //create new instance of the national criminals service 
                var nationalCriminalsSvc = new NationalCriminalSvcClient();

                //Search for criminals 
                var result = nationalCriminalsSvc.SearchCrininals(email, criminalSearchCriteria);

                if (result)
                {
                    ViewBag.Result = "Search completed successfully and email will be sent to you";
                }
                else
                {
                    ViewBag.Result = "error occured try again later";
                }

                return View(searchModel);
            }

        }

        #endregion

        #region Adding Actions

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CriminalModel criminal)
        {
            if (ModelState.IsValid)
            {
                //Create new instance of the national criminals wcf 
                var nationalCriminalsWcf = new NationalCriminalSvcClient();

                //Adds to database 
                var criminalProxy = Mapper.MapCriminalModelToCriminalProxy(criminal);
                nationalCriminalsWcf.AddCriminal(criminalProxy);

                //Redirecto to search action
                return RedirectToAction("Search");

            }
            //Show message in case of errors
            ModelState.AddModelError("", "There was an error in register try again later");

            return View(criminal);
        }

        #endregion

        #endregion
    }
}
