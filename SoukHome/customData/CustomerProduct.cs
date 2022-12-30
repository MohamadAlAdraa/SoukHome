using SoukHome.Models;

namespace SoukHome.customData
{
    public struct CustomerProduct
    {
        public CustomerProduct(BasketOrder order, Product product)
        {
            this.order = order;
            this.product = product;
        }

        public BasketOrder order { get; set; }
        public Product product { get; set; }
    }
}
