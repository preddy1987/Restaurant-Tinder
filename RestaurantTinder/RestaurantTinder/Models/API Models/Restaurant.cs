using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models.API_Models
{
    [Serializable]
    public class Restaurant
    {
        public string Name { get; set; }
        public string PlaceID { get; set; }
        public int PriceLevel { get; set; }
        public int Rating { get; set; }
        public Array Types { get; set; }
        public int UserRatingsTotal { get; set; }
        public string Vicinity { get; set; }
    }
}
