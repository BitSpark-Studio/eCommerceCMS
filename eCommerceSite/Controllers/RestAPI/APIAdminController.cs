using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eCommerceSite.Models;
using eCommerceSite.Models.ApiAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            var Owner_ID = await _context.Cetagorie_Owner_Post
                .Where(x => x.PID == PID)
                .Select(y => new { y.OID })
                .FirstOrDefaultAsync();

            int OID = Convert.ToInt32(Owner_ID.OID);

            var Owner = await _context.Owner
                .Where(x => x.OID == OID)
                .Select(y => new { y.First_Name, y.Last_Name, y.Phone, y.Email,y.Photo })
                .FirstOrDefaultAsync();


            ArrayList list = new ArrayList();

            list.Add(post.Title);
            list.Add(post.Details);

            list.Add(photo[0].Picture_Name);
            list.Add(photo[1].Picture_Name);
            list.Add(photo[2].Picture_Name);
            list.Add(Owner.First_Name +" "+Owner.Last_Name);
            list.Add(Owner.Phone);
            list.Add(Owner.Email);
            list.Add(Owner.Photo);


            return Json(new { success = true , value = list});
        }

        [Route("api/RenewUser/{OID}")]
        [HttpPut("{OID}")]
        public async Task<IActionResult> Renew_User(int OID)
        {
            var owner = await _context.Owner
                .Where(x => x.OID == OID)
                .FirstOrDefaultAsync();
            if (owner == null)
            {
                return BadRequest();
            }

            owner.JWT_Token = GenerateToken(owner.Email.ToString(), "Owner");
            await _context.SaveChangesAsync();

            return Json(new { success = true, data = "User Renew Done!" });

        }

        private string GenerateToken(String Email, String Role)
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

    }
}