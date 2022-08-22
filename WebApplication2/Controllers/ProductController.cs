using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repository;
using WebApplication2.Controllers.DTOS;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController:ControllerBase
    {
        [HttpGet(Name = "GetProducts")]
        public List<Products> GetProducts()
        {
            return ProductHandler.GetProducts();
        }

        [HttpDelete]
        public bool DeleteProduct(int id)
        {
            return ProductHandler.DeleteProduct(id);
        }
        
        [HttpPost]
        public bool AddProduct([FromBody] PostProducts postProducts)
        {
            try
            {
                return ProductHandler.AddProduct(new Products
                {
                    productDescription=postProducts.ProductDescripcion,
                    cost=postProducts.Cost,
                    sellingCost=postProducts.SellingCost,
                    stock=postProducts.Stock,
                    sellerId=postProducts.SellerId
                });
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        [HttpPut]
        public bool ModifyProduct([FromBody] PutProducts putProducts)
        {
            return ProductHandler.ModifyProduct(new Products
            {
                productId= putProducts.id,
                productDescription=putProducts.putProductName,
                cost=putProducts.putProductCost,
                sellingCost=putProducts.putSellingCost,
                stock=putProducts.putStock,
                sellerId=putProducts.putSellerId
            });
        }
    }
}
