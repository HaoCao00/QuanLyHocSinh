using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyHocSinh.Controllers;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Controllers
{
    public class AuthenticateController : BaseController
    {
        #region Property
        /// <summary>
        /// Property Declaration
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IConfiguration _config;

        private ILoginRepository _loginRepository;

        #endregion

        #region Contructor Injector
        /// <summary>
        /// Constructor Injection to access all methods or simply DI(Dependency Injection)
        /// </summary>
        public AuthenticateController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
        }
        #endregion

        #region GenerateJWT
        /// <summary>
        /// Generate Json Web Token Method
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(Account userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region Login Validation
        /// <summary>
        /// Login Authenticaton using JWT Token Authentication
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(Account data)
        {
            if (data != null && data.UserName != null && data.Password != null)
            {
                IActionResult response = Unauthorized();
                var user = await _loginRepository.GetUser(data.UserName, data.Password);
                if (user != null)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    response = Ok(tokenString);
                    return response;
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Authorize the Method
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(Get))]
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return new string[] { accessToken };
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<(Student,Teacher)>> GetStudentOrTeacher(string username)
        {
            if (username == null)
            {
                return NotFound();
            }

            var account = await _loginRepository.GetByUserName(username);
            if (account == null)
            {
                return (null, null);
            }
            var studentOrTeacher = await _loginRepository.GetUserOrTeacher(account.Id);
            

            return studentOrTeacher;
        }
        #endregion

    }
}
