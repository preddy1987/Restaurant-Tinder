using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class BlacklistItem : BaseItem
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }

        public BlacklistItem Clone()
        {
            BlacklistItem item = new BlacklistItem();
            item.Id = this.Id;
            item.RestaurantId = this.RestaurantId;
            item.UserId = this.UserId;
            return item;
        }
    }
}
