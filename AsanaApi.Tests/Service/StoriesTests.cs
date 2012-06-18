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
	public class StoriesTests : BaseTest
	{
		[Test]
		public void GetStories()
		{
			TasksService ts = new TasksService( API_KEY );
			Task t = ts.GetTasksInProject( TEST_PROJECT_ID )[ 0 ];

			StoriesService service = new StoriesService( API_KEY );
			Story[] stories = service.GetStories( t );
			Assert.IsNotNull( stories, "we should have gotten some stories back" );
			Assert.IsTrue( stories.Length > 0, "stories.Length  was only " + stories.Length.ToString() );
			foreach( var item in stories )
			{
				Assert.IsNotNull( item.Text, "text should always come back" );
				Assert.IsTrue( item.Type.HasValue, "type should always be populated" );
				Assert.IsNotNull( item.CreatedBy, "CreatedBy should be back now" );
			}
		}

		[Test]
		public void GetTotalStory()
		{
			TasksService ts = new TasksService( API_KEY );
			Task t = ts.GetTasksInProject( TEST_PROJECT_ID )[ 0 ];

			StoriesService service = new StoriesService( API_KEY );
			Story[] stories = service.GetStories( t, StoriesService.AllOptionalFieldsForStories );
			Assert.IsNotNull( stories, "we should have gotten some stories back" );
			Assert.IsTrue( stories.Length > 0, "stories.Length  was only " + stories.Length.ToString() );
			foreach( var item in stories )
			{
				Assert.IsNotNull( item.Text, "text should always come back" );
				Assert.IsTrue( item.Type.HasValue, "type should always be populated" );
				Assert.IsNotNull( item.CreatedBy, "CreatedBy should be back now" );
				Assert.IsNotNull( item.Target, "Target should be back now" );
				Assert.IsTrue( item.Source.HasValue, "Source should be back now" );
			}
		}
	}
}
