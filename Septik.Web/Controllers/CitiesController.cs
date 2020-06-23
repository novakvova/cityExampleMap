using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Septik.Web.Data;
using Septik.Web.Data.Entities;
using Septik.Web.Models;

namespace Septik.Web.Controllers
{
    public class CitiesController : Controller
    {
        // Create a field to store the mapper object
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        // Assign the object in the constructor for dependency injection
        public CitiesController(IMapper mapper, 
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        // GET: CitiesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CitiesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CityAddEditVM cityAddEdit, IFormFile file)
        {
            try
            {
                string extension;
                extension = Path.GetExtension(file.FileName);
                //string f
                string name = Path.GetRandomFileName()+ extension;
                var path = Path.Combine(
                 Directory.GetCurrentDirectory(), "Uploads", name);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var entity = _mapper.Map<City>(cityAddEdit);
                entity.Image = name;
                _context.Cities.Add(entity);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(cityAddEdit);
            }
        }

        // GET: CitiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CitiesController/Edit/5
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

        // GET: CitiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CitiesController/Delete/5
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
