using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMW.API.Models.Swagger
{
    public class AwmSwaggerConfig
    {
        #region Properties

        public string Title { get; set; }

        public string Version { get; set; }

        public string UIREndPoint { get; set; }

        public string RoutePrfix { get; set; }
        #endregion
    }
}
