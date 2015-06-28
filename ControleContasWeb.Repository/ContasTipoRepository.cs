using ConnectionClass.MySql;
using ControleContasWeb.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleContasWeb.Repository
{
    public class ContasTipoRepository
    {

        public static ContasTipo GetOneTipo(int pId)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("SELECT * ");
            sql.Append("FROM contas_tipo ");
            sql.Append("WHERE id=@id ");
            sql.Append("ORDER BY id ASC");

            cmd.Parameters.AddWithValue("@id", pId);

            ContasTipo tipo;
            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            dr.Read();
            tipo = new ContasTipo
            {
                Id = (int)dr["id"],
                Tipo = (string)dr["nome"],
            };

            dr.Close();
            return tipo;
        }

        public static List<ContasTipo> GetTipos()
        {
            StringBuilder sql = new StringBuilder();
            List<ContasTipo> tipos = new List<ContasTipo>();

            sql.Append("SELECT * ");
            sql.Append("FROM contas_tipo ");

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());
            while (dr.Read())
            {
                tipos.Add(new ContasTipo { Tipo = (string)dr["nome"] });
            }

            return tipos;
        }

        public void CreateTipo(ContasTipo pTipo)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("INSERT INTO contas_tipo (id, nome) ");
            sql.Append("VALUES(@id, @nome)");

            cmd.Parameters.AddWithValue("@id", pTipo.Id);
            cmd.Parameters.AddWithValue("@nome", pTipo.Tipo);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

        public void Delete(int pId)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("DELETE FROM contas_tipo ");
            sql.Append("WHERE id=@id");

            cmd.Parameters.AddWithValue("@id", pId);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

        public void Update(ContasTipo pTipo)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("UPDATE contas_tipo ");
            sql.Append("SET id=@id, nome=@nome ");
            sql.Append("WHERE id=" + pTipo.Id);


            cmd.Parameters.AddWithValue("@id", pTipo.Id);
            cmd.Parameters.AddWithValue("@nome", pTipo.Tipo);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

    }
}
