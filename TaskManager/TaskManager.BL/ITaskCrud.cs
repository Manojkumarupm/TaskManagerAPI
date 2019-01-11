using System;
namespace TaskManager.BL
{
    interface ITaskCrud
    {
        string AddTask(TaskManager.DAL.TaskInformation i);
        System.Collections.Generic.IEnumerable<TaskManager.DAL.TaskInformation> GetAllTasks();
        TaskManager.DAL.TaskInformation GetTask(int TaskId);
        string RemoveTask(int TaskId);
        string UpdateTask(TaskManager.DAL.TaskInformation i);
    }
}
