using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace test.Models
{
    public class DBHelper
    {
        public MySqlCommand miCommand;
        public MySqlDataReader Data;
        public MySqlConnection conn;
        public MySqlTransaction tran;

        public void AbrirConParametros(string consulta)
        {
            conn = new MySqlConnection();
            string proveedor = "server=127.0.0.1;Database=test ;Uid=root;Password=mica2511;Port=3306";
            conn.ConnectionString = proveedor;
            conn.Open();
            miCommand = new MySqlCommand(consulta, conn, tran);
            tran = conn.BeginTransaction();
            miCommand = conn.CreateCommand();
            miCommand.CommandType = CommandType.StoredProcedure;
            miCommand.CommandText = consulta;
        }
        public void Abrir(string consulta)
        {
            conn = new MySqlConnection();
            string proveedor = "server=127.0.0.1;Database=test;Uid=root;Password=mica2511;Port=3306";
            conn.ConnectionString = proveedor;
            conn.Open();
            miCommand = conn.CreateCommand();
            miCommand.CommandType = CommandType.StoredProcedure;
            miCommand.CommandText = consulta;
        }
    }
}