using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTBearerAuth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTBearerAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Login(string username, string pass)
        {
            UserModel login = new UserModel();
            login.Username = username;
            login.Password = pass;
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokernStr = generateJSONWebToken(user);
                //Save token in session object
                HttpContext.Session.SetString("JWToken", tokernStr);
                response = Ok(new { token = tokernStr });
            }
            return response;

        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
             user = UserList.SingleOrDefault(x => x.Username.ToUpper() == login.Username.ToUpper() && x.Password==login.Password);
            if (user!=null)
            {
                user = new UserModel { Username = user.Username, EmailAddress =user.EmailAddress, Password = user.Password};
            }

            return user;
        }

        private string generateJSONWebToken(UserModel userinfo)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userinfo.Username),
                new Claim(JwtRegisteredClaimNames.Email,userinfo.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };



            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials
                );

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;

        }
        [Authorize]
        [HttpPost("Post")]
        public string post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;

            return "Welcome To:" + userName;

        }
        [Authorize]

        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "hi", "hello", "welcome" };
        }

        [HttpGet("Logout")]
        public ActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logout successfully" });
        }



        private List<UserModel> UserList = new List<UserModel>
        {
            new UserModel { Username = "jsmith",Password = "test", EmailAddress = "jsmith@email.com"},
            new UserModel { Username = "srob", Password = "test",EmailAddress = "srob@email.com"},
            new UserModel { Username = "dwill", Password = "test", EmailAddress = "JBlack@email.com" },
            new UserModel { Username = "JBlack", Password = "test",EmailAddress = "JBlack@email.com" },
            new UserModel { Username = "San", Password = "San123",EmailAddress = "San@email.com" }
        };


    }
}
