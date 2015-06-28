using System;
using System.Collections.Generic;

namespace ControleContasWeb.Data
{
    public class Contas
    {

        public int Id { get; set; }
        public DateTime DataLeitura { get; set; }
        public long NumLeitura { get; set; }
        public long Consumo { get; set; }
        public decimal ValorPagar { get; set; }
        public DateTime DataPagto { get; set; }
        public ContasTipo Tipo { get; set; }

    }
}
