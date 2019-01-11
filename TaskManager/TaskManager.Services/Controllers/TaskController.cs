using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TaskManager.BL;
using TaskManager.DAL;
namespace TaskManager.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    
    public class TaskController : ApiController
    {
        public TaskCrud TaskDetailsGetter { get; set; }
        public TaskController()
        {
            TaskDetailsGetter = new TaskCrud();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TaskInformation> Get()
        {
            return TaskDetailsGetter.GetAllTasks();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        [HttpGet]
         [ResponseType(typeof(TaskInformation))]
        public HttpResponseMessage Get(int TaskId)
        {

            TaskInformation i = TaskDetailsGetter.GetTask(TaskId);
            if (i != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, i);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Task Id : " + TaskId + " Not Found");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public HttpResponseMessage Post([FromBody] TaskInformation i)
        {
            string Result = null;
            try
            {
                Result = TaskDetailsGetter.AddTask(i);
                if (Result.Equals("Success"))
                {
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.Created, i);
                    return Message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put([FromBody] TaskInformation i)
        {
            string Result = null;
            try
            {
                Result = TaskDetailsGetter.UpdateTask(i);
                if (Result.Equals("Updated"))
                {
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, i);
                    return Message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Result);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        [ResponseType(typeof(TaskInformation))]
        public HttpResponseMessage Delete(int TaskId)
        {
            string Result = null;
            try
            {
                Result = TaskDetailsGetter.RemoveTask(TaskId);
                if (Result.Equals("Success"))
                {
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, Result);
                    return Message;
                }
                else if (Result.Contains(" Not found"))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, Result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Result);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
