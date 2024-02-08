#region [Using]
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;
using TaskManagement.Api.Data;
using TaskManagement.Api.Models.Domain;
using TaskManagement.Api.Models.DTO;
using TaskManagement.Api.Repositories;
#endregion [Using]

namespace TaskManagement.Api.Controllers
{
    #region [Summary]
    ///<author>Poornima Shanbhag</author>
    ///<date>02-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>TaskController</class>
    /// <summary>
    /// This is the controller class for task
    /// History
    ///     02-Nov-2023: Poornima Shanbhag: Updated for logger, Repository and AutoMapper
    ///     09-Nov-2023: sayyad, shaheena: Updated Filtering, Sort and Pagination
    ///     10-Nov-2023: Poornima Shanbhag: Added GetAllStatus action method
    ///     22-Nov-2023: sayyad, shaheena: Added logging and exception handling for all action methods
    /// </summary>
    #endregion [Summary]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        #region [Private Variables]
        private readonly TaskDBContext dbContext;
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TasksController> logger;
        #endregion [Private Variables]

        #region [Constructor]
        public TasksController(TaskDBContext dbContext, ITaskRepository taskRepository, IMapper mapper, ILogger<TasksController> logger)
        {
            this.dbContext = dbContext;
            this.taskRepository = taskRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

     
        #endregion [Constructor]

        #region [Action Methods]
        #region [GetAll]
        /// <summary>
        /// Get all Tasks
        /// </summary>
        /// <returns>All the tasks</returns>
        /// Ex: Get: https://localhost:<port>/api/tasks
        /// 
        /// Get: /api/task?filterOn=AssignedTo&filterQuery=Shaheena
        [HttpGet]
        public async Task<IActionResult> GetAll
            (
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int PageNumber = 1,
            [FromQuery] int PageSize = 1000)
        {
            try
            {
                logger.LogInformation("GetAll Action method of TaskController was involked");
                //throw new Exception("This is a custom exception");

                // Get data from DB as Data Models
                var taskDM = await this.taskRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, PageNumber, PageSize);

                // Map domain models to DTO
                var taskDTO = mapper.Map<List<TaskDto>>(taskDM);
                logger.LogInformation($"Finished GetAll Action method of TaskController with data: {JsonSerializer.Serialize(taskDM)}");

                // Return DTO
                return Ok(taskDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }
    #endregion [GetAll]

    #region [GetById]
    /// <summary>
    /// Get Task Details by Task Id
    /// </summary>
    /// <param name="id"> Task Id</param>
    /// <returns>Task Details by Id</returns>
    /// Ex: Get: https://localhost:<port>/api/tasks/{id}
    [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                logger.LogInformation("GetById Action method of TaskController was involked");

                // Get domain model from DB
                var taskDM = await this.taskRepository.GetByIdAsync(id);

                if (taskDM == null)
                {
                    return NotFound();
                }

                // Map Domain Model to DTO
                var taskDTO = mapper.Map<TaskDto>(taskDM);

                logger.LogInformation($"Finished GetAll Action method of TaskController with data: {JsonSerializer.Serialize(taskDM)}");


                return Ok(taskDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
           
        }
        #endregion [GetById]

        #region [Create]
        /// <summary>
        /// Create new Task
        /// </summary>
        /// <param name="addTaskRequest">DTO to add new Task</param>
        /// <returns>New Task details</returns>
        /// Ex: Post: https://localhost:<port>/api/tasks 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTaskRequestDto addTaskRequest)
        {
            try
            {

                logger.LogInformation("Create Action method of TaskController was involked");


                if (ModelState.IsValid)
                {
                    // Map DTO to Domain Model
                    var taskDomainModel = mapper.Map<Tasks>(addTaskRequest);

                    // Use DM to create task
                    await taskRepository.CreateAsync(taskDomainModel);

                    // Map Domain Model to DTO
                    var taskDTO = mapper.Map<TaskDto>(taskDomainModel);

                    logger.LogInformation($"Finished GetAll Action method of TaskController with data: {JsonSerializer.Serialize(taskDomainModel)}");


                    return CreatedAtAction(nameof(GetById), new { Id = taskDTO.Id }, taskDTO);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }
        #endregion [Create]

        #region [Update]
        /// <summary>
        /// Update an existing Task
        /// </summary>
        /// <param name="updateTaskRequestDto">DTO to update Task</param>
        /// <returns>Updated Task details</returns>
        /// Ex: Post: https://localhost:<port>/api/tasks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskRequestDto updateTaskRequestDto)
        {
            try
            {
                logger.LogInformation("Update Action method of TaskController was involked");

                if (ModelState.IsValid)
                {
                    var taskDM = mapper.Map<Tasks>(updateTaskRequestDto);

                    taskDM = await taskRepository.UpdateAsync(id, taskDM);

                    if (taskDM == null) { return NotFound(); }

                    logger.LogInformation($"Finished GetAll Action method of TaskController with data: {JsonSerializer.Serialize(taskDM)}");


                    return Ok(mapper.Map<TaskDto>(taskDM));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }



        }
        #endregion [Update]

        #region [Delete]
        /// <summary>
        /// Delete an existing Task
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <returns>Deleted Task details</returns>
        /// Ex: Post: https://localhost:<port>/api/tasks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                logger.LogInformation("Delete Action method of TaskController was involked");

                var taskDM = await taskRepository.DeleteAsync(id);

                if (taskDM == null)
                {
                    return NotFound();
                }

                logger.LogInformation($"Finished GetAll Action method of TaskController with data: {JsonSerializer.Serialize(taskDM)}");

                return Ok(mapper.Map<TaskDto>(taskDM));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }


        }
        #endregion [Delete]

        #region [GetAllStatus]
        /// <summary>
        /// Get all Tasks
        /// </summary>
        /// <returns>All the status</returns>
        /// Ex: Get: https://localhost:<port>/api/tasks/Status?filterOn=Id&filterQuery=1
        [HttpGet]
        [Route("api/[controller]/Status")]
        public async Task<IActionResult> GetAllStatus([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            try
            {
                logger.LogInformation("GetAllStatus Action method of TaskController was involked");

                var statusDM = await this.taskRepository.GetAllStatusAsync(filterOn, filterQuery);

                // Map domain models to DTO
                var statusDTO = mapper.Map<List<StatusDto>>(statusDM);

                logger.LogInformation($"Finished GetAll Action method of TaskController with data: {JsonSerializer.Serialize(statusDM)}");

                // Return DTO
                return Ok(statusDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message); 
                throw;
            }
           
        }
        #endregion [GetAllStatus]
        #endregion [Action Methods]
    }
}
