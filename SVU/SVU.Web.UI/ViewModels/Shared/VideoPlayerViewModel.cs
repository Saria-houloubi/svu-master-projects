using SVU.Web.UI.ViewModels.Base;

namespace SVU.Web.UI.ViewModels.Shared
{
    /// <summary>
    /// the view model to play a vide
    /// </summary>
    public class VideoPlayerViewModel : BaseViewModel
    {
        #region Properties

        public string Title { get; set; }
        public string VideoSrc { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public VideoPlayerViewModel()
        {

        }
        #endregion
    }
}
