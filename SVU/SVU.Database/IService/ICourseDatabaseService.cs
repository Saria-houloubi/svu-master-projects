using SVU.Database.Models;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// The database layer for working with programs
    /// </summary>
    public interface ICourseDatabaseService
    {
        /// <summary>
        /// Gets the course with its full information
        /// </summary>
        /// <param name="name">the name of the course to look for</param>
        /// <returns></returns>
        Task<Course> GetCourse(string name);

    }

}
