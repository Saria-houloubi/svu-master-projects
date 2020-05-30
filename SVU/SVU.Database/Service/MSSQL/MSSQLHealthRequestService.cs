using Microsoft.EntityFrameworkCore;
using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Database.Service.MSSQL.Base;
using SVU.Logging.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SVU.Database.Service.MSSQL
{
    /// <summary>
    /// The mssql implementation for <see cref="IHealthBlogService"/>
    /// </summary>
    public class MSSQLHealthRequestService : BaseMSSQLService, IHealthRequestService
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MSSQLHealthRequestService(SVUDbContext dbContext, ILoggingService loggingService)
            : base(dbContext, loggingService)
        {

        }

        #endregion

        public async Task<HealthRequest> AddOrUpdateRequest(HealthRequest healthRequest)
        {
            try
            {
                //If there is no id assigned to the blog
                if (healthRequest.Id == Guid.Empty)
                {
                    //Then we need to add a new record
                    DbContext.HealthRequests.Add(healthRequest);
                }
                else
                {
                    //Get the blokg from the db
                    var healthrequestDb = await DbContext.HealthRequests.SingleOrDefaultAsync(item => item.Id == healthRequest.Id);
                    //Update the wanted values
                    healthrequestDb.Subject = healthRequest.Subject;
                    healthrequestDb.Content = healthRequest.Content;
                    healthrequestDb.Note = healthRequest.Note;

                    healthRequest = healthrequestDb;
                    //Mark it as modified
                    DbContext.Entry(healthrequestDb).State = EntityState.Modified;
                }

                await DbContext.SaveChangesAsync();

                return healthRequest;
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }

        public async Task<HealthRequestReply> AddOrUpdateRequestReply(HealthRequestReply healthRequestReply)
        {
            try
            {
                //If there is no id assigned to the reply
                if (healthRequestReply.Id == Guid.Empty)
                {
                    //Then we need to add a new record
                    DbContext.HealthRequestReplies.Add(healthRequestReply);

                    //Get the request
                    var healthRequest = await DbContext.HealthRequests.SingleOrDefaultAsync(item => item.Id == healthRequestReply.RequestId);

                    healthRequest.ReplyCount++;

                    DbContext.Entry(healthRequest).State = EntityState.Modified;
                }
                else
                {
                    //Get the blokg from the db
                    var reply = await DbContext.HealthRequestReplies.SingleOrDefaultAsync(item => item.Id == healthRequestReply.Id);
                    //Update the wanted values
                    reply.Content = healthRequestReply.Content;
                    reply.Note = healthRequestReply.Note;

                    healthRequestReply = reply;
                    //Mark it as modified
                    DbContext.Entry(reply).State = EntityState.Modified;
                }

                await DbContext.SaveChangesAsync();

                return healthRequestReply;
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }

        public async Task<HealthRequest> GetHealthRequest(Guid requestId)
        {
            try
            {
                return await DbContext.HealthRequests.Include(item => item.Replies).SingleOrDefaultAsync(item => item.Id == requestId);
            }
            catch (Exception ex)
            {
                LogginService.LogException(ex);
            }

            return null;
        }

        public async Task<IEnumerable<HealthRequest>> GetHealthRequests(int start, int count, Guid userId, bool isAdmin = false)
        {

            try
            {
                return await DbContext.HealthRequests.Include(item => item.Replies).Where(item => isAdmin ? true : item.HealthUserId == userId)
                    .OrderBy(item => item.ReplyCount).ThenByDescending(item => item.CreationDate)
                    .Skip(start)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                LogginService.LogException(ex);
            }

            return null;
        }
    }
}
