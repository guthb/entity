﻿using System;
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
        BlogRepository repo { get; set; }

       
        public void ConnectMocksToDatastore()
        {
            var queryable_list = author_list.AsQueryable();
            //lie to Linq  make it think it is a database table
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            //mock_author_table.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(queryable_list.GetEnumerator());
            mock_author_table.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            //Have our authors property return our fake database table
            mock_context.Setup(c => c.Authors).Returns(mock_author_table.Object);

            // how to define a callback in response to a called method
            mock_author_table.Setup(t => t.Add(It.IsAny<Author>())).Callback((Author a) => author_list.Add(a));
            mock_author_table.Setup(t => t.Remove(It.IsAny <Author>())).Callback((Author a) => author_list.Remove(a));
        }


    [TestInitialize]  //runs before any test
        public void Initialize()
        {
            //create Mock BlogContext
            mock_context = new Mock<BlogContext>();
            mock_author_table = new Mock<DbSet<Author>>();
            author_list = new List<Author>();  //fake database
            repo = new BlogRepository(mock_context.Object);

            ConnectMocksToDatastore();



        }

        [TestCleanup] //runs after every test
        public void TearDown()
        {
            repo = null;  // 
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
            //ConnectMocksToDatastore();
           // BlogRepository repo = new BlogRepository(mock_context.Object);
            

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
           // ConnectMocksToDatastore();
            //BlogRepository repo = new BlogRepository(mock_context.Object);
            Author my_author = new Author {FirstName= "Sally", LastName ="Mae", PenName = "Voldemort" }; // Property Initilazer

            //Act
            repo.AddAuthor(my_author);

            int actual_author_count = repo.GetAuthors().Count;
            int expected_author_count = 1;

            //Assert
            Assert.AreEqual(expected_author_count, actual_author_count);
        }


        [TestMethod]
        public void RepoEnsureAddAuthorWithArgs()
        {
            // Arange
            //ConnectMocksToDatastore();
            //BlogRepository repo = new BlogRepository(mock_context.Object);

            // Act
            repo.AddAuthor("Sally", "Mae", "Voldemort");

            // Assert();
            List<Author> actual_authors = repo.GetAuthors();
            string actual_author_penname = actual_authors.First().PenName;
            string expected_author_penname = "Voldemort";

            Assert.AreEqual(expected_author_penname, actual_author_penname);

        }

        [TestMethod]
        public void RepoEnsureFindAuthorByPenName()
        {
            //Arrange
            author_list.Add(new Author { AuthorId = 1, FirstName = "Sally", LastName = "Mae", PenName = "Voldemort" });
            author_list.Add(new Author { AuthorId = 2, FirstName = "Tim", LastName = "James", PenName = "Tim" });
            author_list.Add(new Author { AuthorId = 3, FirstName = "Golden State", LastName = "Warriors", PenName = "gsw" });
   

            //BlogRepository repo = new BlogRepository(mock_context.Object);
            //ConnectMocksToDatastore();

            //Act
            string pen_name = "voldemort";
            Author actual_author = repo.FindAuthorByPenName(pen_name);

            // Assert
            int expected_author_id = 1;
            int actual_author_id = actual_author.AuthorId;
            Assert.AreEqual(expected_author_id, actual_author_id);
            
        }

        [TestMethod]
        public void RepoEnsureIcanRemoveAuthor()
        {
            //Arrange
            author_list.Add(new Author { AuthorId = 1, FirstName = "Sally", LastName = "Mae", PenName = "Voldemort" });
            author_list.Add(new Author { AuthorId = 2, FirstName = "Time", LastName = "James", PenName = "Tim" });
            author_list.Add(new Author { AuthorId = 3, FirstName = "Golden State", LastName = "Warriors", PenName = "gsw" });

            //BlogRepository repo = new BlogRepository(mock_context.Object);
            //ConnectMocksToDatastore();

            //Act
            string pen_name = "tim";
            Author removed_author = repo.RemoveAuthor(pen_name);
            int expected_author_count = 2;
            int actual_author_count = repo.GetAuthors().Count;
            int expected_author_id = 2;
            int actual_author_id = removed_author.AuthorId;


            //Assert
            Assert.AreEqual(expected_author_count, actual_author_count);
            Assert.AreEqual(expected_author_id, actual_author_id);

        }

        [TestMethod]
        public void RepoEnsureICanNotRemoveThingsNotThere()
        {

            //Arrange
            author_list.Add(new Author { AuthorId = 1, FirstName = "Sally", LastName = "Mae", PenName = "Voldemort" });
            author_list.Add(new Author { AuthorId = 2, FirstName = "Time", LastName = "James", PenName = "Tim" });
            author_list.Add(new Author { AuthorId = 3, FirstName = "Golden State", LastName = "Warriors", PenName = "gsw" });

           // BlogRepository repo = new BlogRepository(mock_context.Object);
           // ConnectMocksToDatastore();

            //Act
            string pen_name = "harry ";
            Author removed_author = repo.RemoveAuthor(pen_name);


            //Assert
            Assert.IsNull(removed_author);
            

        }


    

    }
    
}
