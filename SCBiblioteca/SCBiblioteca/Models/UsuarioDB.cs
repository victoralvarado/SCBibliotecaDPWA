using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class UsuarioDB
    {
        private SqlConnection con;

        private void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["SCBiblioteca"].ToString();
            con = new SqlConnection(constring);
        }

        public bool Login(String usuario, String password)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("select Rol.Rol from Usuario inner join Rol on Usuario.IdRol = Rol.IdRol where Usuario.Usuario = '" + usuario + "' and Usuario.Password ='" + password + "';", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    HttpContext.Current.Session["UserSesion"] = rdr["Rol"].ToString();
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
        public bool CrearCuenta(Usuario u, string telefono)
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
            cmd.Parameters.AddWithValue("@Password", u.Password);
            cmd.Parameters.AddWithValue("@IdRol", Rol());
            cmd.Parameters.AddWithValue("@Telefono", telefono);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
                return true;
            else
                return false;
        }
    }
}