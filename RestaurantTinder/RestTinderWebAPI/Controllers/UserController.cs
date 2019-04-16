using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantService;
using RestaurantService.Security;
using RestaurantTinder.Interfaces;
using RestaurantTinder.Models;
using RestTinderWebAPI.Models;
using RestTinderWebAPI.ViewModels;

namespace RestTinderWebAPI.Controllers
{
    public class UserController : AuthController
    {

        public UserController(IRestaurantService db, IHttpContextAccessor httpContext, ITokenGenerator tokenGenerator) : base(db, httpContext, tokenGenerator)
        {
            
        }
        
        [HttpGet]
        [Route("api/user")]
        public ActionResult<IEnumerable<UserItem>> Get()
        {
            var result = Json(_db.GetUserItems());
            return GetAuthenticatedJson(result, (Role.IsExecutive || Role.IsAdministrator));
            
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginViewModel info)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();
            
            try
            {
                LoginUser(info.UserName, info.Password);
            }
            catch(Exception )
            {
             
            }
            // Get the user by username
            var user = _db.GetUserItem(info.UserName);

            // If we found a user and the password has matches

            PasswordManager passHelper = null;
            try
            {
                 passHelper = new PasswordManager(info.Password, user.Salt);
            }
            catch
            {

            }
            if (user != null && passHelper.Verify(user.Hash))
            {
                // Create an authentication token
                var token = tokenGenerator.GenerateToken(user.Username, user.RoleId);

                // Switch to 200 OK
                result = Ok(token);

            }

            return result;
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody] RegisterViewModel info)
        {
               var user = new User();

            try
            {
                user.ConfirmPassword = info.ConfirmPassword;
                user.Email = info.Email;
                user.FirstName = info.FirstName;
                user.LastName = info.LastName;
                user.Password = info.Password;
                user.Username = info.Username;
                user.ZipCode = info.ZipCode;
                user.RoleId = "Customer";

                RegisterUser(user);
            }
            catch (Exception )
            {
                return BadRequest(new
                {
                    Message = "Username has already been taken."
                });
            }
            var token = tokenGenerator.GenerateToken(user.Username, user.RoleId);

            return Ok(token);
        }
        [HttpGet]
        [Route("api/preferences")]
        public ActionResult<IEnumerable<UserItem>> GetUserPref()
        {
            var result = Json(_db.GetPreferredFoodItems(CurrentUser.Id));
            return GetAuthenticatedJson(result, (Role.IsCustomer));
        }
        [HttpPost]
        [Route("api/savepreference")]
        public ActionResult<IEnumerable<UserItem>> SaveUserPref([FromBody] PreferenceViewModel foodPreferences)
        {
            JsonResult result = null;
            List<PreferredFoodItem> preferredFoodItems = new List<PreferredFoodItem>();
            foreach (var name in foodPreferences.Names)
            {
                PreferredFoodItem preferredFood = new PreferredFoodItem();
                preferredFood.FoodItem = name;
                preferredFood.UserId = CurrentUser.Id;
                preferredFoodItems.Add(preferredFood);
            }
            _db.AddPreferredFoodItems(preferredFoodItems);

            return GetAuthenticatedJson(result, (Role.IsCustomer));
        }

    }
}