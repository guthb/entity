using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class BlogRepository
    {
        public BlogContext Context { get; set; }

        public BlogRepository()
        {
            Context = new BlogContext();
        }

        public BlogRepository(BlogContext _context)
        {
            Context = _context;
        }


        public List<Author> GetAuthors()
        {
            return Context.Authors.ToList();
        }

        public void AddAuthor(Author my_author)
        {
            throw new NotImplementedException();
        }
    }
}