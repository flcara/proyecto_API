using System.Data.SqlClient;
using WebApplication2.Controllers.DTOS;
using WebApplication2.Model;

namespace WebApplication2.Repository;

public class UserHandler
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
    public static bool DeleteUser(int id)
    {
        bool deletesuccess = false;
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            string queryDelete = "DELETE FROM Usuario WHERE Id=@id";

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
    public static bool AddUser(Users users)
    {
        bool addsuccess = false;

        if (!string.IsNullOrEmpty(users.name))
        {
            if (!string.IsNullOrEmpty(users.userLastName))
            {
                if (!string.IsNullOrEmpty(users.userName) && users.userName.Contains(users.userLastName) && users.userName.StartsWith(users.name))
                {
                    if (!string.IsNullOrEmpty(users.password))
                    {
                        if (!string.IsNullOrEmpty(users.mail) && users.mail.Contains(users.userName))
                        {
                            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                            {
                                string sqlInsert = "INSERT INTO Usuario(Nombre,Apellido,NombreUsuario, Contraseña,Mail)\n" +
                                    " VALUES (@nameParameter,@lastNameParameter,@userNameParameter,@passwordParameter,@mailParameter)";

                                SqlParameter nameParameter = new SqlParameter("nameParameter", System.Data.SqlDbType.VarChar) { Value = users.name };
                                SqlParameter lastNameParameter = new SqlParameter("lastNameParameter", System.Data.SqlDbType.VarChar) { Value = users.userLastName };
                                SqlParameter userNameParameter = new SqlParameter("userNameParameter", System.Data.SqlDbType.VarChar) { Value = users.userName };
                                SqlParameter passwordParameter = new SqlParameter("passwordParameter", System.Data.SqlDbType.VarChar) { Value = users.password };
                                SqlParameter mailParameter = new SqlParameter("mailParameter", System.Data.SqlDbType.VarChar) { Value = users.mail };

                                sqlConnection.Open();

                                using (SqlCommand sqlCommand = new SqlCommand(sqlInsert, sqlConnection))
                                {
                                    sqlCommand.Parameters.Add(nameParameter);
                                    sqlCommand.Parameters.Add(lastNameParameter);
                                    sqlCommand.Parameters.Add(userNameParameter);
                                    sqlCommand.Parameters.Add(passwordParameter);
                                    sqlCommand.Parameters.Add(mailParameter);

                                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                                    if (rowsAffected != 0)
                                    {
                                        addsuccess = true;

                                    }
                                }
                                sqlConnection.Close();

                            }
                        }
                    }
                }
            }
        }
        else
            Console.WriteLine("Datos invalidos");
        return addsuccess = false;
    }
    public static bool ModifyUser(Users users)
    {
        bool modifySuccess = false;

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            string modifyUserQuery = "UPDATE Usuario\n" +
                " SET Nombre = @nameParameter, Apellido = @lastNameParameter, NombreUsuario = @codeNameParameter, Contraseña = @passwordParameter, Mail = @mailParameter\n" +
                " WHERE Id = @id";

            SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = users.userId };
            SqlParameter nameParameter = new SqlParameter("nameParameter", System.Data.SqlDbType.VarChar) { Value = users.name };
            SqlParameter nameLastParameter = new SqlParameter("lastNameParameter", System.Data.SqlDbType.VarChar) { Value = users.userLastName };
            SqlParameter codeNameParameter = new SqlParameter("codeNameParameter", System.Data.SqlDbType.VarChar) { Value = users.userName };
            SqlParameter passwordParameter = new SqlParameter("passwordParameter", System.Data.SqlDbType.VarChar) { Value = users.password };
            SqlParameter mailParameter = new SqlParameter("mailParameter", System.Data.SqlDbType.VarChar) { Value = users.mail };


            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(modifyUserQuery, sqlConnection))
            {
                sqlCommand.Parameters.Add(idParameter);
                sqlCommand.Parameters.Add(nameParameter);
                sqlCommand.Parameters.Add(nameLastParameter);
                sqlCommand.Parameters.Add(codeNameParameter);
                sqlCommand.Parameters.Add(passwordParameter);
                sqlCommand.Parameters.Add(mailParameter);
                

                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected != 0)
                {
                    return modifySuccess=true;
                }
                else
                { 
                return modifySuccess = false;
                }          
            }
        } 
        
    }
    public static List<Users> SearchUser(string name)
    {       
      List<Users> searchList = new List<Users>();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            string searchUser = "SELECT * FROM Usuario WHERE Nombre = @nameParameter";
            SqlParameter sqlParameter = new SqlParameter("nameParameter", System.Data.SqlDbType.VarChar) { Value = name };
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(searchUser, sqlConnection))
            {
                sqlCommand.Parameters.Add(sqlParameter);
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
                            searchList.Add(users);
                        }
                    }
                }
            }          
            sqlConnection.Close();
        }
        return searchList;
    }   
}
