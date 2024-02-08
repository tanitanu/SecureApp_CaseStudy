using MeetingScheduler.DAL.Models;
using MeetingScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.DAL
{
    public class ResourceDAL : IResourceDAL
    {
        #region Messages
        private const string CREATE_RESOURCE_NULL= "Create resource request is null";
        private const string CREATE_RESOURCE_SUCCESSFUL = "Resource created successfully";
        private const string CREATE_RESOURCE_NOT_CREATED = "Resource was not created";
        private const string CREATE_RESOURCE_EXCEPTION = "An error occurred while creating the resource: ";
        private const string DELETE_RESOURCE_SUCCESSFUL = "Resource deleted successfully";
        private const string DELETE_RESOURCE_NULL_CREATED = "Resource was not deleted";
        private const string DELETE_RESOURCE_EXCEPTION = "An error occurred while deleting the resource: ";
        private const string RESOURCE_NOT_FOUND = "Resource not found";
        private const string RESOURCE_NULL = "Resource request is null";
        private const string UPDATE_RESOURCE_FAILED = "No changes were made to the resource";
        private const string UPDATE_RESOURCE_SUCCESS = "Resource Details have been successfully updated";
        private const string UPDATE_RESOURCE_ERR = "An error occurred while updating the resource: ";
        #endregion
        public ResourceDAL() { }

        /// <summary>
        /// Get Resource Details
        /// </summary>
        /// <returns></returns>
        public async Task<List<Resource>> GetResourceDetails()
        {

            try
            {
                var context = new MeetingSchedulerContext();
                return await context.Resources.ToListAsync();
            }

            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Get Resources By Meeting id
        /// </summary>
        /// <param name="MeetingId"></param>
        /// <returns></returns>
        public async Task<List<Resource>?> GetResourcesByMeeting(int MeetingId)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {
                    var resources = await context.Resources
                        .Where(r => r.MeetingId == MeetingId)
                        .ToListAsync();
                    if (resources == null || !resources.Any())
                    {
                        return null;
                    }

                    return resources;
                }
            }
            catch (Exception ex)
            {
                return new List<Resource>();
            }
        }

        /// <summary>
        /// Create Meeting Resources
        /// </summary>
        /// <param name="createResourceRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> CreateResources(Resource createResourceRequest)
        {
            try
            {
                if (createResourceRequest == null)
                {
                    return new UserManagerResponse
                    {
                        Message = CREATE_RESOURCE_NULL,
                        IsSuccess = false,
                    };
                }

                using (var context = new MeetingSchedulerContext())
                {
                    var resource = new Resource()
                    {
                        ResourceId = createResourceRequest.ResourceId,
                        MeetingId = createResourceRequest.MeetingId,
                        ResourceEmailId = createResourceRequest.ResourceEmailId,
                        LastUpdtId = createResourceRequest.LastUpdtId,
                        LastUpdtTs = DateTime.Now,
                    };
                    context.Resources.Add(resource);
                    var result = await context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return new UserManagerResponse
                        {
                            Message = CREATE_RESOURCE_SUCCESSFUL,
                            IsSuccess = true,
                        };
                    }
                    else
                    {
                        return new UserManagerResponse
                        {
                            Message = CREATE_RESOURCE_NOT_CREATED,
                            IsSuccess = false,
                        };
                    }
                }
            }
            catch (Exception ex)
            {

                return new UserManagerResponse
                {
                    Message = CREATE_RESOURCE_EXCEPTION + ex.Message,
                    IsSuccess = false,
                };
            }
        }

        /// <summary>
        /// Delete resources
        /// </summary>
        /// <param name="deleteResourceRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> DeleteResource(Resource deleteResourceRequest)
        {
            try
            {
                if (deleteResourceRequest == null)
                {
                    return new UserManagerResponse
                    {
                        Message = RESOURCE_NULL,
                        IsSuccess = false,
                    };
                }

                using (var context = new MeetingSchedulerContext())
                {
                    var resource = await context.Resources.FirstOrDefaultAsync(m => m.MeetingId == deleteResourceRequest.MeetingId);

                    if (resource == null)
                    {
                        return new UserManagerResponse
                        {
                            Message = RESOURCE_NULL,
                            IsSuccess = false,
                        };
                    }

                    context.Resources.Remove(resource);
                    var result = await context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return new UserManagerResponse
                        {
                            Message = DELETE_RESOURCE_SUCCESSFUL,
                            IsSuccess = true,
                        };
                    }
                    else
                    {
                        return new UserManagerResponse
                        {
                            Message = RESOURCE_NOT_FOUND,
                            IsSuccess = false,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = DELETE_RESOURCE_EXCEPTION + ex.Message,
                    IsSuccess = false,
                };
            }
        }

        /// <summary>
        /// Update resources
        /// </summary>
        /// <param name="resourceRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> UpdateResource(Resource resourceRequest)
        {
            try
            {
                if (resourceRequest == null)
                {
                    return new UserManagerResponse
                    {
                        Message = RESOURCE_NULL,
                        IsSuccess = false,
                    };
                }

                using (var context = new MeetingSchedulerContext())
                {
                    context.Entry(resourceRequest).State = EntityState.Modified;

                    var result = await context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return new UserManagerResponse
                        {
                            Message = UPDATE_RESOURCE_SUCCESS,
                            IsSuccess = true,
                        };
                    }
                    else
                    {
                        return new UserManagerResponse
                        {
                            Message = UPDATE_RESOURCE_FAILED,
                            IsSuccess = false,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = UPDATE_RESOURCE_ERR + ex.Message,
                    IsSuccess = false,
                };
            }
        }

    }
}
