namespace SVU.Services.IServices
{
    /// <summary>
    /// The function that needed to execute the ID3 Algorithm
    /// </summary>
    public interface IID3AlgoService
    {

        /// <summary>
        /// Calculates the entropy using the count of positive and negative records
        /// </summary>
        /// <param name="positive"></param>
        /// <param name="negative"></param>
        /// <returns></returns>
        double CalculateEntropy(int positive, int negative);
    }
}
