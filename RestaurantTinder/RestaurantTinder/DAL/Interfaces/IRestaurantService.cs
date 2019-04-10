using System;
using System.Collections.Generic;
using System.Text;
using RestaurantTinder.Models;

namespace RestaurantTinder.Interfaces
{
    public interface IRestaurantService
    {
        #region UserItem
        int AddUserItem(UserItem item);
        bool UpdateUserItem(UserItem item);
        void DeleteUserItem(int userId);
        UserItem GetUserItem(int userId);
        UserItem GetUserItem(string username);
        List<UserItem> GetUserItems();
        #endregion

        #region RoleItem
        int AddRoleItem(RoleItem item);
        List<RoleItem> GetRoleItems();
        #endregion        
    }
}
