using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    //Models will be the data containers/ define the data
    //Models will be used to design the database
    public class Book
    {
        [Key]
        [StringLength(17)] //=> this is a way how to define constraints that will be created in db
        public string Isbn { get; set; } //978-0-13-409341-3
        
        [Required(AllowEmptyStrings =false, ErrorMessage ="Cannot be left blank")]
        public string Title { get; set; }
        public int PublishedYear { get; set; }
        public string? Author { get; set; } //? = allow null will be set in db

        //foreign key
        [ForeignKey("Category")]
        public int CategoryFK { get; set; }
        
        //navigational property
        public Category Category { get; set; }

        //from b in context.Books
        //select b.Isbn, b.Title, b.Category.Name
        public bool Available { get; set; }
    }
}
