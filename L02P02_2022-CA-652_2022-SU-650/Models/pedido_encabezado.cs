using System.ComponentModel.DataAnnotations;
using Azure.Core.Pipeline;

namespace L02P02_2022_CA_652_2022_SU_650.Models
{
    public class pedido_encabezado
    {
        [Key]

        public int id { get; set; }

        public int id_cliente { get; set; }

        public int cantidad_libros { get; set; }

        public decimal total { get; set; }
    }
}
