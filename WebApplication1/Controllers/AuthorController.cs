using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {

        private BlogRepository repo = new BlogRepository();
        
        // GET: Author
        public ActionResult Index()
        {
            List<Author> list_of_authors = repo.GetAuthors();
            return View();
        }

        //adding action for users pen name example
        // /Name/Action?penname=gsw

        public ActionResult PenUsed(string penname)
        {
            return View();
        }


    }
}