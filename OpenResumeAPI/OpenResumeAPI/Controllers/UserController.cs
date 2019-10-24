using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenResumeAPI.Models;
using OpenResumeAPI.Business.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OpenResumeAPI.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {

        private IUserBusiness business;
        private ILogger<UserController> logger;

        public UserController(IUserBusiness business, ILogger<UserController> logger)
        {
            this.business = business;
            this.logger = logger;
        }

        /// <summary>
        /// Execute the user login
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody]User user)
        {
            try
            {
                User result = business.Login(user);
                if (result != null)
                    return Ok(result);
                else
                    return Unauthorized();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "LOGIN_ERROR");
            }
        }

        /// <summary>
        /// Get user data
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult Index([FromQuery]int id)
        {
            try
            {
                User result = business.ByID(id);
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [HttpPost("create")]
        [AllowAnonymous]
        public ActionResult Create([FromBody]User user)
        {
            return View();
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [HttpPost("update")]
        public ActionResult Update([FromBody]User user)
        {
            return View();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public ActionResult Delete([FromBody]User user)
        {
            return View();
        }

    }
}