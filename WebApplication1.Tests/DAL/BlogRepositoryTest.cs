using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.DAL;
using System.Collections.Generic;
using WebApplication1.Models;
using Moq;
using System.Data.Entity;
using System.Linq;


namespace WebApplication1.Tests.DAL
{
    [TestClass]
    public class BlogRepositoryTest
    {

        //create Mock BlogContext
        Mock<BlogContext> mock_context  { get; set; }
        Mock<DbSet<Author>> mock_author_table { get; set; }
        List<Author> author_list { get; set; } //fake database
       
        public void ConnectMocksToDatastore()
        {
            var queryable_list = author_list.AsQueryable();
            //lie to Linq  make it think it is a database table
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(queryable_list.GetEnumerator());

            //Have our authors property return our fake database table
            mock_context.Setup(c => c.Authors).Returns(mock_author_table.Object);
        }


    [TestInitialize]  //runs before any test
        public void Initialize()
        {
            //create Mock BlogContext
            mock_context = new Mock<BlogContext>();
            mock_author_table = new Mock<DbSet<Author>>();
            author_list = new List<Author>();  //fake database
            

        }


        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            BlogRepository repo = new BlogRepository();
            Assert.IsNotNull(repo);

        }

        [TestMethod]
        public void RepoEnsureRepoHasContext()
        {
            BlogRepository repo = new BlogRepository();

            BlogContext actual_context = repo.Context;

            Assert.IsInstanceOfType(actual_context, typeof(BlogContext));

        }

        [TestMethod]
        public void RepoEnsureWeHaveNoAuthors()
        {
            //Arrange
            ConnectMocksToDatastore();
            BlogRepository repo = new BlogRepository(mock_context.Object);
            

            //Act
            List<Author> some_authors = repo.GetAuthors();
            int expected_authors_count = 0;
            int actual_authors_count = some_authors.Count;

            //Assert
            Assert.AreEqual(expected_authors_count, actual_authors_count);
        }


        [TestMethod]
        public void RepoEnsureAddAuthorToDatabase()
        {
            //Arrange
            ConnectMocksToDatastore();
            BlogRepository repo = new BlogRepository(mock_context.Object);
            Author my_author = new Author {FirstName= "Sally", LastName ="Mae", PenName = "Voldemort" }; // Property Initilazer

            //Act
            repo.AddAuthor(my_author);

            int actual_author_count = repo.GetAuthors().Count;
            int expected_author_count = 1;

            //Assert
            Assert.AreEqual(expected_author_count, actual_author_count);
        }
        
    }
    
}
