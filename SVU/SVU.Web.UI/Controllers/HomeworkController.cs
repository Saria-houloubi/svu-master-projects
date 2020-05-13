using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.Models.Homework;
using SVU.Web.UI.Static;
using SVU.Web.UI.ViewModels;
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
        /// <param name="id">The id (name) of the homework</param>
        /// <returns></returns>
        public async Task<IActionResult> ADM(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                switch (id.ToLower())
                {
                    case "tennis":
                        return View(id, new HomeworkTennisViewModel
                        {
                            TennisRecords = await DataSetDatabaseService.GetTennisRecords()
                        });
                    case "heartdisease":
                        return View(id, new HomeworkHeartDiseaseViewModel
                        {
                            HeartDiseasesRecords = await DataSetDatabaseService.GetHeartDiseaseRecords()
                        });

                    default:
                        break;
                }
            }
            return View(StaticViewNames.NOTFOUND);
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
                return View(new HomeworkHeartDiseaseViewModel
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
