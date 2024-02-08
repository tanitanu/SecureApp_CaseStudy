#region [Using]
using AutoMapper;
using TaskManagement.Api.Models.Domain;
using TaskManagement.Api.Models.DTO;
#endregion [Using]

namespace TaskManagement.Api.Mappings
{
    #region [Summary]
    ///<author>Poornima Shanbhag</author>
    ///<date>02-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>AutoMapperProfiles</class>
    /// <summary>
    /// This is the automapper class for task domain model and task related DTOs
    /// </summary>
    #endregion [Summary]
    public class AutoMapperProfiles: Profile
    {
        #region [Constructor]
        public AutoMapperProfiles()
        {
            CreateMap<Tasks, TaskDto>().ReverseMap();
            CreateMap<Tasks, UpdateTaskRequestDto>().ReverseMap();
            CreateMap<Tasks, AddTaskRequestDto>().ReverseMap();
            CreateMap<Status, StatusDto>().ReverseMap();
        }
        #endregion [Constructor]
    }
}
