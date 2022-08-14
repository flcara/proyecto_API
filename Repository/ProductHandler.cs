using System.Data.SqlClient;
using WebApplication2.Model;

namespace WebApplication2.Repository
{
    public class ProductHandler
    {
        public const string connectionString = "Server = DESKTOP-I8KTH26\\SQLEXPRESS;Database = SistemaGestion;Trusted_Connection=true";

        public static List<Products> GetProducts()
        {
            List<Products> productsList = new List<Products>();
            Products products = new Products();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Select * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                products.productId = Convert.ToInt32(sqlDataReader["Id"]);
                                products.productDescription = sqlDataReader["Descripciones"].ToString();
                                products.cost = Convert.ToInt32(sqlDataReader["Costo"]);
                                products.sellingCost = Convert.ToInt32(sqlDataReader["PrecioVenta"]);
                                products.stock = Convert.ToInt32(sqlDataReader["Stock"]);
                                products.sellerId = Convert.ToInt32(sqlDataReader["IdUsuario"]);

                                productsList.Add(products);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return productsList;
        }
    }
}
