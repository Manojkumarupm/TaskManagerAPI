using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL;
namespace TaskManager.BL
{
    /// <summary>
    /// This class will take care of inserting, updating, deleting and getting the task information from the database. everything done based on the entity
    /// </summary>
    public class TaskCrud : ITaskCrud
    {
        /// <summary>
        /// Getting All the Task Information from the database
        /// </summary>
        /// <returns>List of TaskInformation</returns>
        public IEnumerable<TaskInformation> GetAllTasks()
        {
            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                return PE.TaskInformations.ToList();
            }
        }
        /// <summary>
        /// Getting the specific Task from the database
        /// </summary>
        /// <param name="TaskId">TaskId</param>
        /// <returns>TaskInformation</returns>
        public TaskInformation GetTask(int TaskId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                TaskInformation i = PE.TaskInformations.Where(x => x.TaskId == TaskId).FirstOrDefault();
                return i;
            }
        }
        /// <summary>
        /// Add a particular task to database
        /// </summary>
        /// <param name="i">TaskInformation</param>
        /// <returns>Status of the Operation</returns>
        public string AddTask(TaskInformation i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.TaskInformations.Add(i);
                    PE.SaveChanges(); 
                    
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Failed" + ex.Message;
            }
        }
        /// <summary>
        /// Update a particular task to database.
        /// </summary>
        /// <param name="i">TaskInformation</param>
        /// <returns>Status of The Task</returns>
        public string UpdateTask(TaskInformation i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    TaskInformation value = PE.TaskInformations.Where(x => x.TaskId == i.TaskId).FirstOrDefault();
                    value.Priority = i.Priority;
                    value.StartDate = i.StartDate;
                    value.EndDate = i.EndDate;
                    value.ParentID = i.ParentID;
                    value.TaskDescription = i.TaskDescription;
                    value.IsTaskCompleted = i.IsTaskCompleted;
                    PE.SaveChanges();
                    return "Updated";
                }
            }
            catch (Exception ex)
            {
                return "failed : " + ex.Message;
            }
        }
        /// <summary>
        /// Remove Task from the database
        /// </summary>
        /// <param name="TaskId">TaskId</param>
        /// <returns>Status of the Operation</returns>
        public string RemoveTask(int TaskId)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    TaskInformation I = PE.TaskInformations.Where(x => x.TaskId == TaskId).FirstOrDefault();
                    if (I == null)
                    {
                        return  "Task with Id " + TaskId + " Not found";
                    }
                    else
                    {
                        PE.Entry(I).State = System.Data.Entity.EntityState.Deleted;
                        PE.SaveChanges();
                        return "Success";
                    }
                }
              
            }
            catch (Exception ex)
            {
                return "Failed : " + ex.Message;
            }

        }
    }
}
