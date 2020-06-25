using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Septik.Web.Data;
using Septik.Web.Data.Entities;
using Septik.Web.Helpers;
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
            var cities = _context.Cities.Include(c=>c.CityImages);
                //Select(c=> new CityItemVM
                //{
                //    Id=c.Id,
                //    Image="/images/"+c.CityImages.First().Name,
                //    Name=c.Name
                //})
                //.ToList(); 
            var dto = _mapper.Map<IEnumerable<CityItemVM>>(cities);
            return View(dto);
        }

        // GET: CitiesController/Details/5
        public ActionResult Details(int id)
        {
            var entity = _context.Cities
                .Include(c=>c.CityImages)
                .SingleOrDefault(c => c.Id == id);
            if(entity != null)
            {
                var model = _mapper.Map<CityDetailsVM>(entity);
                return View(model);
            }
            return NotFound();
        }

        // GET: CitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityAddEditVM cityAddEdit, 
            string [] cityImages)
        {
            try
            {
                var entity = _mapper.Map<City>(cityAddEdit);
                entity.Image = "no image";
                _context.Cities.Add(entity);
                _context.SaveChanges();
                foreach (var image in cityImages)
                {
                    string name = Path.GetRandomFileName() + ".jpg";
                    var bmp = image.Split(',')[1].ConvertBase64ToBitmap();
                    var path = Path.Combine(Directory.GetCurrentDirectory(), 
                        "Uploads", name);
                    bmp.Save(path, ImageFormat.Jpeg);
                    CityImage cityImage = new CityImage
                    {
                        CityId = entity.Id,
                        Name = name
                    };
                    _context.CityImages.Add(cityImage);
                    _context.SaveChanges();
                }
                //string extension;
                //extension = Path.GetExtension(file.FileName);
                ////string f
                //string name = Path.GetRandomFileName()+ extension;
                //var path = Path.Combine(
                // Directory.GetCurrentDirectory(), "Uploads", name);
                //using (var stream = new FileStream(path, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //}

                //var entity = _mapper.Map<City>(cityAddEdit);
                //entity.Image = name;
                //_context.Cities.Add(entity);
                //_context.SaveChanges();

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
