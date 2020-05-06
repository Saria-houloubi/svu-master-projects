using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.ViewModels;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{

    /// <summary>
    /// The controller to access the class data
    /// </summary>
    public class CourseController : BaseController
    {
        #region Properties
        public ICourseDatabaseService CourseDatabaseService { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CourseController(ICourseDatabaseService courseDatabaseService)
        {
            CourseDatabaseService = courseDatabaseService;
        }
        #endregion

        #region GET Requests
        /// <summary>
        /// Get the course page
        /// </summary>
        /// <param name="course">The name of the course to be mapped to</param>
        /// <returns></returns>
        [HttpGet("/class/{course}")]
        public async Task<IActionResult> Index(string course)
        {
            //Get the data for the sent course
            var result = await CourseDatabaseService.GetCourse(course);
            //Check if anything was found
            if (result != null)
            {
                //Return to the wanted view
                return View(new CourseViewModel()
                {
                    Course = result
                });
            }
            return View("_NotFound");
        }
        #endregion

    }
}
