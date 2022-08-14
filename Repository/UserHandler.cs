using System.Data.SqlClient;
using WebApplication2.Model;

namespace WebApplication2.Repository;

public  class UserHandler
{
    public const string connectionString = "Server = DESKTOP-I8KTH26\\SQLEXPRESS;Database = SistemaGestion;Trusted_Connection=true";
    public static List<Users> GetUsers()
    {

        List<Users> userList = new List<Users>();
        

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = new SqlCommand("Select * FROM Usuario", sqlConnection))
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Users users = new Users();
                            users.userId = Convert.ToInt32(sqlDataReader["Id"]);
                            users.name = sqlDataReader["Nombre"].ToString();
                            users.userLastName = sqlDataReader["Apellido"].ToString();
                            users.userName = sqlDataReader["NombreUsuario"].ToString();
                            users.password = sqlDataReader["Contraseña"].ToString();
                            users.mail = sqlDataReader["Mail"].ToString();
                            userList.Add(users);

                        }
                    }
                }
            }
            sqlConnection.Close();
        }
        return userList;
    }
}
