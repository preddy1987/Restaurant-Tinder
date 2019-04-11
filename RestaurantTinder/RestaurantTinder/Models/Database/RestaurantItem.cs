using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class RestaurantItem : BaseItem
    {
        public string Name { get; set; }

        public RestaurantItem Clone()
        {
            RestaurantItem item = new RestaurantItem();
            item.Id = this.Id;
            item.Name = this.Name;
            return item;
        }
    }
}
