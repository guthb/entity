﻿using System;
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

        public BlogRepository(BlogContext _context)   //only used when we want to connect to a real database
        {
            Context = _context;
        }


        public List<Author> GetAuthors()
        {
            int i = 1;
            return Context.Authors.ToList();
        }

        public void AddAuthor(Author author)
        {
            Context.Authors.Add(author);
            Context.SaveChanges();
        }

        public void AddAuthor(string first_name, string last_name, string penname)
        {
            Author author = new Author { FirstName = first_name, LastName = last_name, PenName = penname };
            Context.Authors.Add(author);
            Context.SaveChanges();
        }

        public Author FindAuthorByPenName(string pen_name)
        {
            //very inefficient
            //select * from Author; gets all the authors.


            // Much faster to use LINQ to generate something like:
            // select * from authors WHERE PenName == pen_name

            Author found_author = Context.Authors.FirstOrDefault(rowinauthortable => rowinauthortable.PenName.ToLower() == pen_name.ToLower());
            return found_author;



            /*
            List<Author> found_authors = Context.Authors.ToList();
            foreach (var author in found_authors)
            {
                if (author.PenName.ToLower() == pen_name.ToLower())
                {
                    return author;
                }

            }
            return null;
            */
        }

        public Author RemoveAuthor(string pen_name)
        {
            Author found_author = FindAuthorByPenName(pen_name);
            if (found_author != null)
            {
                Context.Authors.Remove(found_author);
                Context.SaveChanges();
            }
            return found_author;
        }
    }
}