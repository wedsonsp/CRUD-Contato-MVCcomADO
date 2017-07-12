using ContatoMVCcomADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContatoMVCcomADO.Repositorio
{
    public class ContatoRepositorio
    {
        private SqlConnection _con;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["stringConexao"].ToString();
            _con = new SqlConnection(constr);

        }

        //Adicionar um time
        public bool AdicionarContato(Contato contatoObj)
        {
            Connection();

            //Variável para contar os registros.
            int i;

            //Comando para executar a querie.
            using (SqlCommand command = new SqlCommand("IncluirContato", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                //Recebendo o Time como parâmetro para adicionar os campos. 
                command.Parameters.AddWithValue("@Nome", contatoObj.Nome);
                command.Parameters.AddWithValue("@Telefone", contatoObj.Telefone);
                command.Parameters.AddWithValue("@Email", contatoObj.Email);

                //Abrindo a conexão
                _con.Open();

                //A linha abaixo retorna quantas linhas foram afetadas.
                i = command.ExecuteNonQuery();

                contatoObj.Nome = "";
                contatoObj.Telefone = "";
                contatoObj.Email = "";
            }

            _con.Close();
            //If ternário
            //Se o retorno for maior do que 1, então será true.
            return i >= 1;

            

        }

        //Obter todos os Contato.
        public List<Contato> ObterContato()
        {
            Connection();

            //Criar uma lista de Contato.
            List<Contato> ContatoList = new List<Contato>();

            using (SqlCommand command = new SqlCommand("ObterContato", _con))
            {
                command.CommandType = CommandType.StoredProcedure;

                _con.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Contato time = new Contato()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = Convert.ToString(reader["Nome"]),
                        Telefone = Convert.ToString(reader["Telefone"]),
                        Email = Convert.ToString(reader["Email"])
                    };

                    //Adicionando o objeto time dentro do objeto ContatoList da lista criada.
                    ContatoList.Add(time);

                }

                //Fechar a conexão
                _con.Close();

                //Retorna a lista.
                return ContatoList;
            }
        }

        //Atualizar Time
        public bool AtualizarContato(Contato timeObj)
        {
            Connection();

            //Variável para contar os registros.
            int i;

            //Comando para executar a querie.
            using (SqlCommand command = new SqlCommand("AtualizarContato", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                //Recebendo o Time como parâmetro para adicionar os campos.
                command.Parameters.AddWithValue("@Id", timeObj.Id);
                command.Parameters.AddWithValue("@Nome", timeObj.Nome);
                command.Parameters.AddWithValue("@Telefone", timeObj.Telefone);
                command.Parameters.AddWithValue("@Email", timeObj.Email);

                //Abrindo a conexão-----------------
                _con.Open();

                //A linha abaixo retorna quantas linhas foram afetadas.
                i = command.ExecuteNonQuery();
            }

            _con.Close();
            //If ternário
            //Se o retorno for maior do que 1, então será true.
            return i >= 1;
        }

        //Excluir Time
        public bool ExcluirTime(int id)
        {
            Connection();

            //Variável para contar os registros.
            int i;

            //Comando para executar a querie.
            using (SqlCommand command = new SqlCommand("ExcluirContatoPorId", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                //Recebendo o Time como parâmetro para adicionar os campos.
                command.Parameters.AddWithValue("@Id", id);

                //Abrindo a conexão
                _con.Open();

                //A linha abaixo retorna quantas linhas foram afetadas.
                i = command.ExecuteNonQuery();
            }

            _con.Close();

            if (i >= 1)
            {
                return true;
            }

            return false;
        }
    }
}