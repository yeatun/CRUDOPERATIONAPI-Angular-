using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Core.Entities
{
    public class SuperVillain
    {
        [Key]
       /* public int Id { get; set; }*/
        public string? VillainName { get; set; }
        public string? Franchise { get; set; }
        public string? Powers { get; set; }
        public string? ImageURL { get; set; }
    }
}
