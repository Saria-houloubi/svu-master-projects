using AMW.API.Attributes;
using AMW.API.Controllers.Base;
using AMW.Core.IServices;
using AMW.Data.Models.Candidates;
using AMW.Data.Models.Companies;
using AMW.Data.Models.Jobs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMW.API.Controllers
{
    [AuthorizeJwt(Roles = "Candidate,Company")]
    public class JobController : BaseController
    {

        #region Properties

        private readonly IRepositoryService<Job> jobService;
        private readonly IRepositoryService<Candidate> candidateService;
        #endregion

        #region Constructer

        public JobController(IRepositoryService<Job> jobService, IRepositoryService<Candidate> candidateService)
        {
            this.jobService = jobService;
            this.candidateService = candidateService;
        }
        #endregion
        /// <summary>
        /// Gets a list of all sutable for a candidate
        /// </summary>
        /// <returns></returns>
        [HttpGet("list/suitable")]
        [AuthorizeJwt(Roles = "Candidate")]
        public async Task<IActionResult> GetSuitable([FromQuery] JobSorter sorter)
        {
            try
            {
                var candidate = await candidateService.GetByIdAsync(int.Parse(User.Identity.Name));

                var result = await jobService.GetByFilterAsync(new JobFilter()
                {
                    EducationLevel = candidate?.EducationLevel,
                    ExperienceYears = candidate?.Experince,
                }, sorter);

                return Ok(GetResponse(result));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }
        /// <summary>
        /// Gets a list of all jobs 
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] JobSorter sorter)
        {
            try
            {
                var result = await jobService.GetByFilterAsync(new JobFilter(), sorter);

                return Ok(GetResponse(result));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }
        /// <summary>
        /// Creates a new job
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeJwt(Roles = nameof(Company))]
        public async Task<IActionResult> Post([FromBody]Job model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                model.CompanyId = int.Parse(User.Identity.Name);

                var result = await jobService.InsertOrUpdateAsync(model);

                return Ok(GetResponse(result, model.Id > 0 ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.Created));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }

        /// <summary>
        /// Gets diplomas based on sent filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost(nameof(Filter))]
        public async Task<IActionResult> Filter([FromBody] JobFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                var result = await jobService.GetByFilterAsync(filter);

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
