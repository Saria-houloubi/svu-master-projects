using SVU.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// A service class to get any data set in the db
    /// </summary>
    public interface IDataSetDatabaseService
    {
        /// <summary>
        /// Gets a list of the heart disease records
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<HeartDisease>> GetHeartDiseaseRecords();
        /// <summary>
        /// Gets a lit of the tennis records
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Tennis>> GetTennisRecords();

        /// <summary>
        /// Get a db set based on it name 
        /// </summary>
        /// <param name="setName"></param>
        /// <returns></returns>
        Task<IEnumerable<object>> GetDbSet(string setName);
    }
}
