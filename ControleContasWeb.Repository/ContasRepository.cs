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
    public class ContasRepository
    {

        public static Contas GetOne(int pId, int pIdUsuario)
        {
            StringBuilder sql = new StringBuilder();
            Contas conta = new Contas();

            sql.Append("SELECT c.*, t.id as id_tipo, t.nome as nome_tipo ");
            sql.Append("FROM contas c ");
            sql.Append("INNER JOIN contas_tipo t ");
            sql.Append("ON c.id_tipo=t.id ");
            sql.Append("WHERE c.id=" + pId + " AND c.id_usuario='" + pIdUsuario +"'");

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            while (dr.Read())
            {
                conta.Id = (int)dr["id"];
                conta.DataLeitura = (DateTime)dr["data_leitura"];
                conta.NumLeitura = (long)dr["num_leitura"];
                conta.Consumo = (long)dr["consumo"];
                conta.ValorPagar = (decimal)dr["valor_pagar"];
                conta.DataPagto = (DateTime)dr["data_pagto"];
                conta.Tipo = new ContasTipo
                {
                    Id = (int)dr["id_tipo"],
                    Tipo = (string)dr["nome_tipo"],
                };
                conta.IdUsuario = (int)dr["id_usuario"];
            }
            return conta;
        }

        public static List<Contas> GetAll(int pIdUsuario)
        {
            StringBuilder sql = new StringBuilder();
            List<Contas> contas = new List<Contas>();

            sql.Append("SELECT c.*, t.id as id_tipo, t.nome as nome_tipo ");
            sql.Append("FROM contas c ");
            sql.Append("INNER JOIN contas_tipo t ");
            sql.Append("ON c.id_tipo=t.id ");
            sql.Append("WHERE c.id_usuario='"+pIdUsuario+"' ");
            sql.Append("ORDER BY c.data_leitura ASC");

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            while (dr.Read())
            {
                contas.Add(
                    new Contas
                    {
                        Id = (int)dr["id"],
                        DataLeitura = (DateTime)dr["data_leitura"],
                        NumLeitura = (long)dr["num_leitura"],
                        Consumo = (long)dr["consumo"],
                        ValorPagar = (decimal)dr["valor_pagar"],
                        DataPagto = (DateTime)dr["data_pagto"],
                        Tipo = new ContasTipo
                        {
                            Id = (int)dr["id_tipo"],
                            Tipo = (string)dr["nome_tipo"],
                        },
                        IdUsuario = (int)dr["id_usuario"]
                    });
            }

            return contas;
        }

        public static List<Contas> GetBySearch(Int32 IdUsuario, DateTime dataInicial, DateTime dataFim)
        {
            StringBuilder sql = new StringBuilder();
            List<Contas> contas = new List<Contas>();

            sql.Append("SELECT c.*, t.id as id_tipo, t.nome as nome_tipo ");
            sql.Append("FROM contas c ");
            sql.Append("INNER JOIN contas_tipo t ");
            sql.Append("ON c.id_tipo=t.id ");
            sql.Append("WHERE c.id_usuario='" + IdUsuario + "' AND c.data_leitura BETWEEN '" + Convert.ToDateTime(dataInicial).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(dataFim).ToString("yyyy/MM/dd") + "'");
            sql.Append("ORDER BY c.data_leitura ASC");

            MySqlDataReader dr = ConnControleContas.Get(sql.ToString());

            while (dr.Read())
            {
                contas.Add(
                    new Contas
                    {
                        Id = (int)dr["id"],
                        DataLeitura = (DateTime)dr["data_leitura"],
                        NumLeitura = (long)dr["num_leitura"],
                        Consumo = (long)dr["consumo"],
                        ValorPagar = (decimal)dr["valor_pagar"],
                        DataPagto = (DateTime)dr["data_pagto"],
                        Tipo = new ContasTipo
                        {
                            Id = (int)dr["id_tipo"],
                            Tipo = (string)dr["nome_tipo"],
                        },
                        IdUsuario = (int)dr["id_usuario"],
                    });
            }

            return contas;
        }

        public void Create(Contas pConta)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("INSERT INTO contas (data_leitura, num_leitura, consumo, valor_pagar, data_pagto, id_tipo, id_usuario)");
            sql.Append("VALUES(@data_leitura, @num_leitura, @consumo, @valor_pagar, @data_pagto, @id_tipo, @id_usuario)");

            cmd.Parameters.AddWithValue("@data_leitura", Convert.ToDateTime(pConta.DataLeitura).ToString("yyyy/MM/dd"));
            cmd.Parameters.AddWithValue("@num_leitura", pConta.NumLeitura);
            cmd.Parameters.AddWithValue("@consumo", pConta.Consumo);
            cmd.Parameters.AddWithValue("@valor_pagar", pConta.ValorPagar);
            cmd.Parameters.AddWithValue("@data_pagto", Convert.ToDateTime(pConta.DataPagto).ToString("yyyy/MM/dd"));
            cmd.Parameters.AddWithValue("@id_tipo", pConta.Tipo.Id);
            cmd.Parameters.AddWithValue("@id_usuario", pConta.IdUsuario);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

        public void Update(Contas pConta)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("UPDATE contas SET data_leitura=@data_leitura, num_leitura=@num_leitura, consumo=@consumo, valor_pagar=@valor_pagar, data_pagto=@data_pagto, id_tipo=@id_tipo, id_usuario=@id_usuario ");
            sql.Append("WHERE id=" + pConta.Id);

            cmd.Parameters.AddWithValue("@data_leitura", Convert.ToDateTime(pConta.DataLeitura).ToString("yyyy/MM/dd"));
            cmd.Parameters.AddWithValue("@num_leitura", pConta.NumLeitura);
            cmd.Parameters.AddWithValue("@consumo", pConta.Consumo);
            cmd.Parameters.AddWithValue("@valor_pagar", pConta.ValorPagar);
            cmd.Parameters.AddWithValue("@data_pagto", Convert.ToDateTime(pConta.DataPagto).ToString("yyyy/MM/dd"));
            cmd.Parameters.AddWithValue("@id_tipo", pConta.Tipo.Id);
            cmd.Parameters.AddWithValue("@id_usuario", pConta.IdUsuario);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

        public void Delete(int pId)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmd = new MySqlCommand();

            sql.Append("DELETE FROM contas ");
            sql.Append("WHERE id=" + pId);

            cmd.CommandText = sql.ToString();
            ConnControleContas.CommandPersist(cmd);
        }

    }
}
