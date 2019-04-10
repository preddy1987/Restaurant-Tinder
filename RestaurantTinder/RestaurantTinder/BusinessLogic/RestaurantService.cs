//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RestaurantTinder.Models;
//using RestaurantTinder.Interfaces;
//using RestaurantService.Exceptions;

//namespace RestaurantService
//{
//    /// <summary>
//    /// Manages all the business logic and data for the Restaurant Tinder
//    /// </summary>
//    public class RestaurantService : IRestaurantService
//    {
//        /// <summary>
//        /// Manages the user authentication and authorization
//        /// </summary>
//        private RoleManager _roleMgr = null;

//        /// <summary>
//        /// The data access layer for the Restaurant Tinder
//        /// </summary>
//        private IRestaurantService _db = null;

//        /// <summary>
//        /// Constructor that requires the interface for the database
//        /// </summary>
//        /// <param name="db"></param>
//        /// <param name="log"></param>
//        public RestaurantService(IRestaurantService db)
//        {
//            _db = db;
//            _roleMgr = new RoleManager(null);
//        }

//        public void SetUser(UserItem user)
//        {
//            _roleMgr = new RoleManager(user);
//        }

//        /// <summary>
//        /// Returns true if the Restaurant Tinder has an authenticated user
//        /// </summary>
//        public bool IsAuthenticated
//        {
//            get
//            {
//                return _roleMgr.User != null;
//            }
//        }

//        /// <summary>
//        /// Adds a new user to the Restaurant Tinder system
//        /// </summary>
//        /// <param name="userModel">Model that contains all the user information</param>
//        public void RegisterUser(User userModel)
//        {
//            UserItem userItem = null;
//            try
//            {
//                userItem = _db.GetUserItem(userModel.Username);
//            }
//            catch (Exception)
//            {
//            }

//            if (userItem != null)
//            {
//                throw new UserExistsException("The username is already taken.");
//            }

//            if (userModel.Password != userModel.ConfirmPassword)
//            {
//                throw new PasswordMatchException("The password and confirm password do not match.");
//            }

//            PasswordManager passHelper = new PasswordManager(userModel.Password);
//            UserItem newUser = new UserItem()
//            {
//                FirstName = userModel.FirstName,
//                LastName = userModel.LastName,
//                Email = userModel.Email,
//                Username = userModel.Username,
//                Salt = passHelper.Salt,
//                Hash = passHelper.Hash,
//                RoleId = (int)RoleManager.eRole.Customer
//            };

//            _db.AddUserItem(newUser);
//            LoginUser(newUser.Username, userModel.Password);
//        }

//        /// <summary>
//        /// Logs a user into the Restaurant Tinder system and throws exceptions on any failures
//        /// </summary>
//        /// <param name="username">The username of the user to authenicate</param>
//        /// <param name="password">The password of the user to authenicate</param>
//        public void LoginUser(string username, string password)
//        {
//            UserItem user = null;

//            try
//            {
//                user = _db.GetUserItem(username);
//            }
//            catch (Exception)
//            {
//                throw new Exception("Either the username or the password is invalid.");
//            }

//            PasswordManager passHelper = new PasswordManager(password, user.Salt);
//            if (!passHelper.Verify(user.Hash))
//            {
//                throw new Exception("Either the username or the password is invalid.");
//            }

//            _roleMgr = new RoleManager(user);
//        }

//        /// <summary>
//        /// Logs the current user out of the Restaurant Tinder system
//        /// </summary>
//        public void LogoutUser()
//        {
//            _roleMgr = new RoleManager(null);
//        }

//        public int AddUserItem(UserItem item)
//        {
//            throw new NotImplementedException();
//        }

//        public bool UpdateUserItem(UserItem item)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteUserItem(int userId)
//        {
//            throw new NotImplementedException();
//        }

//        public UserItem GetUserItem(int userId)
//        {
//            throw new NotImplementedException();
//        }

//        public UserItem GetUserItem(string username)
//        {
//            throw new NotImplementedException();
//        }

//        public List<UserItem> GetUserItems()
//        {
//            throw new NotImplementedException();
//        }

//        public int AddRoleItem(RoleItem item)
//        {
//            throw new NotImplementedException();
//        }

//        public List<RoleItem> GetRoleItems()
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// List of all the registered Restaurant Tinder users
//        /// </summary>
//        public IList<UserItem> Users
//        {
//            get
//            {
//                return _db.GetUserItems();
//            }
//        }

//        /// <summary>
//        /// RoleManager used to validate user permissions and have access to the current user information
//        /// </summary>
//        public RoleManager Role
//        {
//            get
//            {
//                return _roleMgr;
//            }
//        }

//        /// <summary>
//        /// The current logged in user of the Restaurant Tinder system
//        /// </summary>
//        public UserItem CurrentUser
//        {
//            get
//            {
//                return _roleMgr.User;
//            }
//        }
//    }
//}
