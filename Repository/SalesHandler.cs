using System.Data.SqlClient;
using WebApplication2.Model;

namespace WebApplication2.Repository
{
    
    public class SalesHandler
    {
        public const string connectionString = "Server = DESKTOP-I8KTH26\\SQLEXPRESS;Database = SistemaGestion;Trusted_Connection=true";
        public static List<Sales> GetSales()
        {
            List<Sales> salesList = new List<Sales>();
            Sales sales = new Sales();
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
                                sales.salesId = Convert.ToInt32(sqlDataReader["Id"]);
                                sales.salesCommentary = sqlDataReader["Descripciones"].ToString();

                                salesList.Add(sales);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return salesList;
        }
    }
}