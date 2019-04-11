using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class ZipItem : BaseItem
    {
        public int Zip { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public ZipItem Clone()
        {
            ZipItem item = new ZipItem();
            item.Zip = this.Zip;
            item.Latitude = this.Latitude;
            item.Longitude = this.Longitude;
            return item;
        }
    }
}
