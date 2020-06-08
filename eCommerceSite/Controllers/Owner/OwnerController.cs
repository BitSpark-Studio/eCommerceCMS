using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using eCommerceSite.Models;
using eCommerceSite.Models.Cetagory;
using eCommerceSite.Models.Owner;
using eCommerceSite.Models.Post;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceSite.Controllers.Owner
{
    [Authorize(Roles = "Owner", AuthenticationSchemes = "Owner")]
    public class OwnerController : Controller
    {

        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public OwnerController(DatabaseContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Owner
        [Route("Owner/Index")]
        public async Task<ActionResult> Index()
        {
            var data = await _context.Cetagorie.ToArrayAsync();

            return View(data);
        }

        [Route("Owner/NotApproveTable")]
        public async Task<ActionResult> NotApproveTable()
        {
            int OID = Convert.ToInt32(HttpContext.Session.GetString("OID"));
            var data = await _context.Cetagorie_Owner_Post.Where((x => x.OID == OID && x.Approve=="NO")).
                Select(y => new { y.CID, y.PID }).ToListAsync();

            List<Posted_Advertisement_Details_ViewModel> Non_Approve_Advertisement = 
                new List<Posted_Advertisement_Details_ViewModel>();

            for(int i = 0; i < data.Count; i++)
            {
                int PID = data[i].PID;
                    
                var Post = await _context.Post.Where(x => x.PID == PID).
                    Select(y => new { y.Title, y.Details }).ToListAsync();
                
                int CID = data[i].CID;

                var Cetagory = await _context.Cetagorie.Where(x => x.CID == CID).
                    Select(y => new { y.Title,y.Photo }).ToListAsync();

                var Non_Approve_Data = new Posted_Advertisement_Details_ViewModel()
                {
                    Product_Title = Post[0].Title,
                    Product_Description = Post[0].Details,
                    Cetagory_Name = Cetagory[0].Title,
                    Cetagory_Image = Cetagory[0].Photo,
                    CID = CID,
                    PID = PID,
                    JWT_Token = HttpContext.Session.GetString("Token"),
                };                                
                Non_Approve_Advertisement.Add(Non_Approve_Data);

            }

            return View(Non_Approve_Advertisement);

        }

        [Route("Owner/ApproveTable")]
        public async Task<ActionResult> ApproveTable()
        {
            int OID = Convert.ToInt32(HttpContext.Session.GetString("OID"));
            var data = await _context.Cetagorie_Owner_Post.Where((x => x.OID == OID && x.Approve == "YES")).
                Select(y => new { y.CID, y.PID }).ToListAsync();

            List<Posted_Advertisement_Details_ViewModel> Non_Approve_Advertisement =
                new List<Posted_Advertisement_Details_ViewModel>();

            for (int i = 0; i < data.Count; i++)
            {
                int PID = data[i].PID;

                var Post = await _context.Post.Where(x => x.PID == PID).
                    Select(y => new { y.Title, y.Details }).ToListAsync();

                int CID = data[i].CID;

                var Cetagory = await _context.Cetagorie.Where(x => x.CID == CID).
                    Select(y => new { y.Title, y.Photo }).ToListAsync();

                var Non_Approve_Data = new Posted_Advertisement_Details_ViewModel()
                {
                    Product_Title = Post[0].Title,
                    Product_Description = Post[0].Details,
                    Cetagory_Name = Cetagory[0].Title,
                    Cetagory_Image = Cetagory[0].Photo,
                    CID = CID,
                    PID = PID,
                };
                Non_Approve_Advertisement.Add(Non_Approve_Data);

            }

            return View(Non_Approve_Advertisement);

        }


        [Route("Owner/Post/{cid}")]
        [HttpGet("{cid}")]
        public async  Task<IActionResult> Post(int cid)
        {

            if (cid == 0)
            {
                return BadRequest();
            }

            var data =  await _context.Cetagorie.Where(x => x.CID == cid)
                .FirstOrDefaultAsync<Cetagory>();
            if (data == null)
            {
                return BadRequest();
            }

            var Cetagory_Details = new PostViewModel()
            {
                Cetagory_Title = data.Title,
                Cetagory_Image = data.Photo,
            };
           
            return View(Cetagory_Details);
        }
        [Route("Owner/Post/{cid}")]
        [HttpPost]
        
        public async Task<IActionResult> Post(int cid,PostViewModel post)
        {
            if (ModelState.IsValid)
            {
               

                String UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Product");
                String UniqueFileName = Guid.NewGuid().ToString() + "_" + post.Photo_1.FileName;
                String FilePath = Path.Combine(UploadFolder, UniqueFileName);
                post.Photo_1.CopyTo(new FileStream(FilePath, FileMode.Create));

                
                String UniqueFileName1 = Guid.NewGuid().ToString() + "_" + post.Photo_2.FileName;
                String FilePath1 = Path.Combine(UploadFolder, UniqueFileName1);
                post.Photo_2.CopyTo(new FileStream(FilePath1, FileMode.Create));

                String UniqueFileName2 = Guid.NewGuid().ToString() + "_" + post.Photo_3.FileName;
                String FilePath2 = Path.Combine(UploadFolder, UniqueFileName2);
                post.Photo_3.CopyTo(new FileStream(FilePath2, FileMode.Create));

                var AddPost = new Post()
                {
                    Title = post.Title,
                    Details=post.Description,
                    Approve="NO",
                };

                _context.Post.Add(AddPost);
                await _context.SaveChangesAsync();

                var data = await _context.Post.Where(x => x.Title == post.Title).
                    FirstOrDefaultAsync<Post>();

                int PID = data.PID;

                var joining = new Models.Joining.Cetagory_Owner_Post()
                {

                    PID=PID,
                    OID = Convert.ToInt32(HttpContext.Session.GetString("OID")),
                    CID = cid,
                    Approve="NO"
                };

                _context.Cetagorie_Owner_Post.Add(joining);
                await _context.SaveChangesAsync();
               

                var photoGelary = new Models.Post.PhotoGellary()
                {
                    PID = PID,
                    Picture_Name = UniqueFileName,
                };
                var photoGelary1 = new Models.Post.PhotoGellary()
                {
                    PID = PID,
                    Picture_Name = UniqueFileName1,
                };
                var photoGelary2 = new Models.Post.PhotoGellary()
                {
                    PID = PID,
                    Picture_Name = UniqueFileName2,
                };

                _context.PhotoGellaries.Add(photoGelary);
                await _context.SaveChangesAsync();

                _context.PhotoGellaries.Add(photoGelary1);
                await _context.SaveChangesAsync();

                _context.PhotoGellaries.Add(photoGelary2);
                await _context.SaveChangesAsync();

                ViewBag.Message = "Done";
            }

            return View();
        }
        [AcceptVerbs("Get","Post")]
        public async Task<ActionResult> Same_Title_Post(String title)
        {
            var data = await _context.Post.Where(x => x.Title == title).
                FirstOrDefaultAsync<Post>();
            if (data == null)
                return Json(true);
            else
                return Json(false);

        }

        // GET: Owner/Details/5
        [Route("Owner/Details/{CID}")]
        public async Task<ActionResult> Details(int CID)
        {
            var data = await _context.Cetagorie.Where(x => x.CID == CID)
                .FirstOrDefaultAsync<Cetagory>();

            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return View(data);
            }

            
        }

        [AllowAnonymous]

        // GET: Owner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Owner/Create
       
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(OwnerRegistrationViewModel owner)
        {
            if (ModelState.IsValid)
            {
                String UploadFolder= Path.Combine(_hostingEnvironment.WebRootPath, "ProfilePicture");
                String UniqueFileName = Guid.NewGuid().ToString() + "_" + owner.Photo.FileName;
                String FilePath = Path.Combine(UploadFolder, UniqueFileName);
                owner.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));


                Models.Owner.Owner SaveOwner = new Models.Owner.Owner()
                {
                    First_Name = owner.First_Name,
                    Last_Name = owner.Last_Name,
                    Email = owner.Email,
                    Phone = owner.Phone,
                    Address = owner.Address,
                    Password = owner.Password,
                    JWT_Token = GenerateToken(owner.Email,"Owner"),
                    Photo = UniqueFileName,

                };

                _context.Owner.Add(SaveOwner);
                await _context.SaveChangesAsync();

                ViewBag.Message = "Registration Complete";
            }
           
            return View(owner);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(OwnerLoginViewModel owner)
        {

            if (ModelState.IsValid)
            {

                var data = await _context.Owner.Where((x => x.Email == owner.Email && x.Password == owner.Password)).
                    FirstOrDefaultAsync<Models.Owner.Owner>();

                if (data == null)
                {
                    ModelState.AddModelError(string.Empty, "Email / Password Doesn't Match");
                }
                else
                {
                    if (data.JWT_Token.Equals("Block"))
                    {
                        ModelState.AddModelError(string.Empty, "Sorry You Are Blocked By Admin");
                        return View(owner);
                    }
                    else if (ValidateToken(data.JWT_Token).Contains("SecurityTokenExpiredException"))
                    {
                        ModelState.AddModelError(string.Empty, "ফ্রি ব্যবহার করার মেয়াদ শেষ হয়ে গেছে দয়া করে এডমিনের সাথে এ ব্যপারে যোগাযোগ করুন।");
                        return View(owner);
                    }
                    else if ((ValidateToken(data.JWT_Token).Contains("Error___110231____6243437_sdr_ewr_Token_Unable_To____Decode")))
                    {
                        ModelState.AddModelError(string.Empty, "এপ্লিকিশনে কাজ চলছে দয়া করে কিছুক্ষন পর আবার লগইন করুন। আপনার ভোগান্তির জন্য আমরা আন্তরিক ভাবে দুঃখিত");
                        return View(owner);
                    }

                    

                    await HttpContext.SignOutAsync("Owner");
                    await HttpContext.SignOutAsync("Admin");

                    ClaimsIdentity identity = null;
                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Email,owner.Email),
                        new Claim(ClaimTypes.Role,"Owner")

                     }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("Owner", principal);

                    HttpContext.Session.SetString("OID", data.OID.ToString());
                    HttpContext.Session.SetString("Token", data.JWT_Token);

                    return Redirect("~/Owner/Index");
                }

            }
            return View(owner);
        }


        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> SameEmail(String Email)
        {
            var data = await _context.Owner.Where(x => x.Email == Email).
                FirstOrDefaultAsync<Models.Owner.Owner>();

            if (data == null)
            {
               return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        // GET: Owner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Owner/Edit/5
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

        // GET: Owner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Owner/Delete/5
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

        private  string GenerateToken(String Email,String Role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constent.key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: Constent.Issuer,
                audience: Constent.Audiunce,
                claims: new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, Email),
                new Claim(ClaimTypes.Role, Role)
                },
                expires: DateTime.UtcNow.AddMonths(2));

            var handler = new JwtSecurityTokenHandler();
         
            return handler.WriteToken(secToken);
        }


        private String ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            }
            catch (Exception e)
            {
                return e.ToString() + "Error___110231____6243437_sdr_ewr_Token_Unable_To____Decode";
            }
            return "Fine";
        }


        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = Constent.Issuer,
                ValidAudience = Constent.Audiunce,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constent.key)) // The same key as the one that generate the token
            };
        }

    }
}