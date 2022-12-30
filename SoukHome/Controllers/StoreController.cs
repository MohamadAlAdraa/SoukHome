using Microsoft.AspNetCore.Mvc;
using SoukHome.Data;
using SoukHome.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoukHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        readonly SoukHomeDbContext db = new();

        // GET: api/<StoreController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Stores);
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var store = db.Stores.FirstOrDefault(x => x.StoreId == id);
            if (store == null)
            {
                return NotFound("No store with such ID!");
            }
            return Ok(store);
        }

        // POST api/<StoreController>
        [HttpPost]
        public IActionResult Post([FromBody] Store store)
        {
            var x = db.Stores.Select(x => x.StoreName == store.StoreName).ToList();
            if (x.Contains(true))
            {
                return BadRequest("Store name is already taken !");
            }
            else
            {
                var y = false;
                foreach (var item in db.Admins.Select(x => x).ToList())
                {
                    if ((item.Email == store.Email)&&(item.AdminId == store.AdminId))
                    {
                        y = true;
                        break;
                    }
                }
                if (!y)
                {
                    return BadRequest("Not authorized !");
                }
                else
                {
                    db.Stores.Add(store);
                    db.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Store store)
        {
            if (id != store.StoreId)
            {
                return BadRequest("Store data does not match with provided store id");
            }
            var upStore = db.Stores.FirstOrDefault(x => x.StoreId == id);
            if (upStore == null)
            {
                return NotFound("No Store with such ID! Create it first.");
            }
            if ((upStore.Email != store.Email) || (upStore.AdminId != store.AdminId))
            {
                return BadRequest("Not authorized !");
            }
            else
            {
                upStore.StoreName = store.StoreName;
                upStore.StoreOwner = store.StoreOwner;
                upStore.StoreLogo = store.StoreLogo;
                upStore.StoreLocation = store.StoreLocation;
                db.SaveChanges();
                return Ok("Store data updated successfully!");
            }
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var store = db.Stores.FirstOrDefault(x => x.StoreId == id);
            if (store == null)
            {
                return NotFound("No Store with such ID!");
            }
            db.Remove(store);
            db.SaveChanges();
            return Ok("Store deleted successfully");
        }
    }
}
