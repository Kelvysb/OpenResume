using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Exceptions;
using OpenResumeAPI.Helpers.Interfaces;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Controllers
{
    /// <summary>
    /// Resume controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private IResumeBusiness business;

        public ResumeController(IResumeBusiness business,
                                ILogger<ResumeController> logger,
                                IIdentityValidator validator) : base(logger, validator)
        {
            this.business = business;
        }

        /// <summary>
        /// Find Resume by User and Resume name (Anonymous)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="resume"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{user}/{resume}")]
        public ActionResult<Resume> Find([FromRoute]string user, [FromRoute]string resume)
        {
            try
            {
                return Ok(business.Find(user, resume));
            }
            catch (NotFoundException<Resume>)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "RESUME-NOT-FOUNT");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "RESUME-LIST-ERROR");
            }
        }

        /// <summary>
        /// List resumes of an user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public ActionResult<List<Resume>> List([FromRoute]int userId)
        {
            try
            {
                validator.Validate(userId, Request.Headers["Authorization"]);
                return Ok(business.List(userId));
            }
            catch (NotFoundException<Resume>)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "RESUME-NOT-FOUNT");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "RESUME-LIST-ERROR");
            }
        }

        /// <summary>
        /// Create an resume
        /// </summary>
        /// <param name="resume"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<Resume> Create([FromBody]Resume resume)
        {
            try
            {
                validator.Validate(resume.UserId, Request.Headers["Authorization"]);
                Resume result = business.Create(resume);
                return Ok(result);
            }
            catch (ResumeLimitException)
            {
                return StatusCode((int)HttpStatusCode.InsufficientStorage, "RESUME-LIMIT");
            }
            catch (DuplicatedException<Resume>)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "RESUME-DUPLICATED");
            }
            catch (NotFoundException<Resume>)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "RESUME-NOT-FOUND");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "RESUME-CREATE-ERROR");
            }
        }

        /// <summary>
        /// Update an resume
        /// </summary>
        /// <param name="resume"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update([FromBody]Resume resume)
        {
            try
            {
                validator.Validate(resume.UserId, Request.Headers["Authorization"]);
                business.UpdateResume(resume);
                return Ok();
            }
            catch (DuplicatedException<Resume>)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "RESUME-DUPLICATED");
            }
            catch (NotFoundException<Resume>)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "RESUME-NOT-FOUND");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "RESUME-UPDATE-ERROR");
            }
        }

        /// <summary>
        /// Delete an resume
        /// </summary>
        /// <param name="resume"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete([FromBody]Resume resume)
        {
            try
            {
                validator.Validate(resume.UserId, Request.Headers["Authorization"]);
                business.Delete(resume);
                return Ok();
            }
            catch (NotFoundException<Resume>)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "RESUME-NOT-FOUND");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "RESUME-UPDATE-ERROR");
            }
        }

    }
}