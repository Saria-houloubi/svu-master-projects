using AMW.API.Controllers.Base;
using AMW.Core.IServices;
using AMW.Data.Models.Candidates;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMW.API.Controllers
{
    public class CandidateController : BaseController
    {

        #region Properties
        private readonly IRepositoryService<Candidate> candidateService;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CandidateController(IRepositoryService<Candidate> candidateService)
        {
            this.candidateService = candidateService;
        }
        #endregion
        /// <summary>
        /// Creates a new candidate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CandidateSecure model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                var result = await candidateService.InsertOrUpdateAsync(model);

                return Ok(GetResponse(result, System.Net.HttpStatusCode.Created));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }
    }
}
