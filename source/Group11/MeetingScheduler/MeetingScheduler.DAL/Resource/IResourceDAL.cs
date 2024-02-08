using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.DAL
{
    public interface IResourceDAL
    {
        Task<List<Resource>> GetResourceDetails();
        Task<UserManagerResponse> CreateResources(Resource createResourceRequest);
        Task<UserManagerResponse> DeleteResource(Resource deleteResourceRequest);
        Task<List<Resource>> GetResourcesByMeeting(int MeetingId);
        Task<UserManagerResponse> UpdateResource(Resource resource);
    }
}
