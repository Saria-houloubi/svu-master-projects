using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.Models.Homework;
using SVU.Web.UI.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{

    /// <summary>
    /// The controller to execute the homeworks 
    /// </summary>
    public class HomeworkController : BaseController
    {
        #region Properties
        public IDataSetDatabaseService DataSetDatabaseService { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeworkController(IDataSetDatabaseService dataSetDatabaseService)
        {
            DataSetDatabaseService = dataSetDatabaseService;
        }
        #endregion

        #region GET Requests

        /// <summary>
        /// The adm course home work for classification using ID3 and bayes
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ADM()
        {
            return View(new HomeworkADMViewModel
            {
                HeartDiseasesRecords = await DataSetDatabaseService.GetHeartDiseaseRecords()
            });
        }

        #endregion

        #region POST Requests
        /// <summary>
        /// Dose the calculation for the sent alog name as the id paramter
        /// </summary>
        /// <param name="id">The name of the alog to execute</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ADM(string id, [FromBody] HeartDiseasePatientModel model)
        {
            //Check if the data is send correctly
            if (ModelState.IsValid)
            {
                return View(new HomeworkADMViewModel
                {
                    HeartDiseasesRecords = await DataSetDatabaseService.GetHeartDiseaseRecords()
                });
            }
            else
            {
                return BadRequest("Some data were not provided plase check and try again");
            }
        }


        #endregion

    }
}
