using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using AsanaApi.Service;
using AsanaApi.Models;

namespace AsanaApi.Tests.Service
{
	[TestFixture]
	public class TasksTests : BaseTest
	{
		[Test]
		public void GetAllMyTasks()
		{
			TasksService taskService = new TasksService( API_KEY );
			Task[] tasks = taskService.GetTasks( new TasksQuery() { AssigneeId = TEST_USER_ID, WorkspaceId = TEST_WORKSPACE_ID } );
			Assert.IsNotNull( tasks, "we should have gotten some tasks back" );
			foreach( var item in tasks )
			{
				Assert.AreEqual( DateTime.MinValue, item.CreatedAt, "Since ReturnCompleteTaskRecords is false, CreatedAt shouldn't come back" );
			}
		}

		[Test]
		public void GetAllMyTasksWithDetails()
		{
			TasksService taskService = new TasksService( API_KEY );
			Task[] tasks = taskService.GetTasks( new TasksQuery() { AssigneeId = TEST_USER_ID, WorkspaceId = TEST_WORKSPACE_ID, ReturnCompleteTaskRecords = true } );
			Assert.IsNotNull( tasks, "we should have gotten some tasks back" );
			foreach( var item in tasks )
			{
				Assert.AreNotEqual( DateTime.MinValue, item.CreatedAt, "Since ReturnCompleteTaskRecords is true, CreatedAt should come back" );
			}
		}

		[Test]
		public void GetTasksInProject()
		{
			TasksService taskService = new TasksService( API_KEY );
			Task[] tasks = taskService.GetTasks( new TasksQuery() { ProjectId = TEST_PROJECT_ID } );
			Assert.IsNotNull( tasks, "we should have gotten some tasks back" );
			Assert.IsTrue( tasks.Length > 0, "task.Length should be > 0, but it was " + tasks.Length.ToString() );
			foreach( var item in tasks )
			{
				Assert.AreEqual( DateTime.MinValue, item.CreatedAt, "Since ReturnCompleteTaskRecords is false, CreatedAt shouldn't come back" );
			}


			Task[] tasks2 = taskService.GetTasksInProject( TEST_PROJECT_ID );
			Assert.IsNotNull( tasks2, "we should have gotten some tasks back" );
			Assert.IsTrue( tasks2.Length > 0, "task.Length should be > 0, but it was " + tasks2.Length.ToString() );
			foreach( var item in tasks2 )
			{
				Assert.AreEqual( DateTime.MinValue, item.CreatedAt, "Since ReturnCompleteTaskRecords is false, CreatedAt shouldn't come back" );
			}

			CompareTasks( tasks, tasks2 );
		}
		
		private void CompareTasks( Task[] tasks, Task[] tasks2 )
		{
			Assert.AreEqual( tasks.Length, tasks2.Length, "we should get the same tasks back" );
			for( int i = 0; i < tasks.Length; i++ )
			{
				CompareTasks( tasks[ i ], tasks2[ i ] );
			}
		}
		private void CompareTasks( Task task, Task task_2 )
		{
			Assert.AreEqual( task.Id, task_2.Id, "wrong id" );
			Assert.AreEqual( task.Name, task_2.Name, "wrong Name" );
			Assert.AreEqual( task.CreatedAt, task_2.CreatedAt, "wrong CreatedAt" );
			Assert.AreEqual( task.CompletedAt, task_2.CompletedAt, "wrong CompletedAt" );
			Assert.AreEqual( task.Notes, task_2.Notes, "wrong Notes" );
		}
	}
}
