using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class PreferredFoodItem : BaseItem
    {
        public string Name { get; set; }
        public int UserId { get; set; }

        public PreferredFoodItem Clone()
        {
            PreferredFoodItem item = new PreferredFoodItem();
            item.Id = this.Id;
            item.Name = this.Name;
            item.UserId = this.UserId;
            return item;
        }
    }
}
