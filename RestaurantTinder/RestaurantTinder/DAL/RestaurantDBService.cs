using RestaurantTinder.Interfaces;
using System;
using RestaurantTinder.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using RestaurantService.Exceptions;


namespace RestaurantTinder.Database
{
    public class RestaurantDBService : IRestaurantService
    {
        #region Variables

        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";
        private string _connectionString;

        #endregion

        #region Constructors
        public RestaurantDBService(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region UserItem Methods

        public int AddUserItem(UserItem item)
        {
            const string sql = "INSERT [User] (FirstName, LastName, Username, Email, Hash, Salt, ZipCode, RoleId) " +
                           "VALUES (@FirstName, @LastName, @Username, @Email, @Hash, @Salt, @ZipCode, @RoleId);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Username", item.Username);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Salt", item.Salt);
                cmd.Parameters.AddWithValue("@ZipCode", item.ZipCode);
                cmd.Parameters.AddWithValue("@RoleId", item.RoleId);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateUserItem(UserItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [User] SET FirstName = @FirstName, " +
                                                 "LastName = @LastName, " +
                                                 "Username = @Username, " +
                                                 "Email = @Email, " +
                                                 "ZipCode = @ZipCode, " +
                                                 "Hash = @Hash, " +
                                                 "Salt = @Salt " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Username", item.Username);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@ZipCode", item.ZipCode);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Salt", item.Salt);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public void DeleteUserItem(int userId)
        {
            const string sql = "DELETE FROM [User] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public UserItem GetUserItem(int userId)
        {
            UserItem user = null;
            const string sql = "SELECT * From [User] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetUserItemFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        public List<UserItem> GetUserItems()
        {
            List<UserItem> users = new List<UserItem>();
            const string sql = "Select * From [User];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(GetUserItemFromReader(reader));
                }
            }

            return users;
        }

        public UserItem GetUserItem(string username)
        {
            UserItem user = null;
            const string sql = "SELECT * From [User] WHERE Username = @Username;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = GetUserItemFromReader(reader);
                    }
                }
                  return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private UserItem GetUserItemFromReader(SqlDataReader reader)
        {
            UserItem item = new UserItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.FirstName = Convert.ToString(reader["FirstName"]);
            item.LastName = Convert.ToString(reader["LastName"]);
            item.Username = Convert.ToString(reader["Username"]);
            item.Email = Convert.ToString(reader["Email"]);
            item.Salt = Convert.ToString(reader["Salt"]);
            item.Hash = Convert.ToString(reader["Hash"]);
            item.ZipCode = Convert.ToInt32(reader["ZipCode"]);
            item.RoleId = Convert.ToString(reader["RoleId"]);

            return item;
        }

        #endregion

        #region RoleItem

        public int AddRoleItem(RoleItem item)
        {
            const string sql = "INSERT RoleItem (Id, Name) " +
                               "VALUES (@Id, @Name);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.ExecuteNonQuery();
            }

            return item.Id;
        }
        public RoleItem GetRoleItem(int id)
        {
            RoleItem roleItem = new RoleItem();
            const string sql = "SELECT * FROM RoleItem WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    roleItem = GetRoleItemFromReader(reader);
                }
            }

            return roleItem;
        }
        public List<RoleItem> GetRoleItems()
        {
            List<RoleItem> roles = new List<RoleItem>();
            const string sql = "Select * From RoleItem;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(GetRoleItemFromReader(reader));
                }
            }

            return roles;
        }

        private RoleItem GetRoleItemFromReader(SqlDataReader reader)
        {
            RoleItem item = new RoleItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Name = Convert.ToString(reader["Name"]);

            return item;
        }

        public bool UpdateRoleItem(RoleItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE RoleItem SET Name = @Name WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public void DeleteRoleItem(int id)
        {
            const string sql = "DELETE FROM RoleItem WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region BlacklistItem

        public int AddBlacklistItem(BlacklistItem item)
        {
            const string sql = "INSERT BlacklistItem (Id, RestaurantId, UserId) " +
                                               "VALUES (@Id, @RestaurantId, @UserId);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@RestaurantId", item.RestaurantId);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.ExecuteNonQuery();
            }

            return item.Id;

        }

        public bool UpdateBlacklistItem(BlacklistItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE BlacklistItem SET Id = @Id, RestaurantId = @RestaurantId WHERE UserId = @UserId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@RestaurantId", item.RestaurantId);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public void DeleteBlacklistItem(int userId, int restaurantId)
        {
            const string sql = "DELETE FROM BlacklistItem WHERE userId = @UserId, RestaurantId = @RestaurantId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public BlacklistItem GetBlacklistItem(int userId, int restaurantId)
        {
            BlacklistItem blacklistItem = new BlacklistItem();
            const string sql = "SELECT * FROM BlacklistItem WHERE userId = @UserId and restaurantId = @RestaurantId;";
           

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    blacklistItem = GetBlacklistItemFromReader(reader);
                }
            }

            return blacklistItem;
        }

        public List<BlacklistItem> GetBlacklistItems(int userId)
        {
            List<BlacklistItem> blacklist = new List<BlacklistItem>();
            const string sql = "Select * From BlacklistItem;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    blacklist.Add(GetBlacklistItemFromReader(reader));
                }
            }

            return blacklist;
        }

        private BlacklistItem GetBlacklistItemFromReader(SqlDataReader reader)
        {
            BlacklistItem item = new BlacklistItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.UserId = Convert.ToInt32(reader["UserId"]);
            item.RestaurantId = Convert.ToInt32(reader["RestaurantId"]);

            return item;
        }

        #endregion

        #region FavoritesItem

        public int AddFavoritesItem(FavoritesItem item)
        {
            const string sql = "INSERT FavoritesItem (Id, RestaurantId, UserId) " +
                                   "VALUES (@Id, @RestaurantId, @UserId);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@RestaurantId", item.RestaurantId);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.ExecuteNonQuery();
            }

            return item.Id;

        }

        public void DeleteFavoritesItem(int userId, int restaurantId)
        {
            const string sql = "DELETE FROM FavoritesItem WHERE userId = @UserId, RestaurantId = @RestaurantId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public FavoritesItem GetFavoritesItem(int userId, int restaurantId)
        {
            FavoritesItem favoritesItem = new FavoritesItem();
            const string sql = "SELECT * FROM FavoritesItem WHERE userId = @UserId and restaurantId = @RestaurantId;";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    favoritesItem = GetFavoritesItemFromReader(reader);
                }
            }

            return favoritesItem;
        }

        public List<FavoritesItem> GetFavoritesItems(int userId)
        {
            List<FavoritesItem> favorites = new List<FavoritesItem>();
            const string sql = "Select * From FavoritesItem;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    favorites.Add(GetFavoritesItemFromReader(reader));
                }
            }

            return favorites;
        }

        private FavoritesItem GetFavoritesItemFromReader(SqlDataReader reader)
        {
            FavoritesItem item = new FavoritesItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.UserId = Convert.ToInt32(reader["UserId"]);
            item.RestaurantId = Convert.ToInt32(reader["RestaurantId"]);

            return item;
        }

        #endregion

        #region PreferredFoodItem

        public int AddPreferredFoodItem(PreferredFoodItem item)
        {
            const string sql = "INSERT INTO PreferredFoodItem (Id, Name, UserId) VALUES (@Id, @Name, @UserId);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        
        public void AddPreferredFoodItems(List<PreferredFoodItem> preferredFoods)
        {

            const string sql = "INSERT INTO PreferredFoodItem (Id, FoodItem, UserId) VALUES (@Id, @FoodItem, @UserId);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                foreach (var item in preferredFoods)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@FoodItem", item.FoodItem);
                    cmd.Parameters.AddWithValue("@UserId", item.UserId);
                    cmd.ExecuteNonQuery();
                    //item.Id = (int)cmd.ExecuteScalar();
                }
            }

            //return item.Id;

        }

        public void DeletePreferredFoodItem(int userId, string name)
        {
            const string sql = "DELETE FROM PreferredFoodItem WHERE name = @Name and userId = @UserId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public PreferredFoodItem GetPreferredFoodItem(int userId)
        {
            PreferredFoodItem preferredFoodItem = new PreferredFoodItem();
            const string sql = "SELECT * FROM PreferredFoodItem WHERE userId = @UserId;";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    preferredFoodItem = GetPreferredFoodItemFromReader(reader);
                }
            }

            return preferredFoodItem;
        }

        public List<PreferredFoodItem> GetPreferredFoodItems(int userId)
        {
            List<PreferredFoodItem> preferredFoods = new List<PreferredFoodItem>();
            const string sql = "Select * From PreferredFoodItem;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    preferredFoods.Add(GetPreferredFoodItemFromReader(reader));
                }
            }

            return preferredFoods;
        }

        private PreferredFoodItem GetPreferredFoodItemFromReader(SqlDataReader reader)
        {
            PreferredFoodItem item = new PreferredFoodItem();

            item.UserId = Convert.ToInt32(reader["UserId"]);
            item.Name = Convert.ToString(reader["Name"]);

            return item;
        }

        #endregion

        #region RestaurantItem

        public int AddRestaurantItem(RestaurantItem item)
        {
            const string sql = "INSERT RestaurantItem (Id, Name) VALUES (@Id, @Name);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Id", item.Id);

                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;

        }

        public bool UpdateRestaurantItem(RestaurantItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE RestaurantItem SET Name = @Name WHERE id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public void DeleteRestaurantItem(string name)
        {
            const string sql = "DELETE FROM RestaurantItem WHERE name = @Name;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public RestaurantItem GetRestaurantItem(string name)
        {
            RestaurantItem restaurantItem = new RestaurantItem();
            const string sql = "SELECT * FROM RestaurantItem WHERE name = @Name;";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    restaurantItem = GetRestaurantItemFromReader(reader);
                }
            }

            return restaurantItem;
        }

        public List<RestaurantItem> GetRestaurantItems(string name)
        {
            List<RestaurantItem> restaurants = new List<RestaurantItem>();
            const string sql = "Select * From RestaurantItem;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    restaurants.Add(GetRestaurantItemFromReader(reader));
                }
            }

            return restaurants;
        }

        private RestaurantItem GetRestaurantItemFromReader(SqlDataReader reader)
        {
            RestaurantItem item = new RestaurantItem();

            item.Name = Convert.ToString(reader["Name"]);
            item.Id = Convert.ToInt32(reader["Id"]);

            return item;
        }

        #endregion

        #region ZipItem

        public ZipItem GetZipItem(int zip)
        {
            ZipItem zipItem = new ZipItem();
            const string sql = "SELECT * FROM ZipCode WHERE ZipCode = @Zip;";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Zip", zip);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    zipItem = GetZipItemFromReader(reader);
                }
            }

            return zipItem;
        }

        private ZipItem GetZipItemFromReader(SqlDataReader reader)
        {
            ZipItem item = new ZipItem();

            item.Zip = Convert.ToInt32(reader["Zip"]);
            item.Latitude = Convert.ToDecimal(reader["Latitude"]);
            item.Longitude = Convert.ToDecimal(reader["Longitude"]);

            return item;
        }

        public void AddPreferredFoodItems(List<PreferredFoodItem> preferredFoods)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
