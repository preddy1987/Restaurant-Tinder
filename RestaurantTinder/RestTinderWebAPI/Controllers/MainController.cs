using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestaurantService;
using RestaurantService.Security;
using RestaurantTinder;
using RestaurantTinder.Interfaces;
using RestaurantTinder.Models;
using RestTinderWebAPI.Models;

namespace RestTinderWebAPI.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : AuthController
    {

        public MainController(IRestaurantService db, IHttpContextAccessor httpContext, ITokenGenerator tokenGenerator) : base(db, httpContext, tokenGenerator)
        {

        }


        [HttpGet]
        public ActionResult<JObject> Index()
        {
            var zip = _db.GetZipItem(CurrentUser.ZipCode);
            var foodItem = _db.GetFavoritesItems(CurrentUser.Id);
            dynamic obj = null;

            foreach (var item in foodItem)
            {
                string testURL = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={{zip.Latitude}},{{zip.Longitude}}&radius=50000&type=restaurant&keyword={{item}}&key=AIzaSyDDHeRZd4LXtzzV41AN2CiZPXEA7R8Y3Tg";

                string json = HttpGet(testURL);

                obj = JObject.Parse(json);

                
            }
            return obj;
        }



        public string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            try
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);

                    return reader.ReadToEnd();
                }
            }
            finally
            {
                response.Close();
            }
        }


    }
}