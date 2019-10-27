using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenResumeAPI.Models;
using OpenResumeAPI.Business.Interfaces;
using Microsoft.Extensions.Logging;
using OpenResumeAPI.Helpers.Interfaces;
using System.Net;

namespace OpenResumeAPI.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {

        private IUserBusiness business;

        public UserController(IUserBusiness business,
                              ILogger<UserController> logger,
                              IIdentityValidator validator) : base(logger, validator)
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
        public ActionResult<User> Login([FromBody]User user)
        {
            try
            {
                User result = business.Login(user);
                if (result != null)
                    return Ok(result);
                else
                    return StatusCode((int)HttpStatusCode.Forbidden, "INVALID_LOGIN");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "LOGIN_ERROR");
            }
        }


        /// <summary>
        /// Emaiol confirmation
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("emailconfirm")]
        public ActionResult EmailConfirm([FromQuery]string email, [FromQuery]string token)
        {
            try
            {
                if (business.EmailConfirm(email, token))
                    return Ok();
                else
                    return StatusCode((int)HttpStatusCode.Forbidden, "INVALID_CONFIRMATION_TOKEN");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "LOGIN_ERROR");
            }
        }


        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost("passwordchange")]
        public ActionResult PasswordChange([FromBody]int userId, [FromBody]string oldPassword, [FromBody]string newPassword)
        {
            try
            {
                if (validator.Validate(userId, Request.Headers["Authorization"]))
                {                    
                    if (business.PasswordChange(userId, oldPassword, newPassword))
                        return Ok();
                    else
                        return StatusCode((int)HttpStatusCode.Forbidden, "WRONG_PASSWORD");
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "PASSWORD_CHANGE_ERROR");
            }
        }

        /// <summary>
        /// Get user data
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public ActionResult<User> Find([FromRoute]int userId)
        {
            try
            {
                if (validator.Validate(userId, Request.Headers["Authorization"]))
                {
                    User result = business.ByID(userId);
                    if (result != null)
                        return Ok(result);
                    else
                        return BadRequest();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER_FIND_ERROR");
            }
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut]
        public ActionResult Create([FromBody]User user)
        {
            try
            {
                business.Insert(user);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER_CREATE_ERROR");
            }
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update([FromBody]User user)
        {
            try
            {
                if (validator.Validate(user.Id, Request.Headers["Authorization"]))
                {
                    if (business.Update(user))
                        return Ok();
                    else
                        return BadRequest();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER_UPDATE_ERROR");
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete([FromBody]User user)
        {
            try
            {
                if (validator.Validate(user.Id, Request.Headers["Authorization"]))
                {
                    if (business.Delete(user))
                        return Ok();
                    else
                        return BadRequest();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER_DELETE_ERROR");
            }
        }

    }
}