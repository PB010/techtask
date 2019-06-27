using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;

namespace TechTask.API.Controllers
{
    [Route("/api/test")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Test()
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);

            return Ok(TokenManager.GenerateToken("check"));
        }
    }
}
