using SVU.Database.Models;
using System;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// The service class to work with application links
    /// </summary>
    public interface IApplicationLinksService
    {
        /// <summary>
        /// Gets the link information with the sent id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExternalLink> GetLink(Guid id);
    }
}
