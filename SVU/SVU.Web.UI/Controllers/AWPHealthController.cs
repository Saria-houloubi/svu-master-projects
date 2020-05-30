using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Logging.IServices;
using SVU.Shared.Messages;
using SVU.Shared.Static;
using SVU.Web.UI.Attribute;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.Extensions;
using SVU.Web.UI.Static;
using SVU.Web.UI.ViewModels.Health;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{
    /// <summary>
    /// The controller to work with the AWP health homework
    /// </summary>
    [Authorize(Roles = UserRoles.ALL)]
    public class AWPHealthController : BaseController
    {
        #region Properties
        public IHealthBlogService HealthBlogService { get; private set; }
        public IHealthRequestService HealthRequestService { get; private set; }
        #endregion

        #region Contructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public AWPHealthController(ILoggingService loggingService, IHealthBlogService healthBlogService, IHealthRequestService healthRequestService)
            : base(loggingService)
        {
            HealthBlogService = healthBlogService;
            HealthRequestService = healthRequestService;
        }
        #endregion

        #region GET Requests
        /// <summary>
        /// Get the blogs page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Blogs()
        {
            return View();
        }

        /// <summary>
        /// Get the blogs page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult HealthRequest()
        {
            return View(new HealthRequestViewModel());
        }
        /// <summary>
        /// Displays a blog to the user
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Blog(string id)
        {
            //Try to read the blog id
            if (Guid.TryParse(id, out Guid blogId))
            {
                //Try to get the blog from teh db
                var blog = await HealthBlogService.GetBlog(blogId, (!User.IsInRole(UserRoles.ADMIN)));

                if (blog != null)
                {
                    return View(new HealthBlogViewModel()
                    {
                        Blog = blog
                    });
                }
            }
            return View(StaticViewNames.NOTFOUND);
        }

        /// <summary>
        /// Displays a blog to the user
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBlog(string id)
        {
            //Try to read the blog id
            if (Guid.TryParse(id, out Guid blogId))
            {
                //Try to get the blog from teh db
                var blog = await HealthBlogService.GetBlog(blogId, !HttpContext.User.Identity.IsAuthenticated);

                if (blog != null)
                {
                    return Ok(blog);
                }
            }
            return NotFound($"Blog not found with the id of {id}");
        }

        /// <summary>
        /// Gets a list of blogs
        /// </summary>
        /// <param name="start">The index to start taking blogs from for pagination</param>
        /// <param name="count">The count of blogs to get</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBlogs(int start, int count, bool loadImages = false)
        {
            //Try to get the list of saved blogs in the db
            var blogs = await HealthBlogService.GetBlogs(start, count, loadImages);

            //Check if we got any data
            if (blogs != null)
            {
                return Ok(blogs.Select(item => CreateResponseBlogJSON(item, loadImages)));
            }
            return InternalServerError();
        }
        /// <summary>
        /// Displays a health request to the user
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetHealthRequest(string id)
        {
            //Try to read the healthRequest id
            if (Guid.TryParse(id, out Guid requestId))
            {
                //Try to get the health request from the db
                var healthRequest = await HealthRequestService.GetHealthRequest(requestId);

                if (healthRequest != null)
                {
                    return Ok(new
                    {
                        healthRequest.Subject,
                        healthRequest.Content,
                        CreationDate = healthRequest.CreationDate.ToString(DateFormats.DefaultDate),
                        Replies = healthRequest.Replies.Select(item => new
                        {
                            item.Content,
                            item.Note,
                            CreationDate = item.CreationDate.ToString(DateFormats.DefaultDate),
                            IsAway = item.UserId.ToString() != User.GetClaimValue(ClaimTypes.NameIdentifier)
                        }).ToList()
                        ,
                        id = healthRequest.Id
                    });
                }
            }
            return NotFound($"Blog not found with the id of {id}");
        }
        /// <summary>
        /// Gets a list of health requests for the user
        /// </summary>
        /// <param name="start">The index to start taking blogs from for pagination</param>
        /// <param name="count">The count of blogs to get</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetHealthRequests(int start, int count)
        {
            //Try to get the list of saved health requests for that user in the db
            var healthRequests = await HealthRequestService.GetHealthRequests(start, count, Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier)), User.IsInRole(UserRoles.ADMIN));

            //Check if we got any data
            if (healthRequests != null)
            {
                return Ok(healthRequests.Select(item => new
                {
                    item.Id,
                    item.Subject,
                    item.Content,
                    item.Note,
                    CreationDate = item.CreationDate.ToString(DateFormats.DefaultDate)
                }).ToList());
            }
            return InternalServerError();
        }
        #endregion

        #region POST Requests
        /// <summary>
        /// Adds a new blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<IActionResult> Blog(Blog model)
        {
            //Check if the model is valid
            if (ModelState.IsValid)
            {
                if (model.Content.Contains("<script>"))
                {
                    return CustomBadRequest($"Content : {ErrorMessages.JavaScriptNotAllowed}");
                }
                //Try to get the user id
                if (Guid.TryParse(HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier), out Guid id))
                {
                    //if the blog is new then set the author id
                    if (model.Id == Guid.Empty)
                    {
                        model.AutherId = id;
                    }

                    model.LastEditUserId = id;
                }
                //Try to add of update the blog
                var blog = await HealthBlogService.AddOrUpdateBlog(model);
                //Check if the operation was succesfull
                if (blog != null)
                {
                    return Ok(CreateResponseBlogJSON(blog));
                }
                return InternalServerError();
            }

            return CustomBadRequest(ModelState.GetValidationErrors());
        }

        /// <summary>
        /// Adds or updates a value for the requets
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [BotDetectorCaptcha]
        public async Task<IActionResult> HealthRequest(HealthRequestViewModel model)
        {
            //Check if the model is valid
            if (ModelState.IsValid)
            {
                //Try to add of update the blog
                var request = await HealthRequestService.AddOrUpdateRequest(new Database.Models.HealthRequest
                {
                    Content = model.Content,
                    Id = model.Id,
                    Subject = model.Subject,
                    Note = model.Note,
                    HealthUserId = Guid.Parse(HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier))
                });

                //Check if the operation was not succesfull
                if (request == null)
                {
                    model.Errors.Add(ErrorMessages.SomthingWorngHappend);
                }

                model.Id = request.Id;
            }
            else
            {
                model.Errors.AddRange(ModelState.GetValidationErrors());
            }

            return View(StaticViewNames.HEALTH_REQUEST, model.Errors.Any() ? model : new HealthRequestViewModel());
        }


        /// <summary>
        /// Adds or updates a value for the request reply
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> HealthRequestReply([FromBody] HealthRequestReply model)
        {
            //Check if the model is valid
            if (ModelState.IsValid)
            {
                //Set the user id
                model.UserId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));
                //Try to add of update the blog
                var reply = await HealthRequestService.AddOrUpdateRequestReply(model);

                //Check if the operation was not succesfull
                if (reply != null)
                {
                    return Ok(new
                    {
                        reply.Content,
                        reply.Id,
                        CreationDate =  reply.CreationDate.ToString(DateFormats.DefaultDate),
                        reply.RequestId,
                    });
                }
                return InternalServerError();
            }

            return CustomBadRequest(ModelState.GetValidationErrors());
        }


        #endregion

        #region DELETE Requests

        /// <summary>
        /// Deletes a blogs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            //Check if we got an id
            if (Guid.TryParse(id, out Guid blogId))
            {
                var result = await HealthBlogService.DeleteBlog(blogId);

                return result ? Ok() : InternalServerError();
            }
            return CustomBadRequest();
        }
        #endregion


        #region Helpers
        /// <summary>
        /// Create a JSON object that is formated for the response
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        private object CreateResponseBlogJSON(Blog blog, bool includeThumnail = false) => new
        {
            blog.Id,
            blog.Title,
            blog.PreviewContent,
            ThumbnailBase64 = includeThumnail ? $"data:image/{blog.ThumbnailMimeType};base64,{blog.ThumbnailBase64}" : "",
            HasThumbnail = !string.IsNullOrWhiteSpace(blog.ThumbnailBase64),
            blog.ThumbnailMimeType,
            blog.Note,
            blog.VisitCout,
            blog.Content,
            CreationDate = blog.CreationDate.ToString(DateFormats.DefaultDate),
            LastUpdatedDate = blog.LastUpdatedDate.ToString(DateFormats.DefaultDate),
        };
        #endregion

    }
}
