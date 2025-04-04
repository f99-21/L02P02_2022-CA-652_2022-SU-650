using System.ComponentModel.DataAnnotations;

namespace L02P02_2022_CA_652_2022_SU_650.Models
{
    public class autores
    {
        [Key]

        public int id { get; set; }
        
        public string autor {  get; set; }
    }
}
