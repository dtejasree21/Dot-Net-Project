using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace AuthenticationService.Controllers
{
    public class AuthController : ApiController
    {
        private string GenerateJWT(string userName, string role, string key)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("Role", role));
            permClaims.Add(new Claim("Name", userName));
            /*var claims = new[] { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, role) };*/
            var token = new JwtSecurityToken(issuer: "http://mysite.com", audience: "http://mysite.com", expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials, claims: permClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet]
        public IHttpActionResult GetToken(string userName, string role, string key)
        {
            string token = GenerateJWT(userName, role, key);
            return Ok(token);
        }

    }
}
