using System.Data.SqlClient;
using WebApplication2.Controllers.DTOS;
using WebApplication2.Model;

namespace WebApplication2.Repository
{
    public class ProductHandler
    {
        public const string connectionString = "Server = DESKTOP-I8KTH26\\SQLEXPRESS;Database = SistemaGestion;Trusted_Connection=true";

        public static List<Products> GetProducts()
        {
            List<Products> productsList = new List<Products>();
            
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
                                Products products = new Products();
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
        public static bool DeleteProduct(int id)
        {
            bool deletesuccess = false;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryDelete = "DELETE FROM Producto WHERE Id=@id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);

                    int rowsAffecteds = sqlCommand.ExecuteNonQuery();

                    if (rowsAffecteds > 0)
                    {
                        deletesuccess = true;
                    }
                    else
                        Console.WriteLine("No se hayó el ID a borrar");
                }
                sqlConnection.Close();
            }
            return deletesuccess;
        }
        public static bool AddProduct(Products products)
        {
            bool addSuccess = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string sqlInsert = "INSERT INTO Producto (Descripciones,Costo,PrecioVenta,Stock,IdUsuario)\n" +
                    "VALUES (@descParameter,@costParameter,@sellingCostParameter,@stockParameter,@sellerIdParameter)";

                SqlParameter descParameter = new SqlParameter("descParameter", System.Data.SqlDbType.VarChar) { Value = products.productDescription };
                SqlParameter costParameter = new SqlParameter("costParameter", System.Data.SqlDbType.Money) { Value = products.cost };
                SqlParameter sellingCostParameter = new SqlParameter("sellingCostParameter", System.Data.SqlDbType.Money) { Value = products.sellingCost };
                SqlParameter stockParameter = new SqlParameter("stockParameter", System.Data.SqlDbType.Int) { Value = products.stock };
                SqlParameter sellerIdParameter = new SqlParameter("sellerIdParameter", System.Data.SqlDbType.BigInt) { Value = products.sellerId };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(sqlInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descParameter);
                    sqlCommand.Parameters.Add(costParameter);
                    sqlCommand.Parameters.Add(sellingCostParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(sellerIdParameter);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected != 0)
                    {
                        addSuccess = true;

                    }
                    else
                        return addSuccess = false;
                }
                sqlConnection.Close();

            }
            return addSuccess;   
        }
        public static bool ModifyProduct(Products products)
        {
            bool modifySuccess = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string modifyUserQuery = "UPDATE Producto\n" +
                    " SET Descripciones = @descParameter, Costo = @costParameter, PrecioVenta = @sellingPriceParameter, Stock = @stockParameter, IdUsuario = @sellerIdParameter\n" +
                    " WHERE Id = @id";

                SqlParameter idProductParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = products.productId };
                SqlParameter descParameter = new SqlParameter("descParameter", System.Data.SqlDbType.VarChar) { Value = products.productDescription };
                SqlParameter costParameter = new SqlParameter("costParameter", System.Data.SqlDbType.Money) { Value = products.cost };
                SqlParameter sellingPriceParameter = new SqlParameter("sellingPriceParameter", System.Data.SqlDbType.Money) { Value = products.sellingCost };
                SqlParameter stockParameter = new SqlParameter("stockParameter", System.Data.SqlDbType.Int) { Value = products.stock };
                SqlParameter sellerIdParameter = new SqlParameter("sellerIdParameter", System.Data.SqlDbType.BigInt) { Value = products.sellerId };
                
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(modifyUserQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idProductParameter);
                    sqlCommand.Parameters.Add(descParameter);
                    sqlCommand.Parameters.Add(costParameter);
                    sqlCommand.Parameters.Add(sellingPriceParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(sellerIdParameter);
                    

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected != 0)
                    {
                        return modifySuccess = true;
                    }
                    else
                    {
                        return modifySuccess = false;
                    }
                }
            }

        }

    }
}
