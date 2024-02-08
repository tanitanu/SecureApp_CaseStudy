using MeetingScheduler.DAL;
using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Model
{
    public class ResourceHandler : IResourceHandler
    {
        private readonly IResourceDAL _resourceDAL;
        public ResourceHandler(IResourceDAL resourceDAL)
        {
            _resourceDAL = resourceDAL;
        }


        public async Task<List<Resource>> GetResourceDetails()
        {
            try
            {
                return await _resourceDAL.GetResourceDetails();
            }
            catch
            {

                throw;
            }
        }
        public Task<List<Resource>> GetResourcesByMeeting(int MeetingId)
        {
            try
            {
                return _resourceDAL.GetResourcesByMeeting(MeetingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserManagerResponse> CreateResources(Resource createResourceRequest)
        {
            try
            {
                return await _resourceDAL.CreateResources(createResourceRequest);
            }
            catch
            {

                throw;
            }
        }

        public async Task<UserManagerResponse> DeleteResource(Resource deleteResourceRequest)
        {
            try
            {
                return await _resourceDAL.DeleteResource(deleteResourceRequest);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<UserManagerResponse> UpdateResource(Resource resourceRequest)
        {
            try
            {
                return await _resourceDAL.UpdateResource(resourceRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

