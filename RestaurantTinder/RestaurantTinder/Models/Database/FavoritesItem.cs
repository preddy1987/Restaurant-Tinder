using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class FavoritesItem : BaseItem
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }

        public FavoritesItem Clone()
        {
            FavoritesItem item = new FavoritesItem();
            item.Id = this.Id;
            item.RestaurantId = this.RestaurantId;
            item.UserId = this.UserId;
            return item;
        }
    }
}
