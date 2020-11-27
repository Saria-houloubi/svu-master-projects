namespace AMW.Shared.Models
{
    public class Pagination
    {
        #region Properties
        public int Page { get; set; }

        private int perPage;

        public int PerPage
        {
            get { return perPage; }
            set { perPage = value < 1 ? 1 : value; }
        }

        #endregion

        #region Methods
        public int GetSkipCount() => (Page - 1) * Page;
        #endregion
    }
}
