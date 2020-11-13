using AMW.API.Controllers.Base;
using AMW.Core.IServices;
using AMW.Data.Models.Educations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMW.API.Controllers
{
    [AllowAnonymous]
    public class LookUpController : BaseController
    {
        #region Properties

        private readonly ILookUpRepository<EducationLevel> eductionLevelLookupService;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public LookUpController(ILookUpRepository<EducationLevel> eductionLevelLookupService)
        {
            this.eductionLevelLookupService = eductionLevelLookupService;
        }
        #endregion


        [HttpGet("EducationLevels")]
        public async Task<ActionResult> Get(EducationLevelFilter filter)
        {
            try
            {
                var result = await eductionLevelLookupService.GetByFilterAsync(filter);

                return Ok(GetResponse(result));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }
    }
}
