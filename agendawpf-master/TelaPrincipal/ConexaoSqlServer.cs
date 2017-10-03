using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class ConexaoSqlServer
{

    private string servidor { get; set; }
    private string banco { get; set; }
    private string usuario { get; set; }
    private string senha { get; set; }
    public string conexao { get; set; }

    public string TestarConexao(string servidor, string banco, string usuario, string senha, ref string strStatus)
    {
        SqlConnection Cnn;

        try
        {
            if (string.IsNullOrEmpty(servidor))
                return "Servidor não foi informada.\n Teste abortado.";
            if (string.IsNullOrEmpty(banco))
                return "Tabela não foi informada.\n Teste abortado.";
            if (string.IsNullOrEmpty(usuario))
                return "Usuário MSSQL não foi informado.\n Teste abortado.";
            if (string.IsNullOrEmpty(senha))
                return "Senha MSSQL não foi informada.\n Teste abortado.";

        }
        catch (Exception e)
        {
            strStatus = "NOK";
            return "Ocorreu um erro na verificação dos dados!\n" + e.ToString();
        }

        try
        {
            Cnn = new SqlConnection("Server=" + servidor +
                                    ";Database=" + banco +
                                    ";User Id=" + usuario +
                                    ";Password=" + senha);
            Cnn.Open();
            SqlConnection.ClearAllPools();
            Cnn.Close();
            Cnn = null;
            strStatus = "OK";
            return "Teste de conexão com o banco de dados realizado com sucesso.";
        }
        catch (SqlException e)
        {
            strStatus = "NOK";
            return "Teste de conexão com o banco de dados falhou.\n" + e.ToString();
        }
        catch (Exception e)
        {
            strStatus = "NOK";
            return "Teste de conexão com o banco de dados falhou.\n" + e.ToString();
        }
    }

    public DataTable Consulta(string query)
    {
        try
        {
            using (SqlConnection conexao = new SqlConnection(this.conexao))
            {
                SqlCommand comando = new SqlCommand(query, conexao);
                comando.CommandType = CommandType.Text;
                conexao.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataSet ds = new DataSet("resultado");
                    adaptador.Fill(ds);
                    conexao.Close();
                    return ds.Tables[0];
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void Atualiza(string query)
    {
        try
        {
            using (SqlConnection conexao = new SqlConnection(this.conexao))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.CommandType = CommandType.Text;
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private static void SqlCommandPrepareEx(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(null, connection);

            // Create and prepare an SQL statement.
            command.CommandText =
                "INSERT INTO Region (RegionID, RegionDescription) " +
                "VALUES (@id, @desc)";
            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int, 0);
            SqlParameter descParam =
                new SqlParameter("@desc", SqlDbType.Text, 100);
            idParam.Value = 20;
            descParam.Value = "First Region";
            command.Parameters.Add(idParam);
            command.Parameters.Add(descParam);

            // Call Prepare after setting the Commandtext and Parameters.
            command.Prepare();
            command.ExecuteNonQuery();

            // Change parameter values and call ExecuteNonQuery.
            command.Parameters[0].Value = 21;
            command.Parameters[1].Value = "Second Region";
            command.ExecuteNonQuery();
        }
    }

}


