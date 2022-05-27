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
            string constring = ConfigurationManager.ConnectionStrings["SCBibliotecaEntities"].ToString().Split('"')[1];
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
                    HttpContext.Current.Session["ClienteId"] = IdCliente(rdr["CorreoElectronico"].ToString()).ToString();
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



        //Mostrar Datos Usuario
        public bool MostrarDatosUsuario(string correo)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select Usuario.NombreCompleto,Usuario.Usuario, Usuario.FechaNacimiento,Usuario.Direccion,Usuario.Password,Cliente.Telefono from Usuario inner join Cliente on Usuario.CorreoElectronico=Cliente.CorreoElectronico where Usuario.CorreoElectronico=@CorreoElectronico", con);
            cmd.CommandType = CommandType.Text;
            SqlParameter cor = new SqlParameter("@correoElectronico", SqlDbType.NVarChar, int.MaxValue);
            cor.Value = correo;
            cmd.Parameters.Add(cor);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    HttpContext.Current.Session["DataNombreCompleto"] = rdr["NombreCompleto"].ToString();
                    HttpContext.Current.Session["DataUsuario"] = rdr["Usuario"].ToString();
                    HttpContext.Current.Session["DataFechaNacimiento"] = rdr["FechaNacimiento"].ToString();
                    HttpContext.Current.Session["DataDirecion"] = rdr["Direccion"].ToString();
                    HttpContext.Current.Session["DataPassword"] = Security.Decrypt(rdr["Password"].ToString());
                    HttpContext.Current.Session["DataTelefono"] = rdr["Telefono"].ToString();

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


        //Actualizar Usuario Cliente 
        public bool ActualizarUsuarioCliente(Usuario u, Cliente c, int idCliente)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("actualizarUsuarioCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreCompleto", u.NombreCompleto);
            cmd.Parameters.AddWithValue("@FechaNacimiento", u.FechaNacimiento);
            cmd.Parameters.AddWithValue("@Direccion", u.Direccion);
            cmd.Parameters.AddWithValue("@CorreoElectronicoN", u.CorreoElectronico);
            cmd.Parameters.AddWithValue("@Usuario", u.Usuario1);
            cmd.Parameters.AddWithValue("@Password", Security.Encrypt(u.Password));
            cmd.Parameters.AddWithValue("@IdRol", Rol());
            cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
            cmd.Parameters.AddWithValue("@Id", u.IdUsuario);
            cmd.Parameters.AddWithValue("@IdCliente", idCliente);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
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
            cmd.Parameters.AddWithValue("@CorreoElectronico", u.CorreoElectronico);
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

        public int CompraCantidad(int idCompra)
        {
            Connection();
            con.Open();
            int cantidad = 0;
            SqlCommand cmd = new SqlCommand("select Cantidad from Compra where IdCompra = @idCompra", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idCompra", SqlDbType.Int, int.MaxValue);
            id.Value = idCompra;
            cmd.Parameters.Add(id);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    cantidad = (int)rdr["Cantidad"];
                    return cantidad;
                }
            }
            else
            {
                return cantidad;
            }
            con.Close();
            return cantidad;
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


        //Sumar Stock
        public bool SumarStock(int cantidad, int idLibro)
        {
            int stock = StockLibros(idLibro);
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Libro set Stock=(@stock+@cantidad) where IdLibro=@id;", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter es = new SqlParameter("@stock", SqlDbType.Int, int.MaxValue);
            SqlParameter ca = new SqlParameter("@cantidad", SqlDbType.Int, int.MaxValue);
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, int.MaxValue);
            es.Value = stock;
            ca.Value = cantidad;
            id.Value = idLibro;

            cmd.Parameters.Add(es);
            cmd.Parameters.Add(ca);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }


        //Restar Stock
        public bool RestarStock(int cantidad, int idLibro)
        {
            int stock = StockLibros(idLibro);
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Libro set Stock=(@stock-@cantidad) where IdLibro=@id;", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter es = new SqlParameter("@stock", SqlDbType.Int, int.MaxValue);
            SqlParameter ca = new SqlParameter("@cantidad", SqlDbType.Int, int.MaxValue);
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, int.MaxValue);
            es.Value = stock;
            ca.Value = cantidad;
            id.Value = idLibro;

            cmd.Parameters.Add(es);
            cmd.Parameters.Add(ca);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }


        //Cantidad Libros Segun el Usuario
        public int CantidadLibrosU(int idUsuario)
        {
            Connection();
            con.Open();
            int cantidad = 0;
            SqlCommand cmd = new SqlCommand("select case when SUM(Solicitud.CantidadLibros) IS NULL then 0 else SUM(Solicitud.CantidadLibros) end as CantidadLibros from Solicitud inner join Prestamo on Solicitud.IdSolicitud=Prestamo.IdSolicitud where Prestamo.Activo = 1 and Solicitud.IdUsuario=@idUsuario", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idUsuario", SqlDbType.Int, int.MaxValue);
            id.Value = idUsuario;
            cmd.Parameters.Add(id);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    cantidad = (int)rdr["CantidadLibros"];
                    return cantidad;
                }
            }
            else
            {
                return cantidad;
            }
            con.Close();
            return cantidad;
        }


        //Comparar contraseñas al editar un usuario
        public string CompararPassword(int idUsuario, string password)
        {
            Connection();
            con.Open();
            string passw = "";
            SqlCommand cmd = new SqlCommand("select Password from Usuario where IdUsuario = @idUsuario and Password = @password", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idUsuario", SqlDbType.Int, int.MaxValue);
            SqlParameter pass = new SqlParameter("@password", SqlDbType.NVarChar, int.MaxValue);

            id.Value = idUsuario;
            pass.Value = password;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(pass);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    passw = rdr["Password"].ToString();
                    return passw;
                }
            }
            else
            {
                return passw;
            }
            con.Close();
            return passw;
        }


        public string ObtenerCorreo(int idUsuario)
        {
            Connection();
            con.Open();
            string correo = "";
            SqlCommand cmd = new SqlCommand("select CoreoElectronico from Usuario where IdUsuario = @idUsuario", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idUsuario", SqlDbType.Int, int.MaxValue);

            id.Value = idUsuario;

            cmd.Parameters.Add(id);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    correo = rdr["CoreoElectronico"].ToString();
                    return correo;
                }
            }
            else
            {
                return correo;
            }
            con.Close();
            return correo;
        }


        public bool DatosPrestamo(int? idSolicitud)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select Usuario.NombreCompleto, Solicitud.CantidadLibros, Usuario.IdUsuario,Libro.Titulo,Libro.IdLibro,Solicitud.FechaSolicitud from Usuario inner join Solicitud on Usuario.IdUsuario = Solicitud.IdUsuario inner join Libro on Solicitud.IdLibro=Libro.IdLibro where Solicitud.IdSolicitud = @idSolicitud;", con);
            cmd.CommandType = CommandType.Text;
            SqlParameter id = new SqlParameter("@idSolicitud", SqlDbType.Int, int.MaxValue);
            id.Value = idSolicitud;
            cmd.Parameters.Add(id);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    HttpContext.Current.Session["PrestamoNombreCompleto"] = rdr["NombreCompleto"].ToString();
                    HttpContext.Current.Session["PrestamoCantidadLibros"] = rdr["CantidadLibros"].ToString();
                    HttpContext.Current.Session["PrestamoIdUsuario"] = rdr["IdUsuario"].ToString();
                    HttpContext.Current.Session["PrestamoTitulo"] = rdr["Titulo"].ToString();
                    HttpContext.Current.Session["PrestamoIdLibro"] = rdr["IdLibro"].ToString();
                    HttpContext.Current.Session["PrestamoFechaSolicitud"] = rdr["FechaSolicitud"].ToString();

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


        public bool CrearComprobante(Comprobante c)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("crearComprobante", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Comprobante", c.Comprobante1);
            cmd.Parameters.AddWithValue("@Activo", c.Activo);
            cmd.Parameters.AddWithValue("@FechaCreacion", c.FechaCreacion);
            cmd.Parameters.AddWithValue("@FechaVencimiento", c.FechaVencimiento);
            cmd.Parameters.AddWithValue("@IdUsuario", c.IdUsuario);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }


        public int ObtenerIdLibro(int idSolicitud)
        {
            Connection();
            con.Open();
            int idLibro = 0;
            SqlCommand cmd = new SqlCommand("select Libro.IdLibro from Libro inner join Solicitud on Solicitud.IdLibro=Libro.IdLibro inner join Prestamo on Prestamo.IdSolicitud = Solicitud.IdSolicitud  where Solicitud.IdSolicitud = @idSolicitud", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idSolicitud", SqlDbType.Int, int.MaxValue);
            id.Value = idSolicitud;
            cmd.Parameters.Add(id);
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    idLibro = (int)rdr["IdLibro"];
                    return idLibro;
                }
            }
            else
            {
                return idLibro;
            }
            con.Close();
            return idLibro;
        }


        public bool PrestamoDes(int idPrestamo)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Prestamo set Activo = 0 where IdPrestamo = @idPrestamo", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idPrestamo", SqlDbType.Int, int.MaxValue);
            id.Value = idPrestamo;

            cmd.Parameters.Add(id);
            cmd.Prepare();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }


        public bool ComprobanteDes(string comprobante)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Comprobante set Activo=0 where Comprobante=@comprobante", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter c = new SqlParameter("@comprobante", SqlDbType.NVarChar, int.MaxValue);
            c.Value = comprobante;

            cmd.Parameters.Add(c);
            cmd.Prepare();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }


        public bool SolicitudDes(int idSolicitud)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Solicitud set Activo = 0 where IdSolicitud = @idSolicitud", con);
            cmd.CommandType = CommandType.Text;

            SqlParameter id = new SqlParameter("@idSolicitud", SqlDbType.NVarChar, int.MaxValue);
            id.Value = idSolicitud;

            cmd.Parameters.Add(id);
            cmd.Prepare();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }
    }
}

