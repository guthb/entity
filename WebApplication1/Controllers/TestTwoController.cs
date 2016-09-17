using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class TestTwoController : Controller
    {
        // GET: TestTwo
        public ActionResult Index()
        {
            return View();
        }

        // GET: TestTwo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestTwo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestTwo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TestTwo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestTwo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TestTwo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestTwo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
