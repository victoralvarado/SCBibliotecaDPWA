using SCBiblioteca.Controllers;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SCBiblioteca.Models
{
    public class ModelDB
    {
        private SqlConnection con;
        private void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["SCBiblioteca"].ToString();
            con = new SqlConnection(constring);
        }


        //Login
        public bool Login(string usuario, string password)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select Usuario.IdUsuario, Rol.Rol, Usuario.CorreoElectronico from Usuario inner join Rol on Usuario.IdRol = Rol.IdRol where Usuario.Usuario=@usuario and Usuario.Password=@password;", con);
            cmd.CommandType = CommandType.Text;
            SqlParameter usu = new SqlParameter("@usuario", SqlDbType.NVarChar, int.MaxValue);
            SqlParameter pas = new SqlParameter("@password", SqlDbType.NVarChar, int.MaxValue);
            usu.Value = usuario;
            pas.Value = Security.Encrypt(password);
            cmd.Parameters.Add(usu);
            cmd.Parameters.Add(pas);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    HttpContext.Current.Session["UserSesion"] = rdr["Rol"].ToString();
                    HttpContext.Current.Session["UserEmail"] = rdr["CorreoElectronico"].ToString();
                    HttpContext.Current.Session["UserId"] = rdr["IdUsuario"].ToString();
                    HttpContext.Current.Session["ClienteId"] = IdCliente(rdr["CorreoElectronico"].ToString());
                    return true;
                }
            }
            else
            {
                return false;
            }
            con.Close();
            return false;
        }


        //Crear Cuenta
        public bool CrearCuenta(Usuario u, Cliente c)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("registroUsuarioCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreCompleto", u.NombreCompleto);
            cmd.Parameters.AddWithValue("@FechaNacimiento", u.FechaNacimiento);
            cmd.Parameters.AddWithValue("@Direccion", u.Direccion);
            cmd.Parameters.AddWithValue("@CorreoElectronico", u.NombreCompleto);
            cmd.Parameters.AddWithValue("@Usuario", u.Usuario1);
            cmd.Parameters.AddWithValue("@Password", Security.Encrypt(u.Password));
            cmd.Parameters.AddWithValue("@IdRol", Rol());
            cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }


        //Consultado el Rol del Usuario
        public int Rol()
        {
            Connection();
            int Rol = 0;
            SqlCommand cmd = new SqlCommand("select IdRol from Rol where Rol = 'Usuario';", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Rol = (int)rdr["IdRol"];
                    return Rol;
                }
            }
            con.Close();
            return Rol;
        }


        //Consultando el Id de Cliente Segun su Email
        public int IdCliente(string email)
        {
            int Id = 0;
            SqlCommand cmd = new SqlCommand("select IdCliente from Cliente where CorreoElectronico=@email", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter em = new SqlParameter("@email", SqlDbType.NVarChar, int.MaxValue);
            em.Value = email;
            cmd.Parameters.Add(em);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Id = (int)rdr["IdCliente"];
                    return Id;
                }
            }
            else
            {
                return Id;
            }
            return Id;
        }


        //Consultar Stock Libros
        public int StockLibros(int idLibro)
        {
            Connection();
            con.Open();
            int stock = 0;
            SqlCommand cmd = new SqlCommand("select Stock from Libro where IdLibro =@idLibro", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idLibro", SqlDbType.Int, int.MaxValue);
            id.Value = idLibro;
            cmd.Parameters.Add(id);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    stock = (int)rdr["Stock"];
                    return stock;
                }
            }
            else
            {
                return stock;
            }
            con.Close();
            return stock;
        }

        //Titulo Libro
        public string TituloLibro(int? idLibro)
        {
            string titulo = "";
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select Titulo from Libro where IdLibro =@idLibro", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idLibro", SqlDbType.NVarChar, int.MaxValue);
            id.Value = idLibro;
            cmd.Parameters.Add(id);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    titulo = rdr["Titulo"].ToString();
                    return titulo;
                }
            }
            else
            {
                return titulo;
            }
            con.Close();
            return titulo;
        }

    }
}

