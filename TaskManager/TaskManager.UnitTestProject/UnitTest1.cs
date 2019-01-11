using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Services.Controllers;
using System.Net.Http;
using System.Web.Http;
using TaskManager.DAL;
using System.Collections.Generic;
using System.Net;

namespace TaskManager.UnitTestProject
{
    [TestClass]
    public class TaskInformationControllerTest
    {
        [TestMethod]
        public void GetBySpecificId()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get(1);
            // Assert the result  
            TaskInformation taskInformation;
            Assert.IsTrue(response.TryGetContentValue<TaskInformation>(out taskInformation));
            Assert.AreEqual("Task 2", taskInformation.TaskDescription);
        }

        
        [TestMethod]
        public void GetErrorResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get(-1);
            // Assert the result  
            Assert.IsTrue(!response.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get(1);
            // Assert the result  
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetNotFoundResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get(-90);
            // Assert the result  
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void PostBadRequest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            TaskInformation i = new TaskInformation();
            i.ParentID =1;
            i.Priority = 5 ;
            i.TaskDescription = "This is beyond expected Range.......................................................................................................................................................................................................................................";
            i.StartDate = Convert.ToDateTime( "2018-12-07");
            i.EndDate =  Convert.ToDateTime( "2018-12-08");
            // Act on Test  
            var response = controller.Post(i);
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
            
        }
        [TestMethod]
        public void PostRequest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Post(new TaskInformation { TaskId= 0 ,ParentID = null, TaskDescription = "New Task for Parent", Priority = 5, StartDate = Convert.ToDateTime( "2018-12-08"), EndDate = Convert.ToDateTime( "2018-12-08") });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
            // Assert.AreEqual(i.ITRATE, TaskInformation.ITRATE);
        }
        [TestMethod]
        public void PutRequest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Put(new TaskInformation { TaskId = 3, ParentID = 1, TaskDescription = "New Task for Parent Updated", Priority = 5, StartDate = Convert.ToDateTime("2018-12-08"), EndDate = Convert.ToDateTime("2018-12-08") });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        [TestMethod]
        public void PutBadRequest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Put(new TaskInformation { TaskId = 0, ParentID = 1, TaskDescription = "Beyond expected Limit........................................................................................................................................................................................................", Priority = 5, StartDate = Convert.ToDateTime("2018-12-08"), EndDate = Convert.ToDateTime("2018-12-08") });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public void DeleteNotFoundRequest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Delete(-49);
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void DeleteRequest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Delete(2);
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
