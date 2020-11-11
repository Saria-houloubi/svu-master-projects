using AMW.API.Controllers.Base;
using AMW.Core.IServices;
using AMW.Data.Models.Amw;
using AMW.Data.Models.Companies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMW.API.Controllers
{
    public class CompanyController : BaseController
    {
        #region Properties
        private readonly IRepositoryService<Company> companyService;
        private readonly IAuthService<Company, AmwSecure> authCandidateService;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CompanyController(IRepositoryService<Company> companyService, IAuthService<Company, AmwSecure> authCandidateService)
        {
            this.companyService = companyService;
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
        public async Task<IActionResult> Post([FromBody]CompanyRegister model)
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

                var result = await companyService.InsertOrUpdateAsync(model);

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
        public async Task<IActionResult> Filter([FromBody] CompanyFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                var result = await companyService.GetByFilterAsync(filter);

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
