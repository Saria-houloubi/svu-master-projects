using SVU.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// The service class to for working with health request
    /// </summary>
    public interface IHealthRequestService
    {
        /// <summary>
        /// Adds or updates teh values for the health request
        /// </summary>
        /// <param name="healthRequest"></param>
        /// <returns></returns>
        Task<HealthRequest> AddOrUpdateRequest(HealthRequest healthRequest);
        /// <summary>
        /// Get the list of healthrequest based on the start and the count to get
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="isAdmin">If true will load all request</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<HealthRequest>> GetHealthRequests(int start, int count, Guid userId, bool isAdmin = false);
        /// <summary>
        /// Adds or updates a reply for a health request
        /// </summary>
        /// <param name="healthRequestReply"></param>
        /// <returns></returns>
        Task<HealthRequestReply> AddOrUpdateRequestReply(HealthRequestReply healthRequestReply);
        /// <summary>
        /// Gets the health request with its replies 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<HealthRequest> GetHealthRequest(Guid requestId);

    }
}
