using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantTinder.Interfaces;
using RestaurantTinder.Models;
using RestTinderWebAPI.Models;
using RestaurantTinder.Database;

namespace RestTinderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRestaurantService _db = null;

        public RoleController(IRestaurantService db)
        {
            _db = db;
        }

        // GET api/role
        [HttpGet]
        public ActionResult<IEnumerable<RoleItem>> Get()
        {
            return _db.GetRoleItems();
        }

        // GET api/role/5
        [HttpGet("{id}")]
        public ActionResult<RoleItem> Get(int id)
        {
            return _db.GetRoleItem(id);
        }

        // POST api/role
        [HttpPost]
        public ActionResult<int> Post([FromBody] RoleItemViewModel value)
        {
            RoleItem item = new RoleItem();
            item.Name = value.Name;
            item.Id = value.Id;
            return _db.AddRoleItem(item);
        }

        // PUT api/role/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id, [FromBody] RoleItemViewModel value)
        {
            RoleItem item = new RoleItem();
            item.Id = id;
            item.Name = value.Name;
            return _db.UpdateRoleItem(item);
        }

        // DELETE api/role/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _db.DeleteRoleItem(id);
        }
    }
}