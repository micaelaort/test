using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class Usuarios
    {
     
        public string user { get; set; }

        public string contraseña { get; set; }
        
        public string apellido { get; set; }

        
        public string nombre { get; set; }

        public string email { get; set; }

        
        public string confirmaciondecontraseña { get; set; }

        public int id { get; set; }
        public string  Administrador { get; set; }
        DBHelper help;
        string consulta;
        Usuarios UsuarioBaseDatos;

        List<Usuarios> lista = new List<Usuarios>();
        public Usuarios login(Usuarios user)
        {
            UsuarioBaseDatos = new Usuarios();
            help = new DBHelper();
            consulta = "SeleccionarUsuarioLogin";
            help.AbrirConParametros(consulta);


            MySqlParameter parametro1 = new MySqlParameter("PUser", user.user);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PPassword", user.contraseña);
            help.miCommand.Parameters.Add(parametro2);

            MySqlDataReader lector = help.miCommand.ExecuteReader();

            if (lector.Read())
            {
                UsuarioBaseDatos = new Usuarios();
                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                UsuarioBaseDatos.user = lector["usuario"].ToString();
                UsuarioBaseDatos.contraseña = lector["password"].ToString();
                UsuarioBaseDatos.Administrador = lector["admin"].ToString();
         
                
            }
            else
            {
                UsuarioBaseDatos = new Usuarios();
                UsuarioBaseDatos.user = "";
                UsuarioBaseDatos.contraseña = "";
                UsuarioBaseDatos.Administrador = "0";
                UsuarioBaseDatos.id = 0;
            }
            help.conn.Close();
            return UsuarioBaseDatos;


        }

        public void registrar(Usuarios ouser, string consulta)
        {
             help = new DBHelper();
            help.AbrirConParametros(consulta);
            

            MySqlParameter parametro1 = new MySqlParameter("PNombre", ouser.nombre);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PApellido", ouser.apellido);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PUser", ouser.user);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PEmail", ouser.email);
            help.miCommand.Parameters.Add(parametro4);

            MySqlParameter parametro5 = new MySqlParameter("PPassword", ouser.contraseña);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro6 = new MySqlParameter("PAdmin", "0");
            help.miCommand.Parameters.Add(parametro6);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }
        public Boolean ValidarContraseña(Usuarios ouser, Usuarios ouser2)
        {
            if (ouser2.contraseña == ouser.contraseña && ouser2.contraseña != " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean ValidarUsuario(Usuarios ouser, Usuarios ouser2)
        {
            if (ouser2.user == ouser.user && ouser2.user != " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean ValidarAdmin(Usuarios ouser)
        {
            //Si return true es admin
            if (ouser.Administrador =="1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CompararContraseña(Usuarios ouser)
        {
            if (ouser.contraseña == ouser.confirmaciondecontraseña)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public List<Usuarios> SeleccionarUsuarios()
        {
            UsuarioBaseDatos = new Usuarios();
            help = new DBHelper();
            consulta = "SeleccionarUsuarios";
            help.Abrir(consulta);


           MySqlDataReader lector = help.miCommand.ExecuteReader();

            while(lector.Read())
            {
                UsuarioBaseDatos = new Usuarios();
                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                UsuarioBaseDatos.user = lector["usuario"].ToString();
                UsuarioBaseDatos.contraseña = lector["password"].ToString();
                UsuarioBaseDatos.Administrador = lector["admin"].ToString();
                UsuarioBaseDatos.nombre = lector["nombre"].ToString();
                UsuarioBaseDatos.apellido = lector["apellido"].ToString();
                UsuarioBaseDatos.email = lector["email"].ToString();
                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                lista.Add(UsuarioBaseDatos);
            }
            help.conn.Close();
            return lista;


        }

        public void Modificar(Usuarios ouser)
        {
            consulta = "ModificarUsuario";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PNombre", ouser.nombre);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PApellido", ouser.apellido);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PUser", ouser.user);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PEmail", ouser.email);
            help.miCommand.Parameters.Add(parametro4);

            MySqlParameter parametro5 = new MySqlParameter("PPassword", ouser.contraseña);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro10 = new MySqlParameter("PID", ouser.id);
            help.miCommand.Parameters.Add(parametro10);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();

        }

        public void EliminarUsuario( int id)
        {
            consulta = "EliminarUsuario";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro10 = new MySqlParameter("idUser", id);
            help.miCommand.Parameters.Add(parametro10);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();

        }

        public Usuarios TraerUsuario(int id)
        {

            UsuarioBaseDatos = new Usuarios();
            help = new DBHelper();
            consulta = "SeleccionarUsuario";
            help.Abrir(consulta);
            MySqlParameter parametro10 = new MySqlParameter("PUser", id);
            help.miCommand.Parameters.Add(parametro10);

            MySqlDataReader lector = help.miCommand.ExecuteReader();

            while (lector.Read())
            {
                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                UsuarioBaseDatos.user = lector["usuario"].ToString();
                UsuarioBaseDatos.contraseña = lector["password"].ToString();
                UsuarioBaseDatos.Administrador = lector["admin"].ToString();
                UsuarioBaseDatos.nombre = lector["nombre"].ToString();
                UsuarioBaseDatos.apellido = lector["apellido"].ToString();
                UsuarioBaseDatos.email = lector["email"].ToString();
                

            }
            help.conn.Close();
            return UsuarioBaseDatos;

        }





    }
}