using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Models;
using eCommerceSite.Models.ApiAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers.RestAPI
{

    [JWTAuthorizationForREST]
   
    public class APIAdminController : Controller
    {
        private readonly DatabaseContext _context;

        public APIAdminController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("api/Admin/{PID}")]
        [HttpPut("{PID}")]
        public async Task<IActionResult> Approve(int PID)
        {
            var post = await _context.Post.Where(x => x.PID == PID)
                 .FirstOrDefaultAsync<Models.Post.Post>();

            if (post == null)
            {
                return BadRequest();
            }

            var COP = await _context.Cetagorie_Owner_Post.Where(x => x.PID == PID)
               .FirstOrDefaultAsync<Models.Joining.Cetagory_Owner_Post>();

            post.Approve = "YES";
            await _context.SaveChangesAsync();

            COP.Approve = "YES";
            await _context.SaveChangesAsync();

            return Json(new { success = "true", data = "Approve Done!" });

        }

        [Route("api/ProductDetails/{PID}")]
        [HttpGet("{PID}")]
        public async Task<IActionResult> Product_Details(int PID)
        {

            var post = await _context.Post
                .Where(x => x.PID == PID).
                Select(y => new { y.Title, y.Details })
                .FirstOrDefaultAsync();

            var photo = await _context.PhotoGellaries
                .Where(x => x.PID == PID)
                .Select(y => y)
                .ToListAsync();

            ArrayList list = new ArrayList();

            list.Add(post.Title);
            list.Add(post.Details);

            list.Add(photo[0].Picture_Name);
            list.Add(photo[1].Picture_Name);
            list.Add(photo[2].Picture_Name);

            return Json(new { success = true , value = list});
        }

    }
}