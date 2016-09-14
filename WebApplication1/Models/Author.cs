using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

       [MaxLength(length: 20, ErrorMessage = "Pen Name can not be larger than 20 Chars")]
        public string PenName { get; set; }

    }
}