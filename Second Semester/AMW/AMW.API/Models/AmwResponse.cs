using System.Collections.Generic;
using System.Net;

namespace AMW.API.Models
{
    public class AmwResponse<T>
    {
        #region Propeties
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }
        public int Count { get; set; }


        public bool HasErrors { get; set; }
        public List<ErrorModel> Errors { get; set; }
        #endregion
    }
}
