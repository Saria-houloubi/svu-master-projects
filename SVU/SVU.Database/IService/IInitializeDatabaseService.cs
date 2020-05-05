namespace SVU.Database.IService
{
    /// <summary>
    /// The functions to work with initializing the database 
    /// </summary>
    public interface IInitializeDatabaseService
    {
        /// <summary>
        /// Checks if the database is created and seed some startup data
        /// </summary>
        /// <returns></returns>
        bool InitailizeDatabase();

    }
}
