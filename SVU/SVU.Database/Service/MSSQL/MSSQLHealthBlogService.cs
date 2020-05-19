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
    public class MSSQLHealthBlogService : BaseMSSQLService, IHealthBlogService
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MSSQLHealthBlogService(SVUDbContext dbContext, ILoggingService loggingService)
            : base(dbContext, loggingService)
        {

        }

        #endregion
       
        public async Task<IEnumerable<Blog>> GetBlogs(int start, int count)
        {
            try
            {
                return await DbContext.Blogs.Skip(start).Take(count).ToListAsync();
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }

        public async Task<Blog> AddOrUpdateBlog(Blog blog)
        {
            try
            {
                //If there is no id assigned to the blog
                if (blog.Id != Guid.Empty)
                {
                    //Then we need to add a new record
                    DbContext.Blogs.Add(blog);
                }
                else
                {
                    //Update the values
                    DbContext.Attach(blog);
                    //Mark it as modified
                    DbContext.Entry(blog).State = EntityState.Modified;
                }

                await DbContext.SaveChangesAsync();

                return blog;
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
    }
}
