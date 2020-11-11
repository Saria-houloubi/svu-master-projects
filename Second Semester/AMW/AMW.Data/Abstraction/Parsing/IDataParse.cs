namespace AMW.Data.Abstraction.Parsing
{
    public interface IDataParse<TReader>
    {

        /// <summary>
        /// Parses the data based on reader
        /// </summary>
        /// <param name="reader"></param>
        void ParseData(TReader reader);
    }
}
