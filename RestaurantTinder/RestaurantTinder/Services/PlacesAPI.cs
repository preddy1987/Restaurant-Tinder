using Newtonsoft.Json;
using RestaurantTinder.Models.API_Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RestaurantTinder
{
    public class PlacesAPI
    {
        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = null;

            string testURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=39.325714,-84.552390&radius=50000&type=restaurant&keyword=mexican&key=AIzaSyDDHeRZd4LXtzzV41AN2CiZPXEA7R8Y3Tg";
                var req = WebRequest.Create(testURL);
                var stream = req.GetResponse().GetResponseStream();
                using (StreamReader sr = new StreamReader(stream))
                {
                    var jsonStr = sr.ReadToEnd();
                    restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(jsonStr);
                    //Console.WriteLine(jsonStr);
                }
            return restaurants;
        }
    }
}
