using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class PreferredFoodItem : BaseItem
    {
        public string FoodItem { get; set; }
        public int UserId { get; set; }

        public PreferredFoodItem Clone()
        {
            PreferredFoodItem item = new PreferredFoodItem();
            item.Id = this.Id;
            item.FoodItem = this.FoodItem;
            item.UserId = this.UserId;
            return item;
        }
    }
}
