using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Models;
using eCommerceSite.Models.Advertisement;
using eCommerceSite.Models.Cetagory;
using eCommerceSite.Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers.Advertisement
{
    public class AdvertisementController : Controller
    {

        public readonly DatabaseContext _context;

        public AdvertisementController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Advertisement
        public ActionResult Index()
        {
            return View();
        }

        
        [Route("Advertisement/Details_Advertisement/{CID}/{PID}")]
        public async Task<ActionResult> Details_Non_Approve_Post(int CID , int PID)
        {
            var cetagory = await _context.Cetagorie.Where(x => x.CID == CID)
                .FirstOrDefaultAsync<Cetagory>();

            var post = await _context.Post.Where(x => x.PID == PID)
                .FirstOrDefaultAsync<Post>();

            var pohots = await _context.PhotoGellaries.Where(x => x.PID == PID).
                Select(y => new { y.Picture_Name }).ToListAsync(); 

           /* var List_Of_Photo = await _context.Post.Where(x => x.PID == PID).
                Select(y =>  y.Photos.Select(z=> new { z.Picture_Name}).ToList() 
               ).ToListAsync();*/


            ArrayList list = new ArrayList();
            foreach (var item in pohots)
            {
                list.Add(item.Picture_Name);
            }


            Posted_Advertisement_View_Model Non_Approve_Model = new Posted_Advertisement_View_Model()
            {
                Product_Image_1 = list[0].ToString(),
                Product_Image_2 = list[1].ToString(),
                Product_Image_3 = list[2].ToString(),
                Cetagory_Image = cetagory.Photo,
                Cetagory_Title = cetagory.Title,
                Cetagory_Description=cetagory.Details,
                Product_Title = post.Title,
                Product_Description = post.Details,
            };


            return View(Non_Approve_Model);
        }

        // GET: Advertisement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advertisement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Advertisement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Advertisement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Advertisement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Advertisement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}