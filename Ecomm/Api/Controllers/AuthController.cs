using Api.Core;
using Api.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly JwtManager manager;

        public AuthController(JwtManager manager)
        {
            this.manager = manager;
        }

        [HttpPost("Login", Name = "Post")]
        public IActionResult Post(  [FromBody] LoginRequest request)
        {        
            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.ASCII.GetBytes(request.Password);
            var md5Data = md5.ComputeHash(bytes);
            var hashedPassword = Convert.ToBase64String(md5Data); 
                   
            request.Password = hashedPassword;    

            // Path name long - to confuser hackers ;)
            var token = manager.MakeToken(request.Username, request.Password); 
               
            if (token == null)
            { 
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}
