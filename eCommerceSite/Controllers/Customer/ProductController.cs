using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using eCommerceSite.Models;
using eCommerceSite.Models.Customer;
using eCommerceSite.Models.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceSite.Controllers.Customer
{
    public class ProductController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("Product/Details/{CID}")]
        
        public async Task<ActionResult> Details(int CID)
        {
            var Cetagory = _context.Cetagorie
                .Where(x => x.CID == CID)
                .FirstOrDefault<Models.Cetagory.Cetagory>();

            if (Cetagory == null) return BadRequest();

            var cat = new ProductCetagoryOwner()
            {
               
                Cetagory_Title = Cetagory.Title,
                Cetagory_Details = Cetagory.Details,
                Cetagory_Photo = Cetagory.Photo,
               

            };

            List<ProductCetagoryOwner> post = new List<ProductCetagoryOwner>();

            post.Add(cat);


            var Product_And_Owner = await _context.Cetagorie_Owner_Post
                .Where(y=> (y.Approve=="YES" && y.CID==CID))
                .Select(x => new { x.PID,x.OID })
                .ToListAsync();

           

            foreach(var item in Product_And_Owner)
            {
                var JWT_Token = await _context.Owner
                   .Where(x => x.OID == item.OID)
                   .Select(y => new { y.JWT_Token })
                   .FirstOrDefaultAsync();

                if (ValidateToken(JWT_Token.JWT_Token.ToString()).Equals("Fine"))
                {
                    var valid_post = await _context.Post
                        .Where(x => x.PID == item.PID)
                        .FirstOrDefaultAsync
                        <Post>();
                    var data = new ProductCetagoryOwner()
                    {
                        Product_Title = valid_post.Title,
                        Product_Description = valid_post.Details,
                       
                        PID = item.PID,
                        OID = item.OID,
                       
                    };


                    post.Add(data);
                }

            }

            return View(post);
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