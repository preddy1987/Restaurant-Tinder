﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class UserItem : BaseItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int ZipCode { get; set; }
        public string RoleId { get; set; }

        public UserItem Clone()
        {
            UserItem item = new UserItem();
            item.Id = this.Id;
            item.FirstName = this.FirstName;
            item.LastName = this.LastName;
            item.Username = this.Username;
            item.Email = this.Email;
            item.Hash = this.Hash;
            item.Salt = this.Salt;
            item.ZipCode = this.ZipCode;
            item.RoleId = this.RoleId;
            return item;
        }
    }
}
