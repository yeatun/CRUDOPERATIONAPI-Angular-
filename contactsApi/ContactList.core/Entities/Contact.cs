using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Core.Entities
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
    }
}
