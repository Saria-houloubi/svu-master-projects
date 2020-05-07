using SVU.Logging.IServices;
using SVU.Services.IServices;
using System;

namespace SVU.Services.Services
{
    /// <summary>
    /// The implementation for the <see cref="IID3AlgoService"/>
    /// </summary>
    public class ID3AlgoService : IID3AlgoService
    {
        #region Properties
        public ILogginService LogginService { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ID3AlgoService(ILogginService logginService)
        {
            LogginService = logginService;
        }
        #endregion


        public double CalculateEntropy(int positive, int negative)
        {
            try
            {
                //Calculate the avgs
                var avgPositive = positive / (positive + negative);
                var avgNegative = negative / (positive + negative);
                //Return the result or the rule
                return -(avgPositive) * Math.Log(avgPositive, 2) - (avgNegative) * Math.Log(avgNegative, 2);
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return 0;
        }
    }
}
