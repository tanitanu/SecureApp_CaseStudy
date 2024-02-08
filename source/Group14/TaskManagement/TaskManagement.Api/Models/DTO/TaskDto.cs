namespace TaskManagement.Api.Models.DTO
{
    #region [Summary]
    ///<author>Poornima Shanbhag</author>
    ///<date>02-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>Tasks</class>
    /// <summary>
    /// This is the DTO class for Task
    /// </summary>
    #endregion [Summary]
    public class TaskDto
    {
        #region [Properties]
        public Guid Id { get; set; }
        public string AssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string? Comments { get; set; }
		public int StatusId { get; set; }
        public StatusDto Status { get; set; }
        #endregion [Properties]
    }
}
