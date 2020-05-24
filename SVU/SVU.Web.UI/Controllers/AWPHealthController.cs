using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Logging.IServices;
using SVU.Shared.Messages;
using SVU.Shared.Static;
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
    [Authorize(Roles = "admin")]

    public class AWPHealthController : BaseController
    {
        #region Properties
        public IHealthBlogService HealthBlogService { get; private set; }
        #endregion

        #region Contructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public AWPHealthController(ILoggingService loggingService, IHealthBlogService healthBlogService)
            : base(loggingService)
        {
            HealthBlogService = healthBlogService;
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
            return View(new HealthUserViewModel());
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
                var blog = await HealthBlogService.GetBlog(blogId, !HttpContext.User.Identity.IsAuthenticated);

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

        #endregion

        #region POST Requests
        /// <summary>
        /// Adds a new blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPost]
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

            return CustomBadRequest(ModelState.GetValidationErrors_LSV());
        }


        #endregion

        #region DELETE Requests

        /// <summary>
        /// Deletes a blogs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
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
