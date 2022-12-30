using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoukHome.Data;
using SoukHome.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoukHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        readonly SoukHomeDbContext db = new();

        // GET: api/<OfferController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Offers);
        }

        // GET api/<OfferController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var offer = db.Offers.FirstOrDefault(x => x.OfferId == id);
            if (offer == null)
            {
                return NotFound("No offer with such ID!");
            }
            return Ok(offer);
        }

        // GET api/<OfferController>/5
        [HttpGet("/api/Product/Offer/{id}")]
        public IActionResult GetByProduct(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound("No product with such ID!");
            }
            var offers = db.Offers.Where(x => x.ProductId == id);
            if (offers.IsNullOrEmpty())
            {
                return NotFound("This product has no offers yet!");
            }
            return Ok(offers);
        }
        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Offer offer)
        {
            var y = false;
            foreach (var item in db.Products.Select(x => x).ToList())
            {
                if (item.ProductId == offer.ProductId)
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
                db.Offers.Add(offer);
                db.SaveChanges();
                var upProduct = db.Products.FirstOrDefault(x => x.ProductId == offer.ProductId);
                upProduct.InOffer = true;
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        // PUT api/<OfferController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Offer offer)
        {
            if (id != offer.OfferId)
            {
                return BadRequest("Offer data does not match with provided offer id");
            }
            var upOffer = db.Offers.FirstOrDefault(x => x.OfferId == id);
            if (upOffer == null)
            {
                return NotFound("No offer with such ID! Create it first.");
            }
            else
            {
                upOffer.ProductId = offer.ProductId;
                upOffer.NewPrice = offer.NewPrice;
                upOffer.OfferPercentage = offer.OfferPercentage;
                upOffer.ExpirationDate = offer.ExpirationDate;
                db.SaveChanges();
                return Ok("Offer data updated successfully!");
            }
        }

        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var offer = db.Offers.FirstOrDefault(x => x.OfferId == id);
            if (offer == null)
            {
                return NotFound("No offer with such ID!");
            }
            var upProduct = db.Products.FirstOrDefault(x => x.ProductId == offer.ProductId);
            upProduct.InOffer = false;
            db.SaveChanges();
            db.Remove(offer);
            db.SaveChanges();
            return Ok("Offer deleted successfully");
        }
    }
}
