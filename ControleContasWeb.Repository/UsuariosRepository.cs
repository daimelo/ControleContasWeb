using ConnectionClass.MySql;
using ControleContasWeb.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ControleContasWeb.Repository
{
    public class UsuariosRepository
    {

        public static Usuarios GetOne(int pId)
        {
            StringBuilder sql = new StringBuilder();
            Usuarios usuario = new Usuarios();

            sql.Append("SELECT u.*, g.nome ");
            sql.Append("FROM usuarios u ");
            sql.Append("INNER JOIN usuarios_grupo g ");
            sql.Append("ON u.id_grupo=g.id ");
            sql.Append("WHERE u.id" + pId);

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            while (dr.Read())
            {
                usuario.Id = (int)dr["id"];
                usuario.Nome = (string)dr["nome"];
                usuario.Email = (string)dr["email"];
                usuario.Senha = (string)dr["senha"];
                usuario.Grupo = new UsuariosGrupo
                {
                    Id = (int)dr["id"],
                    Grupo = (string)dr["nome"],
                };
            }
            return usuario;
        }

        public Usuarios GetByEmail(string pEmail)
        {
            StringBuilder sql = new StringBuilder();
            Usuarios usuario = new Usuarios();

            sql.Append("SELECT u.*, g.id as grupo_id, g.nome as grupo_nome ");
            sql.Append("FROM usuarios u ");
            sql.Append("INNER JOIN usuarios_grupo g ");
            sql.Append("ON u.id_grupo=g.id ");
            sql.Append("WHERE u.email='" + pEmail+"'");

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            while (dr.Read())
            {
                usuario.Id = (int)dr["id"];
                usuario.Nome = (string)dr["nome"];
                usuario.Email = (string)dr["email"];
                usuario.Senha = (string)dr["senha"];
                usuario.Grupo = new UsuariosGrupo
                {
                    Id = (int)dr["grupo_id"],
                    Grupo = (string)dr["grupo_nome"],
                };
            }
            return usuario;
        }

        public static List<Usuarios> GetAll()
        {
            StringBuilder sql = new StringBuilder();
            List<Usuarios> usuarios = new List<Usuarios>();

            sql.Append("SELECT u.*, g.nome ");
            sql.Append("FROM usuarios u ");
            sql.Append("INNER JOIN usuarios_grupo g ");
            sql.Append("ON u.id_grupo=g.id ");
            sql.Append("ORDER BY u.nome ASC");

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            while (dr.Read())
            {
                usuarios.Add(
                    new Usuarios
                    {
                        Id = (int)dr["id"],
                        Nome = (string)dr["nome"],
                        Email = (string)dr["email"],
                        Senha = (string)dr["senha"],
                        Grupo = new UsuariosGrupo
                        {
                            Id = (int)dr["id"],
                            Grupo = (string)dr["nome"],
                        }
                    });
            }

            return usuarios;
        }

        public void Create(Usuarios pUsuario)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("INSERT INTO usuarios (nome, email, senha, id_grupo) ");
            sql.Append("VALUES(@nome, @email, @senha, @id_grupo)");

            cmd.Parameters.AddWithValue("@nome", pUsuario.Nome);
            cmd.Parameters.AddWithValue("@email", pUsuario.Email);
            cmd.Parameters.AddWithValue("@senha", pUsuario.Senha);
            cmd.Parameters.AddWithValue("@id_grupo", pUsuario.Grupo);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

        public void Update(Usuarios pUsuario)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("UPDATE usuarios SET nome=@nome, email=@email, senha=@senha, id_grupo=@id_grupo ");
            sql.Append("WHERE id=" + pUsuario.Id);

            cmd.Parameters.AddWithValue("@nome", pUsuario.Nome);
            cmd.Parameters.AddWithValue("@email", pUsuario.Email);
            cmd.Parameters.AddWithValue("@senha", pUsuario.Senha);
            cmd.Parameters.AddWithValue("@id_tipo", pUsuario.Grupo);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

        public void Delete(int pId)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("DELETE FROM usuarios ");
            sql.Append("WHERE id=" + pId);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

        public bool Valida(Usuarios usuario)
        {
            string sql = "SELECT * FROM usuarios WHERE email='" + usuario.Email + "' AND senha='" + usuario.Senha + "'";

            MySqlDataReader dr;
            dr = ConnControleContas.Get(sql);

            if (dr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }         
        }

    }
}
