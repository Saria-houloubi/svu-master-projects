using SVU.Database.Models;
using System;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// The functions to get information on the serving files
    /// </summary>
    public interface IFileDatabaseService
    {
        /// <summary>
        /// Gets the file info with the sent id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExternalLink> GetLinkInfo(Guid id);

    }
}
