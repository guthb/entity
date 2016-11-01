using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PublishedWork
    {
        [Key]
        public int PublishedWorkId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DatePublished { get; set; }
        // shows the FK relationship  Property that has a Type that matches a class  HAS A
        [Required]
        public CitationStyle Citation { get; set; }   //something that referneces a class , navigation properties

        // many relationship
    
        //public ICollection<Author> Authors { get; set; }  -new term
        public List<Author> Authors { get; set; }  //concrete type used instead as it behaves the same way as ^^



    }
}