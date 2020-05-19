using SVU.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// The service class to for working with health blogs
    /// </summary>
    public interface IHealthBlogService
    {
        /// <summary>
        /// Adds or updates a record of type blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        Task<Blog> AddOrUpdateBlog(Blog blog);
        /// <summary>
        /// Get the list of blogs based on the start and the count to get
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IEnumerable<Blog>> GetBlogs(int start, int count);
        /// <summary>
        /// Deletes the blog with the sent id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteBlog(Guid id);


    }
}
