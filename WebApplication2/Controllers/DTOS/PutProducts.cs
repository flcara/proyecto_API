namespace WebApplication2.Controllers.DTOS
{
    public class PutProducts
    {
        public int id { get; set; }
        public string putProductName { get; set; }
        public int putProductCost { get; set; }
        public int putSellingCost { get; set; }
        public int putStock { get; set; }
        public int putSellerId { get; set; }
    }
}
