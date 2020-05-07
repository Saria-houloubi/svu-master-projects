using SVU.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
