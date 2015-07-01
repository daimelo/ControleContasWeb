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
    public class UsuariosGrupoRepository
    {

        public static UsuariosGrupo GetOneGrupo(int pId)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("SELECT * ");
            sql.Append("FROM usuarios_grupo ");
            sql.Append("WHERE id=@id ");
            sql.Append("ORDER BY id ASC");

            cmd.Parameters.AddWithValue("@id", pId);

            UsuariosGrupo grupo;
            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            dr.Read();
            grupo = new UsuariosGrupo
            {
                Id = (int)dr["id"],
                Grupo = (string)dr["nome"],
            };

            dr.Close();
            return grupo;
        }

        public static List<UsuariosGrupo> GetGrupos()
        {
            StringBuilder sql = new StringBuilder();
            List<UsuariosGrupo> grupos = new List<UsuariosGrupo>();

            sql.Append("SELECT * ");
            sql.Append("FROM usuarios_grupo ");

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());
            while (dr.Read())
            {
                grupos.Add(new UsuariosGrupo { Id = (int)dr["id"], Grupo = (string)dr["nome"] });
            }

            return grupos;
        }

    }
}