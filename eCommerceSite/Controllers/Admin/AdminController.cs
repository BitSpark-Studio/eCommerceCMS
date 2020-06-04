using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eCommerceSite.Models;
using eCommerceSite.Models.Admin;
using eCommerceSite.Models.Cetagory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers.Admin
{
    [Authorize(Roles = "Admin" , AuthenticationSchemes = "Admin")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Admin
      
        public async Task<ActionResult> Index()
        {
            var data = await _context.Cetagorie.ToListAsync();

            return View(data);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginModelView admin)
        {
            if (ModelState.IsValid)
            {
                if(admin.Email=="admin@gmail.com" && admin.Password == "01990251270")
                {
                    await HttpContext.SignOutAsync("Owner");
                    await HttpContext.SignOutAsync("Admin");

                    ClaimsIdentity identity = null;
                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Email,admin.Email),
                        new Claim(ClaimTypes.Role,"Admin")

                     }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("Admin", principal);

                    return Redirect("~/Admin/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login Failed");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Something Went Wrong");
            }

            return View(admin);
        }

        public IActionResult Create_Cetagory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create_Cetagory(CetagoryViewModel cetagoryViewModel)
        {
            if (ModelState.IsValid)
            {

                String UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "CetagoryPicture");
                String UniqueFileName = Guid.NewGuid().ToString() + "_" + cetagoryViewModel.Photo.FileName;
                String FilePath = Path.Combine(UploadFolder, UniqueFileName);
                cetagoryViewModel.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));

                Models.Cetagory.Cetagory cat = new Models.Cetagory.Cetagory()
                {
                    Photo = UniqueFileName,
                    Title= cetagoryViewModel.Title,
                    Details = cetagoryViewModel.Details,
                };

                _context.Cetagorie.Add(cat);
                await _context.SaveChangesAsync();
                ViewBag.Message = "New Cetagory Added";

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Something Went Wrong");
            }
            return View(cetagoryViewModel);
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Cetagory_Details(int id)
        {
            var data = await _context.Cetagorie.Where(x => x.CID == id).
                FirstOrDefaultAsync();
            if (data == null)
            {
                return BadRequest();
            }

            return View(data);
        }

       
       
       

        // GET: Admin/Edit/5
        public async Task<ActionResult> Cetagory_Edit(int id)
        {

           
            var data = await _context.Cetagorie.Where(x => x.CID == id)
                .FirstOrDefaultAsync<Cetagory>();

            if (data == null)
            {
                return BadRequest();
            }

            else
            {
                UpdateCetagory cat = new UpdateCetagory()

                {
                    CID = data.CID,
                    Photo = data.Photo,
                    Title = data.Title,
                    Details = data.Details

                };
                return View(cat);
            }

           
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cetagory_Edit(int id, UpdateCetagory cetagory)
        {
            if (cetagory.Update_Photo == null)
            {
                var data = await _context.Cetagorie.Where(x => x.CID == cetagory.CID).
                    FirstOrDefaultAsync<Cetagory>();
                data.Title = cetagory.Title;
                data.Details = cetagory.Details;
                await _context.SaveChangesAsync();
                
            }
            else
            {
                String UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "CetagoryPicture");
                String UniqueFileName = Guid.NewGuid().ToString() + "_" + cetagory.Update_Photo.FileName;
                String FilePath = Path.Combine(UploadFolder, UniqueFileName);
                cetagory.Update_Photo.CopyTo(new FileStream(FilePath, FileMode.Create));

                var data = await _context.Cetagorie.Where(x => x.CID == cetagory.CID).
                     FirstOrDefaultAsync<Cetagory>();
                data.Photo = UniqueFileName;
                data.Title = cetagory.Title;
                data.Details = cetagory.Details;
                await _context.SaveChangesAsync();

            }
            return Redirect("~/Admin");
        }

        // GET: Admin/Delete/5
        public async Task<ActionResult> Cetagory_Delete(int id)
        {
            var data = await _context.Cetagorie.Where(x => x.CID == id).
                FirstOrDefaultAsync<Cetagory>();
            if (data == null)
            {
                return BadRequest();
            }

            return View(data);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cetagory_Delete(Cetagory cetagory)
        {
            var data = await _context.Cetagorie.Where(x => x.CID == cetagory.CID).
              FirstOrDefaultAsync<Cetagory>();

            if (data == null)
            {
                ModelState.AddModelError(string.Empty, "Something Went Wrong");
                return View();
            }
            else
            {
                _context.Cetagorie.Remove(data);
                await _context.SaveChangesAsync();
                return Redirect("~/Admin");
            }

        }


        [AcceptVerbs("Get","Post")]
        public async Task<ActionResult> Same_Cetagory_Name(String title)
        {
            var data = await _context.Cetagorie.Where(x => x.Title == title).
                FirstOrDefaultAsync<Cetagory>();
            if (data == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

    }
}




