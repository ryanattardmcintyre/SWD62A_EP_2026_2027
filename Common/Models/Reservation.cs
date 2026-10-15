using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Reservation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Book")] //name of the property of the data type
        public string BookFK { get; set; }
        public Book Book { get; set; }

        public DateTime DateTaken { get; set; }
        public DateTime? DateReturned { get; set; }

        //[ForeignKey("User")];
        public string Username { get; set; } //33FC43FC-4273-41A8-A536-9FB5203D5A71
        //public IdentityUser User {get;set;}
    }
}
