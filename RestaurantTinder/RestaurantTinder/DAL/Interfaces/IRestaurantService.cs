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
        void DeleteUserItem(int id);
        UserItem GetUserItem(int id);
        UserItem GetUserItem(string name);
        List<UserItem> GetUserItems();
        #endregion

        #region RoleItem
        int AddRoleItem(RoleItem item);
        List<RoleItem> GetRoleItems();
        RoleItem GetRoleItem(int roleId);
        bool UpdateRoleItem(RoleItem item);
        void DeleteRoleItem(int roleId);
        #endregion

        #region BlacklistItem
        int AddBlacklistItem(BlacklistItem item);
        void DeleteBlacklistItem(int userId, int restaurantId);
        BlacklistItem GetBlacklistItem(int userId, int restaurantId);
        List<BlacklistItem> GetBlacklistItems(int userId);
        #endregion

        #region FavoritesItem
        int AddFavoritesItem(FavoritesItem item);
        void DeleteFavoritesItem(int userId, int restaurantId);
        FavoritesItem GetFavoritesItem(int userId, int restaurantId);
        List<FavoritesItem> GetFavoritesItems(int userId);
        #endregion

        #region PreferredFoodItem
        int AddPreferredFoodItem(PreferredFoodItem item);
        void AddPreferredFoodItems(List<PreferredFoodItem> preferredFoods);
        void DeletePreferredFoodItem(int userId, string name);
        PreferredFoodItem GetPreferredFoodItem(int userId);
        List<PreferredFoodItem> GetPreferredFoodItems(int userId);
        #endregion

        #region RestaurantItem
        int AddRestaurantItem(RestaurantItem item);
        bool UpdateRestaurantItem(RestaurantItem item);
        void DeleteRestaurantItem(string name);
        RestaurantItem GetRestaurantItem(string name);
        List<RestaurantItem> GetRestaurantItems(string name);
        #endregion

        #region ZipItem
        ZipItem GetZipItem(int zip);
        #endregion
    }
}
