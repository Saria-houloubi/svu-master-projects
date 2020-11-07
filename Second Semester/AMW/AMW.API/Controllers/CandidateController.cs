using AMW.API.Controllers.Base;
using AMW.Core.IServices;
using AMW.Data.Models.Amw;
using AMW.Data.Models.Candidates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMW.API.Controllers
{
    public class CandidateController : BaseController
    {

        #region Properties
        private readonly IRepositoryService<Candidate> candidateService;
        private readonly IAuthService<Candidate, AmwSecure> authCandidateService;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CandidateController(IRepositoryService<Candidate> candidateService, IAuthService<Candidate, AmwSecure> authCandidateService)
        {
            this.candidateService = candidateService;
            this.authCandidateService = authCandidateService;
        }
        #endregion

        /// <summary>
        /// Creates a new candidate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]CandidateRegister model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    model.Id = int.Parse(User.Identity.Name);
                }

                var result = await candidateService.InsertOrUpdateAsync(model);

                return Ok(GetResponse(result, model.Id > 0 ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.Created));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }

        /// <summary>
        /// Gets candidate based on sent filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost(nameof(Filter))]
        public async Task<IActionResult> Filter([FromBody] CandidateFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                var result = await candidateService.GetByFilterAsync(filter);

                return Ok(GetResponse(result));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }

        /// <summary>
        /// Tries to authenticate a candidate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(nameof(Auth))]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] AmwSecure model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                var result = await authCandidateService.TryAuthenticateAsync(model);

                var response = GetResponse(result, result != null ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.Unauthorized);

                if (response.Status == System.Net.HttpStatusCode.Unauthorized)
                {
                    response.HasErrors = true;
                    response.Errors.Add(new Models.ErrorModel("Username or password is incorrect please try again"));
                }

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }
    }
}
