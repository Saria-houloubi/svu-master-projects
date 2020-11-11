using AMW.API.Attributes;
using AMW.API.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AMW.API.Controllers.Base
{

    /// <summary>
    /// Base controller for cross functions and data
    /// </summary>
    [AuthorizeJwt]
    [Route("api/[controller]")]
    public abstract partial class BaseController : Controller
    {
        #region Properties
        public ILog log { get; protected set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constroller
        /// </summary>
        public BaseController()
        {
            log = LogManager.GetLogger(this.GetType().Name);
        }
        #endregion

        #region Methods

        protected virtual AmwResponse<T> GetModelValidationResponse<T>()
        {
            var res = new AmwResponse<T>
            {
                Errors = new List<ErrorModel>(),
                HasErrors = true,
                Status = System.Net.HttpStatusCode.BadRequest
            };

            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    res.Errors.Add(new ErrorModel()
                    {
                        Error = error.ErrorMessage,
                        Exception = error.Exception,
                    });
                }
            }

            return res;
        }

        protected virtual AmwResponse<T> GetExceptionResponse<T>(Exception ex)
        {
            var res = new AmwResponse<T>
            {
                HasErrors = true,
                Status = System.Net.HttpStatusCode.InternalServerError
            };

            do
            {
                res.Errors.Add(new ErrorModel
                {
                    Error = ex.Message,
                    Exception = ex
                });

                ex = ex.InnerException;
            } while (ex != null);

            return res;
        }

        protected virtual AmwResponse<object> GetResponse(object data, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new AmwResponse<object>()
            {
                Data = data,
                Count = 1,
                Status = status
            };

            if (data == null)
            {
                response.HasErrors = true;
                response.Count = 0;
            }
            else if (data is IEnumerable<object> dataList)
            {
                response.Count = dataList.Count();
            }

            return response;
        }

        protected virtual string GetJWTToken(object data)
        {


            return string.Empty;
        }
        #endregion
    }
}
