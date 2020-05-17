using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Logging.IServices;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.Static;
using System;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{
    /// <summary>
    /// The controller to provide files to users
    /// </summary>
    public class FileController : BaseController
    {
        #region Properties
        public IFileDatabaseService FileDatabaseService { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public FileController(ILoggingService logginService, IFileDatabaseService fileDatabaseService)
            :base(logginService)
        {
            FileDatabaseService = fileDatabaseService;
        }
        #endregion

        #region GET Request
        /// <summary>
        /// Return the file with the send id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Download(string id)
        {
            //Check if the id was provided
            if (Guid.TryParse(id, out Guid guid))
            {
                //Get the file information
                var linkInfo = await FileDatabaseService.GetLinkInfo(guid);
                //Check if we got the data right
                if (linkInfo != null)
                {
                    return File(linkInfo.Url, linkInfo.ContentType, linkInfo.Title);
                }
            }

            return View(StaticViewNames.NOTFOUND);
        }
        #endregion
    }
}
