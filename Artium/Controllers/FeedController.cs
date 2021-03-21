using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Controllers
{
    public class FeedController : Controller
    {
        // GET: FeedController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FeedController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeedController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeedController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeedController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeedController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
