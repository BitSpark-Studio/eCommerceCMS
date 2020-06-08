using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Models;
using eCommerceSite.Models.ApiAuth;
using eCommerceSite.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers.RestAPI
{

   
    [Route("api/Owners")]
    [JWTAuthorizationForREST]
    public class APIOwnerController : Controller
    {

        private readonly DatabaseContext _context;

        public APIOwnerController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("DeleteProduct/{PID}")]
        [HttpDelete("{PID}")]
        
        public async Task<IActionResult> Delete(int PID)
        {
            var post = await _context.Post.Where(x => x.PID == PID)
                .FirstOrDefaultAsync<Post>();
            post.Approve = "DEL";
            await _context.SaveChangesAsync();

            var owner_post_cat = await _context.Cetagorie_Owner_Post.Where(x => x.PID == PID)
                .FirstOrDefaultAsync<Models.Joining.Cetagory_Owner_Post>();

            owner_post_cat.Approve = "DEL";
            await _context.SaveChangesAsync();

            return Json(new { success = true, data = "Delete Done" });

        }
    }
}
