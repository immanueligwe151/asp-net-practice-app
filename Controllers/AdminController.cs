using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FirebaseAdmin.Auth;
using WebApplication1.Models; //takes all user models as defined in /Models

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserViewModel model)
            //since the data model is defined in CreateUserViewModel
        {
            if (!ModelState.IsValid)
            {
                return View("CreateUser", model);
            }

            try
            {
                var userRecordArgs = new UserRecordArgs()
                {
                    Email = model.Email,
                    Password = model.Password,
                    EmailVerified = false,
                    Disabled = false
                };

                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

                ViewBag.SuccessMessage = "User created successfully!";
                return View("CreateUser"); // stays on the same page
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error creating user: " + ex.Message;
                return View("CreateUser", model);
            }
        }

        // GET: Admin
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
    }
}