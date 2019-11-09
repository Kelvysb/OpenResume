using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenResumeAPI.Models;
using OpenResumeAPI.Business.Interfaces;
using Microsoft.Extensions.Logging;
using OpenResumeAPI.Helpers.Interfaces;
using System.Net;
using OpenResumeAPI.Exceptions;

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
                    return StatusCode((int)HttpStatusCode.Forbidden, "INVALID-LOGIN");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "LOGIN-ERROR");
            }
        }


        /// <summary>
        /// Emaiol confirmation
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("emailconfirm")]
        public ActionResult EmailConfirm([FromQuery]string token)
        {
            try
            {
                business.EmailConfirm(token);
                return Ok();
            }
            catch (InvalidTokenException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "INVALID-CONFIRMATION-TOKEN");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "LOGIN-ERROR");
            }
        }

        /// <summary>
        /// Password Reset
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("passwordreset")]
        public ActionResult<User> PasswordReset([FromQuery]string token)
        {
            try
            {
                User result = business.PasswordReset(token);
                return Ok(result);
            }
            catch (InvalidTokenException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "INVALID-RESET-TOKEN");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "LOGIN-ERROR");
            }
        }

        /// <summary>
        /// Forget Password
        /// </summary>
        /// <param name="email">Token</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("forgetpassword")]
        public ActionResult ForgetPassword([FromQuery]string email)
        {
            try
            {
                business.ForgetPassword(email);
                return Ok();
            }
            catch (InvalidEmailException)
            {
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "LOGIN-ERROR");
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
                validator.Validate(userId, Request.Headers["Authorization"]);
                business.PasswordChange(userId, oldPassword, newPassword);
                return Ok();
            }
            catch (InvalidLoginException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "WRONG-PASSWORD");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "PASSWORD-CHANGE-ERROR");
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
                validator.Validate(userId, Request.Headers["Authorization"]);
                User result = business.ByID(userId);
                return Ok(result);
            }
            catch (NotFoundException<User>)
            {
                return BadRequest();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER-FIND-ERROR");
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
                business.Create(user);
                return Ok();
            }
            catch (DuplicateEmailException)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "EMAIL-DUPLICATED");
            }
            catch (DuplicateLoginException)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "USER-DUPLICATED");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER-CREATE-ERROR");
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
                validator.Validate(user.Id, Request.Headers["Authorization"]);
                business.Update(user);
                return Ok();
            }
            catch (NotFoundException<User>)
            {
                return BadRequest();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER-UPDATE-ERROR");
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
                validator.Validate(user.Id, Request.Headers["Authorization"]);
                business.Delete(user);
                return Ok();
            }
            catch (NotFoundException<User>)
            {
                return BadRequest();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "USER-DELETE-ERROR");
            }
        }

    }
}