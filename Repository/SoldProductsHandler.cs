using System.Data.SqlClient;
using WebApplication2.Model;

namespace WebApplication2.Repository
{
    public  class SoldProductsHandler
    {
        public const string connectionString = "Server = DESKTOP-I8KTH26\\SQLEXPRESS;Database = SistemaGestion;Trusted_Connection=true";
        public static List<SoldProducts> GetSoldProducts()
        {
            List<SoldProducts> soldProductsList = new List<SoldProducts>();
            SoldProducts soldProducts = new SoldProducts();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Select * FROM ProductoVendido", sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                soldProducts.psId = Convert.ToInt32(sqlDataReader["Id"]);
                                soldProducts.psStock = Convert.ToInt32(sqlDataReader["Id"]);
                                soldProducts.psIdProduct = Convert.ToInt32(sqlDataReader["Id"]);
                                soldProducts.psIdSell = Convert.ToInt32(sqlDataReader["Id"]);

                                soldProductsList.Add(soldProducts);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return soldProductsList;
        }
    }
}
