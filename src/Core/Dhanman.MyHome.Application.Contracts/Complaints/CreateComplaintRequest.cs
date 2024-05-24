namespace Dhanman.MyHome.Application.Contracts.Complaints
{
    public class CreateComplaintRequest
    {
        #region Properties
        public string Subject { get; set; }
        public string Description { get; set; }
        public string DocLink { get; set; }
        public DateTime PrefferedTime { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int PriorityId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime OccuredDate { get; set; }
        public DateTime PrefferedDate { get; set; }
        public bool IsUrgent { get; set; }
        #endregion

        #region Constructor
        public CreateComplaintRequest() { }
        #endregion
    }
}