using AMW.API.Attributes;
using AMW.API.Controllers.Base;
using AMW.Core.IServices;
using AMW.Data.Models.Candidates;
using AMW.Data.Models.Diplomas;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMW.API.Controllers
{
    [AuthorizeJwt(Roles = nameof(Candidate))]
    public class DiplomaController : BaseController
    {

        #region Properties

        private readonly IRepositoryService<Diploma> diplomaService;
        #endregion

        #region Constructer

        public DiplomaController(IRepositoryService<Diploma> diplomaService)
        {
            this.diplomaService = diplomaService;
        }
        #endregion



        /// <summary>
        /// Creates a new diploma
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Diploma model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                model.CandidateId = int.Parse(User.Identity.Name);

                var result = await diplomaService.InsertOrUpdateAsync(model);

                return Ok(GetResponse(result, model.Id > 0 ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.Created));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }

        /// <summary>
        /// Gets the list of diplomas for a user
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }
            try
            {
                var result = await diplomaService.GetByFilterAsync(new DiplomaFilter()
                {
                    CandidateId = int.Parse(User.Identity.Name)
                });

                return Ok(GetResponse(result));
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
        public async Task<IActionResult> Filter([FromBody] DiplomaFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetModelValidationResponse<object>());
            }

            try
            {
                var result = await diplomaService.GetByFilterAsync(filter);

                return Ok(GetResponse(result));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);

                return Ok(GetExceptionResponse<object>(ex));
            }
        }

        /// <summary>
        /// Delete a diploma for 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("Bad request", "Id must be over 0");
                return Ok(GetModelValidationResponse<object>());
            }
            try
            {

                var result = await diplomaService.DeleteAsync(new Diploma()
                {
                    CandidateId = int.Parse(User.Identity.Name),
                    Id = id
                });

                var response = GetResponse(result);

                if (!result)
                {
                    //We just mark the count as 0 as nothing was changed
                    response.Count = 0;
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
