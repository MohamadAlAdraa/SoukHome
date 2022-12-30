using Microsoft.AspNetCore.Mvc;
using SoukHome.Data;
using SoukHome.Models;

namespace SoukHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        readonly SoukHomeDbContext db = new();

        // GET: api/<AdminController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Admins);
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var admin = db.Admins.FirstOrDefault(x => x.AdminId == id);
            if (admin == null)
            {
                return NotFound("No Admin with such ID!");
            }
            return Ok(admin);
        }

        // POST api/<AdminController>
        [HttpPost]
        public IActionResult Post([FromBody] Admin admin)
        {
            var x = db.Admins.Select(x => x.Email == admin.Email).ToList();
            if (x.Contains(true))
            {
                return BadRequest("Email is already in use !");
            }
            else
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        // POST api/<AdminController>
        [HttpPost("/api/Admin/login")]
        public IActionResult PostLogin([FromBody] Admin admin)
        {
            var x = db.Admins.Select(x => x.Email == admin.Email).ToList();
            if (x.Contains(true))
            {
                var exAdmin = db.Admins.Where(x => x.Email == admin.Email).ToList();
                if (exAdmin[0].Password == admin.Password)
                {
                    return Ok(exAdmin[0]);
                }
                else
                {
                    return NotFound("Wrong Credentials");
                }
            }
            else
            {
                return NotFound("Wrong Credentials");
            }
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest("Admin data does not match with provided admin id");
            }
            var upAdmin = db.Admins.FirstOrDefault(x => x.AdminId == id);
            if (upAdmin == null)
            {
                return NotFound("No Admin with such ID! Create it first.");
            }
            else
            {
                upAdmin.FirstName = admin.FirstName;
                upAdmin.LastName = admin.LastName;
                upAdmin.Password = admin.Password;
                db.SaveChanges();
                return Ok("Admin data updated successfully. Please note that Admin email cannot be changed !");
            }
        }

        // GET api/<AdminController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var admin = db.Admins.FirstOrDefault(x => x.AdminId == id);
            if (admin == null)
            {
                return NotFound("No Admin with such ID!");
            }
            db.Remove(admin);
            db.SaveChanges();
            return Ok("Admin deleted successfully");
        }
    }
}
