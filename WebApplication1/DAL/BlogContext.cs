using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    // inheritance
    public class BlogContext : DbContext
    {

        public virtual DbSet<PublishedWork> Works { get; set; }
        public virtual DbSet<CitationStyle> CitationStyles { get; set; }  //to make seeding easy/
        public virtual DbSet<Author> Authors { get; set; }
    }
}