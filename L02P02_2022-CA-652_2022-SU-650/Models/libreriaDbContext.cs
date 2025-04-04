using Microsoft.EntityFrameworkCore;

namespace L02P02_2022_CA_652_2022_SU_650.Models
{
    public class libreriaDbContext : DbContext
    {
        public libreriaDbContext(DbContextOptions options) : base(options) 
        {
        }
    }
}
