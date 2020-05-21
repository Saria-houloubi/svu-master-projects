namespace SVU.Web.UI.Models.TagHelpers
{
    /// <summary>
    /// A tag helper for the delete confirmation modal
    /// </summary>
    public class DeleteConfirmationModel
    {
        #region Properties
        public string Name { get; set; }
        public string Value { get; set; }
        public string DeleteCallbackFunctionName { get; set; }
        public string CancelCallbackFunctionName { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public DeleteConfirmationModel()
        {

        }
        #endregion
    }
}
