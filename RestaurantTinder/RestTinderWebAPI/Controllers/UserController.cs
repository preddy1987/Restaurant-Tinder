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
            if (_roleMgr.User != null )
            {
                // Create an authentication token
                var token = tokenGenerator.GenerateToken(_roleMgr.User);

                // Switch to 200 OK
                result = Ok(token);

            }

            return result;
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody] RegisterViewModel info)
        {
               

            try
            {
                var user = new User();
                user.ConfirmPassword = info.ConfirmPassword;
                user.Email = info.Email;
                user.FirstName = info.FirstName;
                user.LastName = info.LastName;
                user.Password = info.Password;
                user.Username = info.Username;
                user.ZipCode = info.ZipCode;
                RegisterUser(user);
            }
            catch (Exception )
            {
                return BadRequest(new
                {
                    Message = "Username has already been taken."
                });
            }
            var token = tokenGenerator.GenerateToken(_roleMgr.User);

            return Ok(token);
        }
        [HttpGet]
        [Route("api/preferences")]
        public ActionResult<List<PreferredFoodItem>> GetUserPref()
        {
            var result = _db.GetPreferredFoodItems(CurrentUser.Id);
            return result;
        }
        [HttpPost]
        [Route("api/savepreference")]
        public void SaveUserPref([FromBody] string preference)
        {
            PreferredFoodItem preferredFood = new PreferredFoodItem();
            preferredFood.Name = preference;
            preferredFood.UserId = CurrentUser.Id;
            _db.AddPreferredFoodItem(preferredFood);
        }
    }
}