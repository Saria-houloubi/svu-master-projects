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

        public async Task<Blog> GetBlog(Guid id, bool addVisit = true)
        {
            try
            {
                var blog = await DbContext.Blogs.SingleOrDefaultAsync(item => item.Id == id);

                if (addVisit)
                {
                    blog.VisitCout++;
                    await DbContext.SaveChangesAsync();
                }

                return blog;
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
        public async Task<IEnumerable<Blog>> GetBlogs(int start, int count, bool loadImages)
        {
            try
            {
                return await DbContext.Blogs.Select(blog => new Blog
                {
                    Id = blog.Id,
                    AutherId = blog.AutherId,
                    PreviewContent = blog.PreviewContent,
                    Title = blog.Title,
                    CreationDate = blog.CreationDate,
                    LastUpdatedDate = blog.LastUpdatedDate,
                    ThumbnailBase64 = loadImages ? blog.ThumbnailBase64 : string.IsNullOrEmpty(blog.ThumbnailBase64) ? string.Empty : "Data",
                    ThumbnailMimeType = loadImages ? blog.ThumbnailMimeType : "",
                    VisitCout = blog.VisitCout

                }).OrderByDescending(item => item.VisitCout).Skip(start).Take(count).ToListAsync();
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
                if (blog.Id == Guid.Empty)
                {
                    //Then we need to add a new record
                    DbContext.Blogs.Add(blog);
                }
                else
                {
                    //Get the blokg from the db
                    var blogDb = await DbContext.Blogs.SingleOrDefaultAsync(item => item.Id == blog.Id);
                    //Update the wanted values
                    blogDb.Title = blog.Title;
                    blogDb.Content = blog.Content;
                    //Only update the image if new value provided
                    if (!string.IsNullOrEmpty(blog.ThumbnailMimeType))
                    {
                        blogDb.ThumbnailBase64 = blog.ThumbnailBase64;
                        blogDb.ThumbnailMimeType = blog.ThumbnailMimeType;
                    }
                    blogDb.Note = blog.Note;
                    blogDb.LastEditUserId = blog.LastEditUserId;
                    blogDb.PreviewContent = blog.PreviewContent;

                    blog = blogDb;
                    //Mark it as modified
                    DbContext.Entry(blogDb).State = EntityState.Modified;
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


        public async Task<bool> DeleteBlog(Guid id)
        {
            try
            {
                //Get the blog
                var blogDb = await DbContext.Blogs.SingleOrDefaultAsync(item => item.Id == id);

                if (blogDb != null)
                {
                    //Mark it as deleted
                    DbContext.Entry(blogDb).State = EntityState.Deleted;
                    //Delete the record
                    await DbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return false;
        }
    }
}
