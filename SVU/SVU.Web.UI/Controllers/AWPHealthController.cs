using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Logging.IServices;
using SVU.Web.UI.Controllers.Base;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{
    /// <summary>
    /// The controller to work with the AWP health homework
    /// </summary>
    [Authorize(Roles ="admin")]
    
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
        /// Gets a list of blogs
        /// </summary>
        /// <param name="start">The index to start taking blogs from for pagination</param>
        /// <param name="count">The count of blogs to get</param>
        /// <returns></returns>
        public async Task<IActionResult> Blogs(int start, int count)
        {
            //Try to get the list of saved blogs in the db
            var blogs = await HealthBlogService.GetBlogs(start, count);

            //Check if we got any data
            if (blogs != null)
            {
                return Ok(blogs);
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
                //Try to add of update the blog
                var blog = await HealthBlogService.AddOrUpdateBlog(model);
                //Check if the operation was succesfull
                if (blog != null)
                {
                    return Ok(blog);
                }
                return InternalServerError();
            }

            //TODO: only return the validation results why it is not validated
            return CustomBadRequest(ModelState.Values);
        }
        #endregion
    }
}
