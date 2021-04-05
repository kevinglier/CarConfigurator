using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarConfigurator.Controllers
{
    public class CarConfiguratorController : Controller
    {
        // GET: CarConfiguratorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CarConfiguratorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarConfiguratorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarConfiguratorController/Create
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

        // GET: CarConfiguratorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarConfiguratorController/Edit/5
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

        // GET: CarConfiguratorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarConfiguratorController/Delete/5
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
