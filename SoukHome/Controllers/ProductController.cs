using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoukHome.Data;
using SoukHome.Models;


namespace SoukHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly SoukHomeDbContext db = new();
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound("No product with such ID!");
            }
            return Ok(product);
        }

        // GET api/<StoreController>/GetByStore/5
        [HttpGet("/api/Store/Product/{id}")]
        public IActionResult GetByStore(int id)
        {
            var store = db.Stores.FirstOrDefault(x => x.StoreId == id);
            if (store == null)
            {
                return NotFound("No store with such ID!");
            }
            var products = db.Products.Where(x => x.StoreId == id);
            if (products.IsNullOrEmpty())
            {
                return NotFound("This store has no products yet!");
            }
            return Ok(products);
        }


        // GET api/<StoreController>/GetByStore/5
        [HttpGet("/api/Product/BestSeller")]
        public IActionResult GetByBestSellers()
        {
            var products = db.Products.Where(x => x.IsBestSeller == true);
            if (products.IsNullOrEmpty())
            {
                return NotFound("No best seller products yet!");
            }
            return Ok(products);
        }

        // GET api/<StoreController>/GetByStore/5
        [HttpGet("/api/Product/Offers")]
        public IActionResult GetByOffer()
        {
            var products = db.Products.Where(x => x.InOffer == true);
            if (products.IsNullOrEmpty())
            {
                return NotFound("No offers yet!");
            }
            return Ok(products);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            var y = false;
            foreach (var item in db.Stores.Select(x => x).ToList())
            {
                if (item.StoreId == product.StoreId)
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
                db.Products.Add(product);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Product data does not match with provided store id");
            }
            var upProduct = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (upProduct == null)
            {
                return NotFound("No product with such ID! Create it first.");
            }
            else
            {
                upProduct.ProductName = product.ProductName;
                upProduct.ProductPrice = product.ProductPrice;
                upProduct.ProductImg = product.ProductImg;
                upProduct.ProductDescription = product.ProductDescription;
                upProduct.IsBestSeller = product.IsBestSeller;
                db.SaveChanges();
                return Ok("Product data updated successfully!");
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound("No product with such ID!");
            }
            db.Remove(product);
            db.SaveChanges();
            return Ok("Product deleted successfully");
        }
    }
}
