using ClienteCore.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClienteCore.Models
{
    public class ClienteDAL : IClienteDAL
    {
        string connection = new DataSettingsManager().LoadSettings().DataConnectionString;
        //string connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Clientes;User ID=sa;pwd=s@ntos1992;Integrated Security=True;";

        public IEnumerable<Cliente> GetAllClientes()
        {
            List<Cliente> lstcliente = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                string comandoSQL = @"SELECT [IdCliente]
                                                        ,[NomeCliente]
                                                        ,[EmailCliente]
                                                        ,[CpfCliente]
                                                        ,[DtNascCliente]
                                                        ,[Ativo] 
                                                    FROM Cliente
                                                   WHERE [Ativo] = 1";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    cliente.NomeCliente = rdr["NomeCliente"].ToString();
                    cliente.EmailCliente = rdr["EmailCliente"].ToString();
                    cliente.CpfCliente = rdr["CpfCliente"].ToString();
                    cliente.DtNascCliente = Convert.ToDateTime(rdr["DtNascCliente"]);
                    cliente.Ativo = Convert.ToBoolean(rdr["Ativo"]);
                    lstcliente.Add(cliente);
                }
                con.Close();
            }
            return lstcliente;
        }
        public void AddCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string comandoSQL = @"INSERT INTO Cliente ([NomeCliente]
                                                           ,[EmailCliente]
                                                           ,[CpfCliente]
                                                           ,[DtNascCliente]
                                                           ,[Ativo]) 
                                                   VALUES (@NomeCliente
                                                          ,@EmailCliente
                                                          ,@CpfCliente
                                                          ,@DtNascCliente
                                                          ,@Ativo)";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@NomeCliente", cliente.NomeCliente);
                cmd.Parameters.AddWithValue("@EmailCliente", cliente.EmailCliente);
                cmd.Parameters.AddWithValue("@CpfCliente", cliente.CpfCliente);
                cmd.Parameters.AddWithValue("@DtNascCliente", cliente.DtNascCliente);
                cmd.Parameters.AddWithValue("@Ativo", 1);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string comandoSQL = @"UPDATE Cliente SET [NomeCliente] = @NomeCliente
                                                         ,[EmailCliente] = @EmailCliente
                                                         ,[CpfCliente] = @CpfCliente
                                                         ,[DtNascCliente] = @DtNascCliente
                                                    WHERE [IdCliente] = @IdCliente";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@NomeCliente", cliente.NomeCliente);
                cmd.Parameters.AddWithValue("@EmailCliente", cliente.EmailCliente);
                cmd.Parameters.AddWithValue("@CpfCliente", cliente.CpfCliente);
                cmd.Parameters.AddWithValue("@DtNascCliente", cliente.DtNascCliente);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Cliente GetCliente(int? id)
        {
            Cliente cliente = new Cliente();
            using (SqlConnection con = new SqlConnection(connection))
            {
                string sqlQuery = "SELECT * FROM Cliente WHERE IdCliente= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cliente.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    cliente.NomeCliente = rdr["NomeCliente"].ToString();
                    cliente.EmailCliente = rdr["EmailCliente"].ToString();
                    cliente.CpfCliente = rdr["CpfCliente"].ToString();
                    cliente.DtNascCliente = Convert.ToDateTime(rdr["DtNascCliente"]);
                    cliente.Ativo = Convert.ToBoolean(rdr["Ativo"]);
                }
            }
            return cliente;
        }
        public void DeleteCliente(int? id)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string comandoSQL = @"UPDATE Cliente SET [Ativo] = 0
                                                   WHERE [IdCliente] = @IdCliente";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdCliente", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}