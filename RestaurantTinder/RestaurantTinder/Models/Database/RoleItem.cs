using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class RoleItem : BaseItem
    {
        public string Name { get; set; }

        public RoleItem Clone()
        {
            RoleItem item = new RoleItem();
            item.Id = this.Id;
            item.Name = this.Name;
            return item;
        }
    }
}
