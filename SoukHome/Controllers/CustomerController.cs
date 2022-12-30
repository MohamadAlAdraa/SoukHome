using Microsoft.AspNetCore.Mvc;
using SoukHome.Data;
using SoukHome.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoukHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly SoukHomeDbContext db = new();

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = db.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (customer == null)
            {
                return NotFound("No customer with such ID!");
            }
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            var x = db.Customers.Select(x => x.Email == customer.Email).ToList();
            if (x.Contains(true))
            {
                return BadRequest("Email is already in use !");
            }
            else
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                var idOfLastCustomer = db.Customers.OrderBy(x => x.CustomerId).Last().CustomerId;
                Basket customerBasket = new()
                {
                    BasketId = idOfLastCustomer,
                    CustomerBasketEmailId = customer.Email
                };
                db.Baskets.Add(customerBasket);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        // POST api/<AdminController>
        [HttpPost("/api/Customer/login")]
        public IActionResult PostLogin([FromBody] Customer customer)
        {
            var x = db.Customers.Select(x => x.Email == customer.Email).ToList();
            if (x.Contains(true))
            {
                var exAdmin = db.Customers.Where(x => x.Email == customer.Email).ToList();
                if (exAdmin[0].Password == customer.Password)
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

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = db.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (customer == null)
            {
                return NotFound("No customer with such ID!");
            }
            db.Remove(customer);
            db.SaveChanges();
            return Ok("Customer deleted successfully");
        }
    }
}
