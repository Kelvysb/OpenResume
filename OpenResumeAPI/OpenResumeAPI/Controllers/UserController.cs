using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenResumeAPI.Models;
using OpenResumeAPI.Business.Interfaces;

namespace OpenResumeAPI.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {

        IUserBusiness business;

        public UserController(IUserBusiness business)
        {
            this.business = business;
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
            return View();
        }

        /// <summary>
        /// Get user data
        /// </summary>
        /// <returns></returns>
        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromBody]User user)
        {
            return View();
        }

    }
}