using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceSite.Models.ApiAuth
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class OwnerExpiredAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            var Old_Token = context.HttpContext.Session.GetString("Token");

            if (Old_Token == null)
            {
                context.Result = new RedirectToActionResult(actionName:"Login",controllerName:"Owner",routeValues:"");
                return ; 
            }
            else if (Old_Token.ToString().Equals(""))
            {
                context.Result = new RedirectToActionResult(actionName: "Login", controllerName: "Owner", routeValues: "");
                return;
            }

            else if (!ValidateToken(Old_Token.ToString()).Equals("Fine"))
            {
                context.Result = new RedirectToActionResult(actionName: "Privacy", controllerName: "Home", routeValues: "");
                return;
            }


            //if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var potentialApiKey))
            //{

            //}

            //var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            //var apikey = config.GetValue<string>("ApiKey");

            //if (!apikey.Equals(potentialApiKey))
            //{
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}


            await next();
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
