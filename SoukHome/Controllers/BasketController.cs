using Microsoft.AspNetCore.Mvc;
using SoukHome.customData;
using SoukHome.Data;
using SoukHome.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoukHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        readonly SoukHomeDbContext db = new();

        // GET api/<BasketController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = db.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (customer == null)
            {
                return NotFound("No customer with such ID!");
            }
            else
            {
                var basketOrders = db.BasketOrders.Select(x=>x).ToList();
                if(basketOrders!=null)
                {
                    List<int> pds = new();
                    List<int> ods = new();
                    foreach (var item in basketOrders)
                    {
                        if ((item.BasketId == customer.CustomerId) && (item.CustomerBasketEmailId == customer.Email))
                        {
                            pds.Add(item.ProductId);
                            ods.Add(item.OrderId);
                        }
                    }
                    List<CustomerProduct> customerProducts = new();
                    for (int i = 0; i < pds.Count; i++)
                    {
                        CustomerProduct customerProduct = new();
                        var product = db.Products.FirstOrDefault(x => x.ProductId == pds[i]);
                        if(product!=null)
                        {
                            Product copyProduct = new()
                            {
                                ProductId = product.ProductId,
                                StoreId = product.StoreId,
                                ProductDescription = product.ProductDescription,
                                ProductImg = product.ProductImg,
                                ProductName = product.ProductName,
                                ProductPrice = product.ProductPrice,
                                IsBestSeller = product.IsBestSeller,
                                InOffer = product.InOffer
                            };
                            customerProduct.product = copyProduct;
                        }
                        var order = db.BasketOrders.FirstOrDefault(x => x.OrderId == ods[i]);
                        if (order != null)
                        {
                            BasketOrder copyOrder = new()
                            {
                                OrderId = order.OrderId,
                                BasketId = order.BasketId,
                                CustomerBasketEmailId = order.CustomerBasketEmailId,
                                ProductId = order.ProductId,
                                OrderState = order.OrderState,   
                                Date = order.Date
                            };
                            customerProduct.order = copyOrder;
                        }
                        customerProducts.Add(customerProduct);
                    }
                    return Ok(customerProducts);
                }
                else
                {
                    return NotFound("No products yet!");
                }

            }
        }

        // POST api/<BasketController>
        [HttpPost]
        public IActionResult Post([FromBody] BasketOrder basketOrder)
        {
            var y = false;
            foreach (var item in db.Customers.Select(x => x).ToList())
            {
                if ((item.Email == basketOrder.CustomerBasketEmailId) && (item.CustomerId == basketOrder.BasketId))
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
                var product = db.Products.FirstOrDefault(x => x.ProductId == basketOrder.ProductId);
                if(product==null)
                {
                    return NotFound("No product with such id!");
                }
                else
                {
                    var dt = DateTime.Now;
                    BasketOrder basketOrder1 = new()
                    {
                        BasketId = basketOrder.BasketId,
                        CustomerBasketEmailId = basketOrder.CustomerBasketEmailId,
                        ProductId = basketOrder.ProductId,
                        OrderState = "IN PROGRESS",
                        Date = dt.ToString("MM/dd/yyyy")
                    };
                    db.BasketOrders.Add(basketOrder1);
                    db.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
        }

        // PUT api/<BasketController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BasketOrder basketOrder)
        {

            if (id != basketOrder.OrderId)
            {
                return BadRequest("Order data does not match with provided order id");
            }
            var order = db.BasketOrders.FirstOrDefault(x => x.OrderId == id);
            if (order == null)
            {
                return NotFound("No order with such ID! Create it first.");
            }
            else
            {
                order.OrderState = basketOrder.OrderState;
                db.SaveChanges();
                return Ok("Order data updated successfully!");
            }
            
        }

        // DELETE api/<BasketController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] BasketOrder basketOrder)
        {
            var y = false;
            foreach (var item in db.Customers.Select(x => x).ToList())
            {
                if ((item.Email == basketOrder.CustomerBasketEmailId) && (item.CustomerId == basketOrder.BasketId))
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
                var order = db.BasketOrders.FirstOrDefault(x => x.OrderId == id);
                if (order == null)
                {
                    return NotFound("No order with such ID!");
                }
                db.Remove(order);
                db.SaveChanges();
                return Ok("Order deleted successfully");
            }
        }
    }
}
